using _2.UsersManagement.Application.Interfaces.Permissions;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions;
using _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions.Menus;
using _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions.Views;
using _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions.Views.Grouped;
using _2.UsersManagement.Application.DTOs.Permissions.In_Services.GetPermissions.Views.NotGrouped;
using _2.UsersManagement.Application.Transients;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Permissions
{
    public class PermissionsService : IPermissionsService
    {
        private readonly IGenericUnit _gUnit;

        public PermissionsService(IGenericUnit gUnit)
        {
            _gUnit = gUnit;
        }

        //----------------------------------PUBLIC--------------------------------------------
        public async Task<PermissionsDto> GetUserPermissions(int operation, string id, string language)
        {
            await using var connection = new SqlConnection(ConnectionString.Connection);
            var command = PrepareSqlCommand(operation, id, connection);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            var (menus, vistas, vistasControles)
                = ProcessSqlQueryResults(command.Parameters["@jsonMenus"],
                    command.Parameters["@jsonViewsGroupControls"], command.Parameters["@jsonViewsControls"]);
            connection.Close();

            JoinVistasControlsAndVistas(vistas, vistasControles);

            var mainMenus = BuildMainMenus(menus);
            menus.Clear();

            FilterInactiveElements(mainMenus, vistas);

            var permissions = new PermissionsDto
            {
                Menus = menus,
                MainMenus = mainMenus,
                Vistas = vistas,
            };

            permissions = await TranslatePermissions(permissions, language);

            return permissions;
        }

        //----------------------------------PRIVATE-------------------------------------------

        private static SqlCommand PrepareSqlCommand(int operation, string id, SqlConnection connection)
        {
            var command = new SqlCommand("CNT_SP_GETPERMISSONS", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@operationType", SqlDbType.Int) { Value = operation });
            command.Parameters.Add(new SqlParameter("@idSearch", SqlDbType.NVarChar, 50) { Value = id });
            command.Parameters.Add(new SqlParameter("@jsonMenus", SqlDbType.NVarChar, int.MaxValue)
                { Direction = ParameterDirection.InputOutput, Value = null });
            command.Parameters.Add(new SqlParameter("@jsonViewsGroupControls", SqlDbType.NVarChar, int.MaxValue)
                { Direction = ParameterDirection.InputOutput, Value = null });
            command.Parameters.Add(new SqlParameter("@jsonViewsControls", SqlDbType.NVarChar, int.MaxValue)
                { Direction = ParameterDirection.InputOutput, Value = null });
            return command;
        }

        private (List<MenusDto> menus, List<ViewDto> vistas, List<ViewsControlsDto> vistasControles)
            ProcessSqlQueryResults(IDataParameter menusParameter, IDataParameter viewsGroupParameter,
                IDataParameter viewsParameter)
        {
            var jsonMenus = menusParameter.Value as string;
            var jsonViewsGroup = viewsGroupParameter.Value as string;
            var jsonViews = viewsParameter.Value as string;

            var rootMenus = JsonConvert.DeserializeObject<RootMenusDto>(jsonMenus);
            var menus = rootMenus.Menus;

            var vistasRoot = JsonConvert.DeserializeObject<RootViewsGroupDto>(jsonViewsGroup);
            var vistas = vistasRoot.Vistas;

            var vistasControlesRoot = JsonConvert.DeserializeObject<RootViewsControlsDto>(jsonViews);
            var vistasControles = vistasControlesRoot.VistasControles;

            return (menus, vistas, vistasControles);
        }

        private void JoinVistasControlsAndVistas(IReadOnlyCollection<ViewDto> vistas,
            List<ViewsControlsDto> vistasControles)
        {
            foreach (var vistaControl in vistasControles)
            {
                var vista = vistas.FirstOrDefault(v => v.Id == vistaControl.Id);
                if (vista is not null)
                {
                    vista.Controles = vistaControl.Controles.Where(c => c.Active).ToList();
                }
            }
        }

        private List<MenusDto> BuildMainMenus(ICollection<MenusDto> menus)
        {
            //var mainMenus = new List<MenusDto>();

            /*var homeMenu = menus.FirstOrDefault(m => m.NameMenu.Equals("Home", StringComparison.OrdinalIgnoreCase));
            var settingsMenu =
                menus.FirstOrDefault(m => m.NameMenu.Equals("Settings", StringComparison.OrdinalIgnoreCase));

            if (homeMenu is not null)
            {
                mainMenus.Add(homeMenu);
                menus.Remove(homeMenu);
            }

            if (settingsMenu is not null)
            {
                mainMenus.Add(settingsMenu);
                menus.Remove(settingsMenu);
            }*/

            foreach (var menu in menus.Where(i => i.SubMenus is not null))
            {
                menu.SubMenus = menu.SubMenus.OrderBy(sm => sm.OrderMenu).ToList();
            }

            return menus.OrderBy(m => m.OrderMenu).ToList();
        }

        private void FilterInactiveElements(List<MenusDto> menus, List<ViewDto> vistas)
        {
            menus = menus.Where(m => m.Active).ToList();
            vistas = vistas.Where(v => v.Active).ToList();

            foreach (var vista in vistas)
            {
                if (vista.GrupoControles is not null)
                {
                    vista.GrupoControles = vista.GrupoControles.Where(gc => gc.Active).ToList();
                }

                if (vista.Controles is not null)
                {
                    vista.Controles = vista.Controles.Where(c => c.Active).ToList();
                }
            }

            foreach (var menu in menus.Where(menu => menu.SubMenus is not null))
            {
                menu.SubMenus = menu.SubMenus.Where(sm => sm.Active).ToList();
            }
        }

        private async Task<PermissionsDto> TranslatePermissions(PermissionsDto permissions, string language)
        {
            await TranslateMenus(permissions, language);
            await TranslateViews(permissions, language);
            return permissions;
        }

        private async Task TranslateMenus(PermissionsDto permissions, string language)
        {
            if (permissions.Menus is not null)
            {
                foreach (var menu in permissions.MainMenus)
                {
                    await TranslateMenu(menu, language);
                    if (menu.SubMenus is null) continue;
                    foreach (var submenu in menu.SubMenus)
                    {
                        await TranslateSubMenu(submenu, language);
                    }
                }
            }
        }

        private async Task TranslateViews(PermissionsDto permissions, string language)
        {
            if (permissions.Vistas is not null)
            {
                foreach (var vista in permissions.Vistas)
                {
                    await TranslateView(vista, language);
                    foreach (var group in vista.GrupoControles)
                    {
                        await TranslateGroupControl(group, language);
                    }
                    foreach (var control in vista.Controles)
                    {
                        await TranslateControl(control, language);
                    }
                }
            }
        }

        private async Task TranslateMenu(MenusDto menu, string language)
        {
            var id = await _gUnit.Menu.GetEntityPropertyOrNull(menu.NameMenu, "nameMenu", "id");
            if (id is not null)
            {
                var nameMenu = await _gUnit.Language.GetEntityPropertyOrNull(id, "idControl", language);
                menu.NameMenu = nameMenu ?? menu.NameMenu;
            }
        }

        private async Task TranslateSubMenu(SubMenuDto submenu, string language)
        {
            var id = await _gUnit.Menu.GetEntityPropertyOrNull(submenu.NameMenu, "nameMenu", "id");
            if (id is not null)
            {
                var nameMenu = await _gUnit.Language.GetEntityPropertyOrNull(id, "idControl", language);
                submenu.NameMenu = nameMenu ?? submenu.NameMenu;
            }
        }

        private async Task TranslateView(ViewDto view, string language)
        {
            var id = await _gUnit.View.GetEntityPropertyOrNull(view.NameView, "nameView", "id");
            if (id is not null)
            {
                var nameView = await _gUnit.Language.GetEntityPropertyOrNull(id, "idControl", language);
                view.NameView = nameView ?? view.NameView;
            }
        }

        private async Task TranslateGroupControl(ControlsGroupDto groupControl, string language)
        {
            foreach (var control in groupControl.Controles)
            {
                await TranslateControl(control, language);
            }
        }

        private async Task TranslateControl(ControlDto control, string language)
        {
            var id = await _gUnit.Control.GetEntityPropertyOrNull(control.NameControl, "nameControl", "id");
            if (id is not null)
            {
                var nameControl = await _gUnit.Language.GetEntityPropertyOrNull(id, "idControl", language);
                control.NameControl = nameControl ?? control.NameControl;
            }
        }
    }
}