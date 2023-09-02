using System;
using System.Data;
using _2.UsersManagement.Application.DTOs.Users.Accounts.In_Services;
using Microsoft.Data.SqlClient;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Users.Accounts.StoredProcedures
{
    public class RegisterSps
    {
        public static ValidateUserDto CallValidateUsersSp(string user, string curp)
        {
            using var connection = new SqlConnection(ConnectionString.Connection);
            using var command = new SqlCommand("CNT_SP_VALIDATEUSERS", connection);
            command.CommandType = CommandType.StoredProcedure;
            var userParameter = new SqlParameter("@USER", SqlDbType.NVarChar, 50)
            { 
                Direction = ParameterDirection.InputOutput,
                Value = user
            };
            var idUserParameter = new SqlParameter("@IDUSER", SqlDbType.NVarChar, 50)
            {
                Direction = ParameterDirection.InputOutput,
                Value = null
            };
            var idCurpParameter = new SqlParameter("@CURP", SqlDbType.NVarChar, 50)
            {
                Value = curp
            };
            command.Parameters.Add(userParameter);
            command.Parameters.Add(idCurpParameter);
            command.Parameters.Add(idUserParameter);
            connection.Open();
            command.ExecuteScalar();
            connection.Close();
            var idUserOutput = command.Parameters["@IDUSER"].Value == DBNull.Value ? string.Empty : (string)command.Parameters["@IDUSER"].Value;
            var userOutput = command.Parameters["@USER"].Value == DBNull.Value ? string.Empty : (string)command.Parameters["@USER"].Value;
            return new ValidateUserDto
            {
                IdUser = idUserOutput,
                UserName = userOutput,
            };
        }
    }
}