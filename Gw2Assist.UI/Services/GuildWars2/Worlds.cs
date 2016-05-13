using System.Collections.Generic;

using Gw2Models = Gw2Assist.Core.Models;

namespace Gw2Assist.UI.Services.GuildWars2
{
    public class Worlds
    {
        public Dictionary<int, Gw2Models.World> GetAll()
        {
            var cachedWorlds = Core.Cache.Repository.Instance.GetContainer<Core.Cache.Containers.Gw2Worlds>();
            return cachedWorlds.Contents;
        }
    }
}
