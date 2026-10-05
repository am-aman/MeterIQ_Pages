using System;
using System.Collections.Generic;
using System.Text;

namespace ElectricityMeterIQ1.Models
{
    public class PowerFactorRow
    {
        public string Mdb { get; set; } = "";
        public double Pf { get; set; }
        public string Demand { get; set; } = "";
        public string Energy { get; set; } = "";
        public string Scope { get; set; } = "";
    }
}
