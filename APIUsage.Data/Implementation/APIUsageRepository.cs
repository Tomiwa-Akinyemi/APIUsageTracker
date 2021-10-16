using APIUsage.Core;
using APIUsage.Data.Contract;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace APIUsage.Data.Implementation
{
    public class APIUsageRepository : IAPIUsageRepository
    {
        public IEnumerable<Core.APIUsage> GetTotalCallsByToken(string token)
        {
            // fetch list for the last 90 days
            //Trace.TraceInformation($"About to get institution by etz code.... {code}");
            DateTime startDate = DateTime.Now.AddDays(-90);

            IEnumerable<Core.APIUsage> requestList = new List<Core.APIUsage>();
            try
            {
                using (SqlConnection con = new SqlConnection(Global.ConnectionString))
                {
                    string query = @"SELECT * FROM APIUsageLogs WHERE CreatedAt >= @Startdate and Token=@token";
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    requestList = con.Query<Core.APIUsage>(query, new { token, startDate });
                }
            }
            catch (SqlException sEx)
            {
                throw sEx;
            }
            return requestList;
        }

        public int SaveCall(Core.APIUsage apiCall)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Global.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    var a = con.Query<int>("LogAPICall", this.SetInsertParameters(apiCall), commandType: CommandType.StoredProcedure);
                    return 1;
                }
            }
            catch (SqlException sEx)
            {
                return 0;
            }
        }

        private DynamicParameters SetInsertParameters(Core.APIUsage apiCall)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CreatedAt", apiCall.CreatedAt);
            parameters.Add("@IPAddress", apiCall.IPAddress);
            parameters.Add("@Token", apiCall.Token);
            return parameters;
        }
    }
}
