using System;
using System.Collections.Generic;
using System.Data;
using _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Services;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using UsersManagement.Common.Utilities;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Distributors.Application.StoredProcedures
{
    public class ApplyDvSps
    {
        public static string CallSearchCurpSp(string curp)
        {
            using var connection = new SqlConnection(ConnectionString.Connection);
            using var command = new SqlCommand("CNT_SP_SEARCHCURPPORSPECT", connection);
            command.CommandType = CommandType.StoredProcedure;
            var curpParameter = new SqlParameter("@CURP", SqlDbType.NVarChar, 50)
            {
                Value = curp
            };
            var inoutParameter = new SqlParameter("@CodeRetunr", SqlDbType.NVarChar, 50)
            {
                Value = DBNull.Value,
                Direction = ParameterDirection.InputOutput
            };
            command.Parameters.Add(curpParameter);
            command.Parameters.Add(inoutParameter);
            connection.Open();
            command.ExecuteScalar();
            var value = command.Parameters["@CodeRetunr"].Value;
            if (value != null)
            {
                var inoutParamResult = value.ToString();
                connection.Close();
                return inoutParamResult;
            }
            connection.Close();
            return null;
        }
        
        public static string CallGetUserToProspectSp()
        {
            using var connection = new SqlConnection(ConnectionString.Connection);
            using var command = new SqlCommand("CNT_SP_GETUSERTOPROSPECT", connection);
            command.CommandType = CommandType.StoredProcedure;
            var userParameter = new SqlParameter("@idUserOut", SqlDbType.NVarChar, 50)
            {
                Direction = ParameterDirection.InputOutput,
                Value = null
            };
            command.Parameters.Add(userParameter);
            connection.Open();
            command.ExecuteScalar();
            connection.Close();
            var idUserOutput = command.Parameters["@idUserOut"].Value == DBNull.Value ? string.Empty : (string)command.Parameters["@idUserOut"].Value;
            return idUserOutput;
        }
        
        public static object CallGetReportProspectsSp(string idUser)
        {
            using var connection = new SqlConnection(ConnectionString.Connection);
            using var command = new SqlCommand("CNT_SP_GETREPORTPROSPECTS", connection);
            command.CommandType = CommandType.StoredProcedure;
            var userParameter = new SqlParameter("@idUser", SqlDbType.NVarChar, 50)
            {
                Value = idUser
            };
            command.Parameters.Add(userParameter);
            connection.Open();
            var prospect = command.ExecuteScalar()?.ToString();
            connection.Close();
            return JsonConvert.DeserializeObject<List<GetAssignedProspectsDto>>(prospect);
        }
        
        public static object CallTransTypesSp<T>(int type, string idUser)
        {
            using var connection = new SqlConnection(ConnectionString.Connection);
            using var command = new SqlCommand("CNT_SP_REPORTSPROSPECTS", connection);
            command.CommandType = CommandType.StoredProcedure;
            var transParameter = new SqlParameter("@trasnType", SqlDbType.Int)
            {
                Value = type
            };
            var userParameter = new SqlParameter("@idUser", SqlDbType.NVarChar, 50)
            {
                Value = idUser
            };
            command.Parameters.Add(transParameter);
            command.Parameters.Add(userParameter);
            connection.Open();
            var appointments = (string)command.ExecuteScalar();
            connection.Close();
            return JsonConvert.DeserializeObject<T>(appointments);
        }
    }
}