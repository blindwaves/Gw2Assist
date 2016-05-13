using System;

using Newtonsoft.Json;

using Gw2Assist.Anet.Converters;
using Gw2Assist.Anet.Drawing;
using Gw2Assist.Anet.GuildWars2.Api.V2.Enums;

namespace Gw2Assist.Anet.GuildWars2.Api.V2.Models.Wvw
{
    public class Objective
    {
        /// <summary>
        /// Gets or sets the coordinates (X, Y, Z) of the objective marker on the map.
        /// </summary>
        [JsonConverter(typeof(JsonStringPoint3DConverter))]
        public Point3D Coord { get; set; }

        /// <summary>
        /// Gets or sets the guild ID that claimed the objective. NULL if not claimed
        /// </summary>
        [JsonProperty("claim_by")]
        public string ClaimedBy { get; set; }

        /// <summary>
        /// Gets or sets the DateTime that was claimed by the guild. NULL if not claimed.
        /// </summary>
        [JsonProperty("claim_at")]
        public DateTime? ClaimedAt { get; set; }

        /// <summary>
        /// Gets or sets the objective ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the coordinates (X, Y) of the sector centroid.
        /// </summary>
        [JsonProperty("label_coord")]
        [JsonConverter(typeof(JsonStringPoint2DConverter))]
        public Point2D LabelCoord { get; set; }

        /// <summary>
        /// Gets or sets the DateTime the objective was flipped.
        /// </summary>
        [JsonProperty("last_flipped")]
        public DateTime LastFlipped { get; set; }

        /// <summary>
        /// Gets or sets the map ID this objective belongs to.
        /// </summary>
        [JsonProperty("map_id")]
        public int MapId { get; set; }

        /// <summary>
        /// Gets or sets the map type that it belongs to (Blue, Green, Red).
        /// </summary>
        [JsonProperty("map_type")]
        public MapType MapType { get; set; }

        /// <summary>
        /// Gets or sets the name of the objective.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the owner that the objective belongs to.
        /// </summary>
        public BorderlandColor Owner { get; set; }

        /// <summary>
        /// Gets or sets the sector ID (see /v2/continents) that the objective belongs to.
        /// </summary>
        [JsonProperty("sector_id")]
        public int SectorId { get; set; }

        /// <summary>
        /// Gets or sets the type of objective (Castle, Keep, etc).
        /// </summary>
        public ObjectiveType Type { get; set; }
    }
}
