using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


using System.Windows.Media;

using System.Windows;

namespace ElectricityMeterIQ1.Models
{
    public class DemandChartModel : INotifyPropertyChanged
    {
        // Y axis range (matches the labels 0.8 .. 2.6, with some headroom)
        private const double YMin = 0.8;
        private const double YMax = 2.8;

        public double[] Today { get; } =
            { 2.20, 2.30, 2.40, 2.48, 2.55, 2.55, 2.50, 2.55, 2.70, 2.75, 0.85, 2.00, 2.15, 2.15, 2.15, 2.15, 2.15 };

        public double[] Yesterday { get; } =
            { 1.45, 1.45, 1.40, 1.38, 1.40, 1.50, 1.70, 1.80, 1.85, 2.00, 2.20, 2.25, 2.25, 2.25, 2.25, 2.25, 2.25 };

        private Geometry? _todayLine, _todayArea, _yesterdayLine;
        public Geometry? TodayLine { get => _todayLine; private set { _todayLine = value; OnChanged(nameof(TodayLine)); } }
        public Geometry? TodayArea { get => _todayArea; private set { _todayArea = value; OnChanged(nameof(TodayArea)); } }
        public Geometry? YesterdayLine { get => _yesterdayLine; private set { _yesterdayLine = value; OnChanged(nameof(YesterdayLine)); } }

        public void Rebuild(double width, double height)
        {
            if (width <= 0 || height <= 0) return;
            TodayLine = Build(Today, width, height, closeArea: false);
            TodayArea = Build(Today, width, height, closeArea: true);
            YesterdayLine = Build(Yesterday, width, height, closeArea: false);
        }

        // Smooth curve through every point (Catmull-Rom converted to Bezier segments)
        private static Geometry Build(double[] values, double w, double h, bool closeArea)
        {
            int n = values.Length;
            var pts = new Point[n];
            for (int i = 0; i < n; i++)
            {
                double x = w * i / (n - 1);
                double y = h * (1 - (values[i] - YMin) / (YMax - YMin));
                pts[i] = new Point(x, y);
            }

            var figure = new PathFigure { StartPoint = pts[0], IsClosed = closeArea, IsFilled = closeArea };
            for (int i = 0; i < n - 1; i++)
            {
                Point p0 = pts[Math.Max(i - 1, 0)];
                Point p1 = pts[i];
                Point p2 = pts[i + 1];
                Point p3 = pts[Math.Min(i + 2, n - 1)];

                var c1 = new Point(p1.X + (p2.X - p0.X) / 6, p1.Y + (p2.Y - p0.Y) / 6);
                var c2 = new Point(p2.X - (p3.X - p1.X) / 6, p2.Y - (p3.Y - p1.Y) / 6);
                figure.Segments.Add(new BezierSegment(c1, c2, p2, true));
            }

            if (closeArea)
            {
                figure.Segments.Add(new LineSegment(new Point(w, h), false));
                figure.Segments.Add(new LineSegment(new Point(0, h), false));
            }

            var geometry = new PathGeometry();
            geometry.Figures.Add(figure);
            geometry.Freeze();
            return geometry;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
