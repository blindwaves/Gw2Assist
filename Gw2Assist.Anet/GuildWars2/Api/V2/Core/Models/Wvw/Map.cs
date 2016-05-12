using System.Collections.Generic;

namespace Gw2Assist.Anet.GuildWars2.Api.V2.Core.Models.Wvw
{
    public class Map
    {
        /// <summary>
        /// Gets or sets the list of bonuses granted by the map.
        /// </summary>
        public List<Bonus> Bonuses { get; set; }

        /// <summary>
        /// Gets or sets the death on the map.
        /// </summary>
        public Borderland Deaths { get; set; }

        /// <summary>
        /// Gets or sets the map ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the kills on the map.
        /// </summary>
        public Borderland Kills { get; set; }

        /// <summary>
        /// Gets or sets the list of objectives on the map.
        /// </summary>
        public List<Objective> Objectives { get; set; }

        /// <summary>
        /// Gets or sets the scores on the map.
        /// </summary>
        public Borderland Scores { get; set; }

        /// <summary>
        /// Gets or sets the type of map (BlueHome, GreenHome, RedHome).
        /// </summary>
        public Enums.MapType Type { get; set; }
    }
}
