namespace Gw2Assist.Anet.Core.Drawing
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

        public static Point3D Convert(float[] value)
        {
            return new Point3D(value[0], value[1], value[2]);
        }
    }
}
