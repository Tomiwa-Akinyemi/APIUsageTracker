using APIUsage.Core;
using APIUsage.Data.Implementation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIUsage.Service
{
    public class UsageTracker : IUsageTracker
    {
        private readonly IOptions<Band> _options;

        public UsageTracker(IOptions<Band> options)
        {
            _options = options;
        }

        public CalculateCostResponse CalculateCost(string token, int month, int year)
        {
            CalculateCostResponse response = new CalculateCostResponse();

            #region InputValidation

            if (string.IsNullOrEmpty(token))
            {
                response = new CalculateCostResponse() { IsSuccessful = false, ResponseMessage = "Missing Token" };
                return response;
            }
            if (month == 0 || month > 12)
            {
                response = new CalculateCostResponse() { IsSuccessful = false, ResponseMessage = "Incorrect month input" };
                return response;
            }
            if (year == 0 || year.ToString().Length > 4 || year.ToString().Length < 4)
            {
                response = new CalculateCostResponse() { IsSuccessful = false, ResponseMessage = "Incorrect year input" };
                return response;
            }

            #endregion InputValidation

            List<Core.APIUsage> apiCallMonth = new List<Core.APIUsage>();
            var totalAPICall = new APIUsageRepository().GetTotalCallsByToken(token);
            apiCallMonth = totalAPICall.Where(c => c.CreatedAt.Year == year && c.CreatedAt.Month == month).ToList();
            double apiCountMonth = apiCallMonth.Count();

            if (apiCountMonth == 0)
            {
                response = new CalculateCostResponse() { IsSuccessful = true, MonthlyCharge = 0, TotalNoOfCalls = 0, ResponseMessage = "No Charge for this month" };

                return response;
            }
            //now we have the count get the band value
            double costPerThreshold = 0;
            int countThreshold = Global.CountThreshold;


            if (apiCountMonth > 0 && apiCountMonth <= _options.Value.Bands[0].Description)
            {
                costPerThreshold = _options.Value.Bands[0].Cost;
            }
            else if (apiCountMonth > _options.Value.Bands[0].Description && apiCountMonth <= _options.Value.Bands[1].Description)
            {
                costPerThreshold = _options.Value.Bands[1].Cost;
            }
            else
            {
                costPerThreshold = _options.Value.Bands[2].Cost;
            }
            //using the band properties calculate the cost
            double countPerThreshold = Convert.ToDouble(Math.Ceiling(apiCountMonth / countThreshold));
            double totalCharge = countPerThreshold * costPerThreshold;

            response = new CalculateCostResponse() { IsSuccessful = true, MonthlyCharge = totalCharge, TotalNoOfCalls = apiCountMonth, ResponseMessage = "Total Charge Gotten" };

            return response;
        }

        public APILogResponse LogAPIRequest(string token, string ipAddress)
        {
            
            APILogResponse response = new APILogResponse();

            #region InputValidation
            if (string.IsNullOrEmpty(token))
            {
                response = new APILogResponse() { IsSuccessful = false, ResponseMessage = "Missing Token" };
                return response;
            }
            if (string.IsNullOrEmpty(ipAddress))
            {
                response = new APILogResponse() { IsSuccessful = false, ResponseMessage = "Missing IPAddress" };
                return response;
            }
            if ((!string.IsNullOrEmpty(ipAddress)) && !ValidateIPAddress(ipAddress))
            {
                response = new APILogResponse() { IsSuccessful = false, ResponseMessage = "Incorrect IPAddress" };
                return response;
            }
            #endregion InputValidation
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
                    response.ResponseMessage = "Access denied";
                }
            }
            catch (Exception ex)
            {
                //log exception message
                response = new APILogResponse() { IsSuccessful = false, ResponseMessage = "System Error"};
            }

            return response;
        }

        public bool ValidateIPAddress(string ipAddress)
        {
            if (String.IsNullOrWhiteSpace(ipAddress))
            {
                return false;
            }

            string[] splitValues = ipAddress.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempValue;

            return splitValues.All(r => byte.TryParse(r, out tempValue));
        }

    }
}
