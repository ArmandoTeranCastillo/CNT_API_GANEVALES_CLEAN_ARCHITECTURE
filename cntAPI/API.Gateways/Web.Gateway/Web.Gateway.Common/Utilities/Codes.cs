using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Gateway.Common.Utilities
{
    public abstract class Codes
    {
        public const string

            UserNotFound = "WA-00001-BE",
            WrongPassword = "WA-00002-BE",
            UserNotActive = "WA-00003-BE",
            ExpiredPassword = "WA-00004-BE",
            UserStillLogged = "WA-00005-BE",
            UserNotLogged = "WA-00006-BE",
            HasCharacters = "WA-00007-BE",
            FieldTooLong = "WA-00008-BE",
            EmptyField = "ER-00001-BE",
            CantGet = "ER-00002-BE",
            FieldNotValid = "ER-00003-BE",
            EntityAlreadyCreated = "ER-00004-BE",
            TokenExpired = "WA-00012-BE",
            Undefined = "",
            FailedConnection = "Error de conexión con la base de datos";
    }
}
