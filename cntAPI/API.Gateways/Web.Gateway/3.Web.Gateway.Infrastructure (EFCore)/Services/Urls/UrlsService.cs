using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _2.Web.Gateway.Application.Interfaces.Urls;

namespace _3.Web.Gateway.Infrastructure__EFCore_.Services.Urls
{
    public class UrlsService : IUrlsService
    {
        //------------------------GENERIC CONTROLLER-------------------------------
        public string GetAll(string controller, string entity, string userid, string host)
        {
            return $"{host}/api/Generic/GetAllGeneric?controllerName={controller}&entity={entity}&userId={userid}";
        }

        public string GetAllById(string controller, string entity, string reference, string userid, string host)
        {
            return $"{host}/api/Generic/GetAllGenericById?controllerName={controller}&entity={entity}&userid={userid}&Reference={reference}";
        }

        public string GetSimpleById(string controller, string entity, string reference, string userid, string host)
        {
            return $"{host}/api/Generic/GetSimpleGenericById?controllerName={controller}&entity={entity}&Reference={reference}&userid={userid}";
        }

        public string GetAllOneField(string controller, string entity, string userid, string host)
        {
            return $"{host}/api/Generic/GetAllGenericOneField?controllerName={controller}&entity={entity}&userid={userid}";
        }

        public string GetSimpleOneField(string controller, string entity, string id, string userid, string host)
        {
            return $"{host}/api/Generic/GetSimpleGenericOneFieldById?controllerName={controller}&entity={entity}&Reference={id}&userId={userid}";
        }

        public string GetEnum(string controller, string name, string userid, string host)
        {
            return $"{host}/api/Generic/GetEnum?controllerName={controller}&name={name}&userid={userid}";
        }

        public string Insert(string controllerName, string entity, string userid, string host)
        {
            return $"{host}/api/Generic/InsertGeneric?controllerName={controllerName}&entity={entity}&userid={userid}";
        }

        public string Update(string controllerName, string entity, string userid, string host)
        {
            return $"{host}/api/Generic/UpdateGeneric?controllerName={controllerName}&entity={entity}&userid={userid}";
        }

        //---------------------------ZIPCODES CONTROLLER---------------------------------------------------------

        public string GetSimpleAddressByZipcode(string controller, string reference, string userid, string host)
        {
            return $"{host}/api/Zipcodes/GetSimpleAddressByZipcode?controller={controller}&zipcode={reference}&userid={userid}";
        }

        //------------------------------LOGIN CONTROLLER---------------------------------------------------

        public string Login(string language, string host)
        {
            return $"{host}/api/Login/login?language={language}";
        }

        public string Logout(string reference, string host)
        {
            return $"{host}/api/Login/logout?user={reference}";
        }

        public string RefreshToken(string userid, string host)
        {
            return $"{host}/api/Login/Get-Refresh-Token?userid={userid}";
        }

        //-----------------------------REGISTER USER CONTROLLER--------------------------------------------

        public string RegisterUserAndProfile(string host)
        {
            return $"{host}/api/RegisterUser/RegisterUserAndProfile";
        }

        public string RegisterUserRoleAndUserType(string host)
        {
            return $"{host}/api/RegisterUser/RegisterUserRoleAndUserType";
        }

        //-----------------------------EDIT USER CONTROLLER-------------------------------------------------
        public string ChangePassword(string controller, string host)
        {
            return $"{host}/api/EditUser/ChangePassword?controllerName={controller}";
        }
        
        //-----------------------------DOCUMENTS CONTROLLER-------------------------------------------------
        public string RegisterDocumentsUser(string controller, string entity, string host)
        {
            return $"{host}/api/Documents/RegisterDocuments?entity={controller}&subEntity={entity}";
        }
        
        public string GetDocument(string reference, string userid, string language, string host)
        {
            return $"{host}/api/Documents/GetDocument?id={reference}&userid={userid}&language={language}";
        }
        
        //-----------------------------APPLY DISTRIBUTOR CONTROLLER----------------------------------------
        public string RegisterProspect(string host)
        {
            return $"{host}/api/ApplyDistributor/RegisterProspect";
        }

        public string AssignAppointment(string host)
        {
            return $"{host}/api/ApplyDistributor/AssignAppointment";
        }

        public string RegisterDistributor(string host)
        {
            return $"{host}/api/ApplyDistributor/RegisterDistributor";
        }
        
        public string RegisterEndorsement(string host)
        {
            return $"{host}/api/ApplyDistributor/RegisterEndorsement";
        }
        
        public string RegisterDocuments(string host)
        {
            return $"{host}/api/ApplyDistributor/RegisterDocuments";
        }
        
        public string RegisterJobInfo(string host)
        {
            return $"{host}/api/ApplyDistributor/RegisterJobInfo";
        }
        
        public string RegisterSalesXp(string host)
        {
            return $"{host}/api/ApplyDistributor/RegisterSalesXp";
        }
        
        public string RegisterSpouse(string host)
        {
            return $"{host}/api/ApplyDistributor/RegisterSpouse";
        }
        
        public string RegisterSpouseJobInfo(string host)
        {
            return $"{host}/api/ApplyDistributor/RegisterSpouseJobInfo";
        }
        
        public string UpdateTotalIncoming(string host)
        {
            return $"{host}/api/ApplyDistributor/UpdateTotalIncoming";
        }
        
        public string RegisterDependents(string host)
        {
            return $"{host}/api/ApplyDistributor/RegisterDependents";
        }
        
        public string RegisterVehicles(string host)
        {
            return $"{host}/api/ApplyDistributor/RegisterVehicles";
        }
        
        public string RegisterReferences(string host)
        {
            return $"{host}/api/ApplyDistributor/RegisterReferences";
        }
        
        public string ValidateCosts(string host)
        {
            return $"{host}/api/ApplyDistributor/ValidateCosts";
        }

        //---------------------------------------------------------------------------------------------

        public string GetUrl(string methodName, Dictionary<string, object> parameters)
        {
            var type = typeof(UrlsService);
            var method = type.GetMethod(methodName);

            if (method == null)
            {
                throw new ArgumentException("Invalid method name.");
            }

            var neededParameters = method.GetParameters();
            var actualParameters = GetActualParameters(neededParameters, parameters);

            var result = method.Invoke(this, actualParameters);
            return result?.ToString();
        }

        private object[] GetActualParameters(ParameterInfo[] neededParameters, Dictionary<string, object> parameters)
        {
            return neededParameters.Select(p => GetParameterValue(p, parameters)).ToArray();
        }

        private object GetParameterValue(ParameterInfo paramInfo, Dictionary<string, object> parameters)
        {
            if (paramInfo.Name != null && parameters.TryGetValue(paramInfo.Name, out var paramValue))
            {
                return paramValue;
            }
            if (paramInfo.HasDefaultValue)
            {
                return paramInfo.DefaultValue;
            }
            throw new ArgumentException($"Parameter {paramInfo.Name} is required.");
        }
        
        public string GetUrl(string methodName, object[] parameters)
        {
            var type = typeof(UrlsService);
            var method = type.GetMethod(methodName);
    
            if (method != null)
            {
                var result = method.Invoke(this, parameters);
                if (result != null) return result.ToString();
            }
            else
            {
                throw new ArgumentException("Invalid method name.");
            }
    
            return null;
        }
    }
}
