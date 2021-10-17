using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsage.Core
{
    public class Band
    {
        public List<BandProperties> Bands { get; set; }
    }

    public class BandProperties
    {
        public double Description { get; set; }
        public double Cost { get; set; }
    }
}
