namespace UsersManagement.Common.Utilities
{
    public static class CNames
    {
        public const string
                //---------------GENERIC CONTROLLER----------------------
                GetAll = ".GenericController.GetAllGeneric",
                GetAllById = ".GenericController.GetAllGenericById",
                GetSimpleById = ".GenericController.GetSimpleGenericById",
                GetAllOneField = ".GenericController.GetAllGenericOneField",
                GetSimpleOneField = ".GenericController.GetSimpleGenericOneFieldById",
                Insert = ".GenericController.InsertGeneric",
                Update = ".GenericController.UpdateGeneric",

                //---------------APPLY-DISTRIBUTOR CONTROLLER------------------
                RegisterProspect = ".ApplyDistributorController.ApplyForDistributor",
                AssignAppointment = ".ApplyDistributorController.AssignAppointment",
                RegisterDistributor = ".ApplyDistributorController.RegisterDistributor",
                RegisterEndorsement = ".ApplyDistributorController.RegisterEndorsement",
                RegisterJobInfo = ".ApplyDistributorController.RegisterJobInfo",
                RegisterSalesXp = ".ApplyDistributorController.RegisterSalesXp",
                RegisterSpouse = ".ApplyDistributorController.RegisterSpouse",
                RegisterSpouseJobInfo = ".ApplyDistributorController.RegisterSpouseJobInfo",
                RegisterDependent = ".ApplyDistributorController.RegisterDependent",
                RegisterVehicle = ".ApplyDistributorController.RegisterVehicle",
                RegisterReference = ".ApplyDistributorController.RegisterReference",
                ValidateCosts = ".ApplyDistributorController.ValidateCosts",
                UpdateTotalIncoming = ".ApplyDistributorController.UpdateTotalIncoming",
                
                //---------------DOCUMENTS CONTROLLER----------------------
                RegisterDocuments = "ApplyDistributorController.RegisterDocuments",
                GetDocument = "ApplyDistributorController.GetDocument",
                
                //---------------ZIPCODES CONTROLLER----------------------
                SimpleAddressByZipcode = ".ZipcodesController.GetSimpleAddressByZipcode",

                //---------------LOGIN CONTROLLER------------------------
                Login = ".LoginController.login",
                Logout = ".LoginController.logout",
                GetPermissions = ".LoginController.GetPermissions",
                RefreshToken = ".LoginController.RefreshToken",

                //---------------REGISTER CONTROLLER----------------------
                RegisterUserAndProfile = ".RegisterUserController.RegisterUserAndProfile",
                RegisterRoleAndType = ".RegisterUserController.RegisterUserRoleAndUserType",

                //---------------EDIT CONTROLLER--------------------------
                ChangePassword = ".EditUserController.ChangePassword";
    }
}
