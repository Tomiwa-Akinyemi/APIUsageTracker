using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsage.Core
{
    public class APILogResponse
    {
        public virtual bool IsSuccessful { get; set; }
        public virtual string ResponseMessage { get; set; }
    }
}
