using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsage.Core
{
    public class CalculateCostResponse
    {
        public virtual double MonthlyCharge { get; set; }
        public virtual double TotalNoOfCalls { get; set; }
        public virtual string ResponseMessage { get; set; }
        public virtual bool IsSuccessful { get; set; }
    }
}
