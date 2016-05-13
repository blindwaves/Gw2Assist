namespace Gw2Assist.Anet.Drawing
{
    public class Point3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public double HorizontalDistanceFrom(Point3D point)
        {
            return (new Point2D(this.X, this.Y).DistanceFrom(new Point2D(point.X, point.Y)));
        }
    }
}
