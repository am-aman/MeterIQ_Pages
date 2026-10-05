using System;
using System.Collections.Generic;
using System.Text;

namespace ElectricityMeterIQ1.Models
{
    public class AlertItem
    {
        public string Title { get; set; } = "";
        public string Severity { get; set; } = "";
        public string Description { get; set; } = "";
        public string Detail { get; set; } = "";
        public string TimeAgo { get; set; } = "";
    }
}
