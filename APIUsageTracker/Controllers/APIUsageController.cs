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

        public IActionResult LogAPICall()
        {
            APILogResponse response = new APILogResponse();

            #region TokenCheck
            string token = Request.Headers["AuthorizationToken"].ToString();

            if (string.IsNullOrEmpty(token))
            {
                response = new APILogResponse() { IsSuccessful = false, ResponseMessage = "Missing token" };
                return Json(response);
            }
            #endregion TokenCheck

            //string IpAddress = Request.
            return Json(response);
        }
    }
}
