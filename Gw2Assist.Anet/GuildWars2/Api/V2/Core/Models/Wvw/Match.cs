using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using Gw2Assist.Anet.GuildWars2.Api.V2.Core.Enums;

namespace Gw2Assist.Anet.GuildWars2.Api.V2.Core.Models.Wvw
{
    public class Match
    {
        /// <summary>
        /// Gets or sets the deaths for the match.
        /// </summary>
        public Borderland Deaths { get; set; }

        /// <summary>
        /// Gets or sets the end DateTime of the match.
        /// </summary>
        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the ID of the match.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the kills for the match.
        /// </summary>
        public Borderland Kills { get; set; }

        /// <summary>
        /// Gets or sets the list of maps this match contains.
        /// </summary>
        public List<Map> Maps { get; set; }

        /// <summary>
        /// Gets or sets the scores for the match.
        /// </summary>
        public Borderland Scores { get; set; }

        /// <summary>
        /// Gets or sets the start DateTime of the match.
        /// </summary>
        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the list of world IDs the match contains.
        /// </summary>
        public Borderland Worlds { get; set; }
    }
}
