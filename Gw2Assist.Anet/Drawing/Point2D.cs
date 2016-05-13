using System;

namespace Gw2Assist.Anet.Drawing
{
    public class Point2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point2D(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double DistanceFrom(Point2D point)
        {
            return Math.Sqrt(((this.X - point.X) * (this.X - point.X)) + ((this.Y - point.Y) * (this.Y - point.Y)));
        }
    }
}
