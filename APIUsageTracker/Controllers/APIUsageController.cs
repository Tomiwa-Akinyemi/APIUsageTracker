using APIUsage.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIUsageTracker.Controllers
{
    public class APIUsageController : Controller
    {
        public string GetString(string username)
        {
            return "Hello " + username;
        }

        public IActionResult LogAPICall(string IpAddress)
        {
            APILogResponse response = new APILogResponse();

            #region TokenCheck
            string token = Request.Headers["Access-Token"].ToString();

            if (string.IsNullOrEmpty(token))
            {
                response = new APILogResponse() { IsSuccessful = false, ResponseMessage = "Missing token" };
                return Json(response);
            }
            #endregion TokenCheck

            response = new APIUsage.Service.UsageTracker().LogAPIRequest(token, IpAddress);
            return Json(response);
        }

        public IActionResult CalculateMonthlyCost(int month, int year)
        {
            CalculateCostResponse response = new CalculateCostResponse();
            #region TokenCheck
            string token = Request.Headers["Access-Token"].ToString();

            if (string.IsNullOrEmpty(token))
            {
                response = new CalculateCostResponse() { IsSuccessful = false, ResponseMessage = "Missing Token", MonthlyCharge = 0, TotalNoOfCalls = 0};
                return Json(response);
            }
            #endregion TokenCheck

            response = new APIUsage.Service.UsageTracker().CalculateCost(token, month, year);
            return Json(response);
        }
    }
}
