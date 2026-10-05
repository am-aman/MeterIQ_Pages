using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace ElectricityMeterIQ1.Models
{
    public class PhaseImbalanceRow
    {
        private static readonly Brush Red = new SolidColorBrush(Color.FromRgb(245, 10, 13));
        private static readonly Brush Orange = new SolidColorBrush(Color.FromRgb(0xFF, 0xA9, 0x40));

        public string Meter { get; set; } = "";
        public string WorstPhase { get; set; } = "";
        public double VImb { get; set; }
        public double IImb { get; set; }

        public Brush VImbBrush => VImb >= 2.0 ? Red : Orange;
        public Brush IImbBrush => IImb >= 8.0 ? Red : Orange;
    }
}
