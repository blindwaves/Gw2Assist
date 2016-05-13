using Gw2Models = Gw2Assist.Anet.GuildWars2.Api.V2.Models;

namespace Gw2Assist.Core.Models.GuildWars2
{
    public class World : Gw2Models.World, System.IComparable<World>
    {
        public int CompareTo(World world)
        {
            return this.Name.CompareTo(world.Name);
        }
    }
}
