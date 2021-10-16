using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsage.Core
{
    public class CalculateCostResponse
    {
        public virtual decimal MonthlyCharge { get; set; }
        public virtual int TotalNoOfCalls { get; set; }
        public virtual string ResponseMessage { get; set; }
        public virtual bool IsSuccessful { get; set; }
    }
}
