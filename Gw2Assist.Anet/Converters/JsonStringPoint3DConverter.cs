using System;
using Newtonsoft.Json;

namespace Gw2Assist.Anet.Converters
{
    public class JsonStringPoint3DConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Core.Drawing.Point3D);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var doubleArray = (double[])serializer.Deserialize(reader, typeof(double[]));

            if (doubleArray == null)
            {
                return null;
            }

            return new Core.Drawing.Point3D(doubleArray[0], doubleArray[1], doubleArray[2]);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var point = (Core.Drawing.Point3D)value;
            var doubleArray = new double[] { point.X, point.Y, point.Z };
            serializer.Serialize(writer, doubleArray);
        }
    }
}
