using APIUsage.Core;
using APIUsage.Data.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIUsage.Service
{
    public class UsageTracker
    {
        public CalculateCostResponse CalculateCost(string token, int month, int year)
        {
            CalculateCostResponse response = new CalculateCostResponse();

            List<Core.APIUsage> requestList = new List<Core.APIUsage>();

            var totalAPICall = new APIUsageRepository().GetTotalCallsByToken(token);

            requestList = totalAPICall.Where(c => c.CreatedAt.Year == year && c.CreatedAt.Month == month).ToList();

            double totalAPICount = requestList.Count();

            return response;
        }

        public APILogResponse LogAPIRequest(string token, string ipAddress)
        {
            APILogResponse response = new APILogResponse();

            try
            {
                if (Global.TokenList.Contains(token))
                {
                    //if token exist then call method to save IP,datetime and token
                    Core.APIUsage apiCall = new Core.APIUsage()
                    {
                        Token = token,
                        IPAddress = ipAddress,
                        CreatedAt = DateTime.Now
                    };
                    int row = new APIUsageRepository().SaveCall(apiCall);
                    if (row == 1)
                    {
                        response.IsSuccessful = true;
                        response.ResponseMessage = "Successful";
                    }
                }
                else
                {
                    response.IsSuccessful = false;
                    response.ResponseMessage = "Token not configured";
                }
            }
            catch (Exception ex)
            {
                //log exception message
                response = new APILogResponse() { IsSuccessful = false, ResponseMessage = "System Error"};
            }

            return response;
        }
    }
}
