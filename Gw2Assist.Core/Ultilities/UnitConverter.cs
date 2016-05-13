using Gw2Assist.Anet.Drawing;

namespace Gw2Assist.Core.Ultilities
{
    /// <summary>
    /// Mainly use to convert coordinates for the API, for a very good discussion:
    /// https://forum-en.guildwars2.com/forum/community/api/Event-Details-API-location-coordinates/
    /// </summary>
    public static class UnitConverter
    {
        public static readonly double MeterToInchesRatio = 39.3700787;
        public static readonly double InchToPixelsRatio = 0.0416667;

        public static double MetersToInches(double value)
        {
            return value * MeterToInchesRatio;
        }

        public static double InchesToPixels(double value)
        {
            return value * InchToPixelsRatio;
        }
    }
}
