namespace Gw2Assist.Anet.Core.Drawing
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

        public static Point2D Convert(float[] value)
        {
            return new Point2D(value[0], value[1]);
        }
    }
}
