using APIUsage.Core;
using APIUsage.Data.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIUsage.Service
{
    public class UsageTracker
    {
        public decimal CalculateCost(string token, int month, int year)
        {
            List<Core.APIUsage> requestList = new List<Core.APIUsage>();

            var totalAPICall = new APIUsageRepository().GetTotalCallsByToken(token);

            requestList = totalAPICall.Where(c => c.CreatedAt.Year == year && c.CreatedAt.Month == month).ToList();

            double totalAPICount = requestList.Count();

            return 0;
        }

        public bool LogAPIRequest(string token, out string responseMessage)
        {
            string message = string.Empty;
            bool isSuccessful = false;

            try
            {
                if (Global.TokenList.Contains(token))
                {
                    //if token exist then call method to save IP,datetime and token
                    Core.APIUsage apiCall = new Core.APIUsage()
                    {
                        Token = token,
                        IPAddress = "",
                        CreatedAt = DateTime.Now
                    };
                    int row = new APIUsageRepository().SaveCall(apiCall);
                    if (row == 1)
                    {
                        message = "Successful";
                        isSuccessful = true;
                    }
                }
                else
                {
                    message = "Token not configured";
                }
            }
            catch (Exception ex)
            {
                message = "System error";
                throw;
            }

            responseMessage = message;

            return isSuccessful;
        }
    }
}
