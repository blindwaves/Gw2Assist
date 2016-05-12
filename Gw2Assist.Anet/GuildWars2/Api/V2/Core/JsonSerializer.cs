using Newtonsoft.Json;

using Gw2Assist.Anet.Converters;

namespace Gw2Assist.Anet.GuildWars2.Api.V2.Core
{
    public class JsonSerializer
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings();

        public JsonSerializer()
        {
            JsonSerializerSettings.Converters.Add(new JsonStringEnumConverter<Enums.BonusType>() { DefaultValue = Enums.BonusType.Unknown });
            JsonSerializerSettings.Converters.Add(new JsonStringEnumConverter<Enums.BorderlandColor>() { DefaultValue = Enums.BorderlandColor.Unknown });
            JsonSerializerSettings.Converters.Add(new JsonStringEnumConverter<Enums.MapType>() { DefaultValue = Enums.MapType.Unknown });
            JsonSerializerSettings.Converters.Add(new JsonStringEnumConverter<Enums.ObjectiveType>() { DefaultValue = Enums.ObjectiveType.Unknown });
            JsonSerializerSettings.Converters.Add(new JsonStringEnumConverter<Enums.PopulationLevel>() { DefaultValue = Enums.PopulationLevel.Unknown });
        }

        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, JsonSerializerSettings);
        }
    }
}
