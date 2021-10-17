using APIUsage.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsage.Service
{
    public interface IUsageTracker
    {
        CalculateCostResponse CalculateCost(string token, int month, int year);
        APILogResponse LogAPIRequest(string token, string ipAddress);
        bool ValidateIPAddress(string ipAddress);
    }
}
