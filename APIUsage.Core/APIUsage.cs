using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsage.Core
{
    public class APIUsage
    {
        public virtual long Id { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string IPAddress { get; set; }
        public virtual string Token { get; set; }
    }
}
