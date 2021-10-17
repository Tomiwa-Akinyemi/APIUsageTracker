using APIUsage.Core;
using APIUsage.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIUsageTracker.Controllers
{
    public class APIUsageController : Controller
    {
        private readonly IUsageTracker _usageTracker;

        public APIUsageController(IUsageTracker usageTracker)
        {
            _usageTracker = usageTracker;
        }
        [Route("APIUsage/Welcome/{username}")]
        public string GetString(string username)
        {
            return "Hello " + username;
        }

        [Route("APIUsage/LogRequest/{IpAddress}")]
        public IActionResult LogAPICall(string IpAddress)
        {
            APILogResponse response = new APILogResponse();

            #region TokenCheck
            string token = Request.Headers["Access-Token"].ToString();

            if (string.IsNullOrEmpty(token))
            {
                response = new APILogResponse() { IsSuccessful = false, ResponseMessage = "Missing token header" };
                return Json(response);
            }
            #endregion TokenCheck

            response = _usageTracker.LogAPIRequest(token, IpAddress);
            return Json(response);
        }

        [Route("APIUsage/CalculateCharge/{month}/{year}")]
        public IActionResult CalculateMonthlyCost(int month, int year)
        {
            CalculateCostResponse response = new CalculateCostResponse();
            #region TokenCheck
            string token = Request.Headers["Access-Token"].ToString();

            if (string.IsNullOrEmpty(token))
            {
                response = new CalculateCostResponse() { IsSuccessful = false, ResponseMessage = "Missing token header", MonthlyCharge = 0, TotalNoOfCalls = 0};
                return Json(response);
            }
            #endregion TokenCheck

            response = _usageTracker.CalculateCost(token, month, year);
            return Json(response);
        }
    }
}
