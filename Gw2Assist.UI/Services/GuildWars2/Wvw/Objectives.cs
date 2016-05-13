using System.Collections.Generic;
using System.Linq;

using Gw2Models = Gw2Assist.Core.Models;

namespace Gw2Assist.UI.Services.GuildWars2.Wvw
{
    public class Objectives
    {
        private static Gw2Models.Wvw.Match CachedWvwMatch;
        private static Dictionary<int, List<Gw2Models.Wvw.Objective>> CachedWvwObjectives;

        public Objectives()
        {
            CachedWvwObjectives = Core.Cache.Repository.Instance.GetContainer<Core.Cache.Containers.Gw2WvwObjectives>().Contents;
        }

        public List<Gw2Models.Wvw.Objective> GetAllByAvatarLocation(int worldId, int mapId, Anet.Drawing.Point3D avatarPosition)
        {
            CachedWvwMatch = Core.Cache.Repository.Instance.GetContainer<Core.Cache.Containers.Gw2WvwMatch>().Contents;

            var wvwObjectives = new List<Gw2Models.Wvw.Objective>();

            if (CachedWvwObjectives.ContainsKey(mapId))
            {
                foreach(var obj in CachedWvwObjectives[mapId])
                {
                    // If coordinates is null, means it is removed from the map.
                    if (obj.Coord == null) continue;

                    if (CachedWvwMatch != null)
                    {
                        var matchMap = CachedWvwMatch.Maps.Find(m => m.Id == mapId);
                        var matchObjective = matchMap?.Objectives?.Find(o => o.Id == obj.Id);

                        if (matchObjective != null)
                        {
                            obj.ClaimedAt = matchObjective.ClaimedAt;
                            obj.ClaimedBy = matchObjective.ClaimedBy;
                            obj.LastFlipped = matchObjective.LastFlipped;
                            obj.Owner = matchObjective.Owner;
                        }
                    }

                    // TODO: Calculate distance.
                    obj.DistanceFromAvatar = 0;

                    wvwObjectives.Add(obj);
                }

                return wvwObjectives;
            }

            return new List<Gw2Models.Wvw.Objective>();
        }
    }
}
