using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Gateway.Common.Utilities
{
    public abstract class NUrls
    {
        public const string
            //-----GENERIC CONTROLLER---------------------
            GetAllOneField = "GetAllOneField",
            GetSimpleOneField = "GetSimpleOneField",
            GetAll = "GetAll",
            GetAllById = "GetAllById",
            GetSimpleById = "GetSimpleById",
            GetEnum = "GetEnum",
            Insert = "Insert",
            Update = "Update",

            //-----ZIPCODES CONTROLLER-------------------------
            GetSimpleAddressByZip = "GetSimpleAddressByZipcode",

            //-----LOGIN CONTROLLER----------------------------
            Login = "Login",
            Logout = "Logout",
            RefreshToken = "RefreshToken",

            //-----REGISTER USER CONTROLLER-------------------------
            RegisterUserAndProfile = "RegisterUserAndProfile",
            RegisterUserRoleAndUserType = "RegisterUserRoleAndUserType",

            //-----EDIT USER CONTROLLER-----------------------------
            ChangePassword = "ChangePassword",

            //-----DOCUMENTS CONTROLLER------------------
            RegisterDocuments = "RegisterDocumentsUser",
            GetDocument = "GetDocument",
            
            //-----APPLY DISTRIBUTOR CONTROLLER----------------------
            RegisterProspect = "RegisterProspect",
            AssignAppointment = "AssignAppointment",
            RegisterDistributor = "RegisterDistributor",
            RegisterEndorsement = "RegisterEndorsement",
            RegisterJobInfo = "RegisterJobInfo",
            RegisterSalesXp = "RegisterSalesXp",
            RegisterSpouse = "RegisterSpouse",
            RegisterSpouseJobInfo = "RegisterSpouseJobInfo",
            UpdateTotalIncoming = "UpdateTotalIncoming",
            RegisterDependents = "RegisterDependents",
            RegisterVehicles = "RegisterVehicles",
            RegisterReferences = "RegisterReferences",
            ValidateCosts = "ValidateCosts";
    }
}
