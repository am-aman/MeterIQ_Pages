using ElectricityMeterIQ1.Models;


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElectricityMeterIQ1.Views
{
    public partial class Electricity : UserControl
    {
        public Electricity()
        {
            InitializeComponent();
            DataContext = this;
        }

        public ObservableCollection<AlertItem> AlertItems { get; } = new()
            {
                new() { Title = "PMU-L2-D-021 · Level 2D", Severity = "Critical",
                        Description = "Current imbalance exceeded 10%", Detail = "Level 2D · 11.4%", TimeAgo = "212 min ago" },
                new() { Title = "MDB-L3-02", Severity = "Warning",
                        Description = "Power factor below threshold", Detail = "Level 3 · PF 0.76", TimeAgo = "8 min ago" },
                new() { Title = "PMU-G-A-014", Severity = "Warning",
                        Description = "Voltage imbalance above limit", Detail = "Ground A · 2.8%", TimeAgo = "14 min ago" },

            };

        public ObservableCollection<PhaseImbalanceRow> PhaseImbalanceRows { get; } = new()
            {
                new() { Meter = "PMU-L2-D-021 · Level 2D", WorstPhase = "C", VImb = 2.4, IImb = 11.4 },
                new() { Meter = "PMU-G-A-014 · Ground A",  WorstPhase = "B", VImb = 2.8, IImb = 8.2  },
                new() { Meter = "PMU-L1-B-044 · Level 1B", WorstPhase = "A", VImb = 2.1, IImb = 74.6  },
                new() { Meter = "PMU-L3-C-018 · Level 3C", WorstPhase = "C", VImb = 1.9, IImb = 64.8  },
                new() { Meter = "PMU-G-B-009 · Ground B",  WorstPhase = "B", VImb = 14.7, IImb = 64.1  },
                new() { Meter = "PMU-L2-A-031 · Level 2A", WorstPhase = "A", VImb = 14.6, IImb = 5.7  },
                new() { Meter = "PMU-L4-D-012 · Level 4D", WorstPhase = "C", VImb = 14.5, IImb = 5.2  },
                new() { Meter = "PMU-L4-D-012 · Level 4D", WorstPhase = "C", VImb = 14.5, IImb = 5.2  },
                new() { Meter = "PMU-L4-D-012 · Level 4D", WorstPhase = "C", VImb = 14.5, IImb = 5.2  },
                new() { Meter = "PMU-L4-D-012 · Level 4D", WorstPhase = "C", VImb = 14.5, IImb = 5.2  },


            };

        public ObservableCollection<PowerFactorRow> PowerFactorRows { get; } = new()
        {
            new() { Mdb = "MDB-L3-02", Pf = 0.82, Demand = "284 kW", Energy = "18.9 MWh", Scope = "Level 3" },
            new() { Mdb = "MDB-GF-04", Pf = 0.87, Demand = "311 kW", Energy = "21.6 MWh", Scope = "Ground" },
            new() { Mdb = "MDB-L1-01", Pf = 0.89, Demand = "226 kW", Energy = "16.4 MWh", Scope = "Level 1" },

        };

        public string PhaseAttentionText =>
    $"{PhaseImbalanceRows.Count(r => r.VImb >= 2.0)} meter(s) require attention";

      public DemandChartModel DemandChart { get; } = new();

        private void PlotHost_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DemandChart.Rebuild(e.NewSize.Width, e.NewSize.Height);
        }
    }
}