using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;

namespace SalonAppointmentSystem.DAL
{
    public class DapperORM
    {
        public static readonly string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static void ExecuteWithoutReturn(string ProcedureName,DynamicParameters param = null)
        {
            using(SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                con.Execute(ProcedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
        public static T ReturnSingle<T>(string ProcedureName, DynamicParameters param = null)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                var result = con.QueryFirstOrDefault<T>(ProcedureName, param, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public static IEnumerable<T> ReturnList<T>(string ProcedureName, DynamicParameters param = null)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                var list = con.Query<T>(ProcedureName, param, commandType: CommandType.StoredProcedure);
                return list;
            }
        }
    }
}