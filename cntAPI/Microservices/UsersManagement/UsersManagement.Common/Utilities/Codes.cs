namespace UsersManagement.Common.Utilities
{
    public static class Codes
    {
        public const string

            OkGet = "OK-00001-BE",
            OkPost = "OK-00002-BE",
            OkPut = "OK-00003-BE",
            OkUserActivate = "OK-00004-BE",
            OkUserUpdated = "OK-00005-BE",
            OkLogin = "OK-00006-BE",
            OkProspectCreated = "Prospect created successfully",

            UserNotFound = "WA-00001-BE",
            WrongPassword = "WA-00002-BE",
            UserNotActive = "WA-00003-BE",
            ExpiredPassword = "WA-00004-BE",
            UserStillLogged = "WA-00005-BE",
            UserNotLogged = "WA-00006-BE",
            HasCharacters = "WA-00007-BE",
            FieldTooLong = "WA-00008-BE",
            CurpAlreadyExists = "WA-00009-BE",
            EmptyField = "ER-00001-BE",
            FailedPassword = "ER-00007-BE",
            FailedEmail = "ER-00008-BE",
            CantGet = "ER-00002-BE",
            FieldNotValid = "ER-00003-BE",
            EntityAlreadyCreated = "ER-00004-BE",
            FailedEnumParse = "ER-00005-BE",
            FailedToken = "ER-00006-BE",
            FailedCurp = "ER-00009-BE",
            FailedInsertErrorLog = "ER-00011-BE Error al Insertar el Log del Error",
            FailedSp = "ER-00001-DB",
            UserAlreadyRelate = "ER-00010-BE",
            TokenExpired = "WA-00012-BE",
            ZipcodeNotValid = "WA-00010-BE",
            ProspectHasNotDv = "WA-00013-BE",
            UserHasNotAddress = "WA-00014-BE",

            SuccessLog = "OK-00007-BE",
            FailedLog = "ER-00011-BE",
            FailedConnection = "Error de conexión con la base de datos";
    }
}
