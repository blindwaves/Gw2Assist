using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Gw2Assist.Anet.Converters
{
    public class JsonStringEnumConverter<T> : StringEnumConverter
    {
        public T DefaultValue { get; set; }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch (JsonSerializationException)
            {
                return DefaultValue;
            }
        }
    }
}
