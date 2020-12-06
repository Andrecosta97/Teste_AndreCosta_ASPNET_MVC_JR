using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Teste_AndreCosta_ASPNET_MVC_JR.Models
{
    static class DapperORM
    {

        private static string conn = "Password=123456A@;Persist Security Info=True;User ID=adminteste;Initial Catalog=TesteAndreCosta;Data Source=testeandrecosta.database.windows.net";

        public static bool ExcecuteQuery(string processName, DynamicParameters param)
        {
            try
            {
                SqlConnection sqlConn;
                using (sqlConn = new SqlConnection(conn))
                {
                    sqlConn.Open();
                    sqlConn.Execute(processName, param, commandType: CommandType.StoredProcedure);
                }
                return false;
            }
            catch (Exception)
            {

                return true;
            }
        }

        public static T ExecuteReturnScalar<T>(string processName, DynamicParameters param)
        {
            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                sqlConn.Open();
                return (T)Convert.ChangeType(sqlConn.Execute(processName, param, commandType: CommandType.StoredProcedure), typeof(T));
            }

        }

        public static IEnumerable<T> ReturnList<T>(string processName, DynamicParameters param = null)
        {
            SqlConnection sqlConn;

            using (sqlConn = new SqlConnection(conn))
            {
                sqlConn.Open();
                return sqlConn.Query<T>(processName, param, commandType: CommandType.StoredProcedure);
            }

        }

    }
}
