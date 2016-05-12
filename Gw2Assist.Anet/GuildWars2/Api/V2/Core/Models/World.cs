using Gw2Assist.Anet.GuildWars2.Api.V2.Core.Enums;

namespace Gw2Assist.Anet.GuildWars2.Api.V2.Core.Models
{
    public class World
    {
        /// <summary>
        /// Gets or sets the world ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the world.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the population level.
        /// </summary>
        public PopulationLevel Population { get; set; }
    }
}
