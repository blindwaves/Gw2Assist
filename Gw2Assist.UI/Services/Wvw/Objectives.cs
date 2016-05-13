using System.Collections.Generic;

using Gw2Models = Gw2Assist.Core.Models;

namespace Gw2Assist.UI.Services.Wvw
{
    public class Objectives
    {
        public List<Gw2Models.Wvw.Objective> GetAllByMapId(int mapId, Anet.Drawing.Point3D avatarPosition)
        {
            var cachedWvWObjectives = Core.Cache.Repository.Instance.GetContainer<Core.Cache.Containers.Gw2WvwObjectives>();
            var wvwObjectives = new List<Gw2Models.Wvw.Objective>();

            if (cachedWvWObjectives.Contents.ContainsKey(mapId))
            {
                foreach(var obj in cachedWvWObjectives.Contents[mapId])
                {
                    // If coordinates is null, means it is removed from the map.
                    if (obj.Coord == null) continue;

                    obj.DistanceFromAvatar = 0;
                    wvwObjectives.Add(obj);
                }

                return wvwObjectives;
            }

            return new List<Gw2Models.Wvw.Objective>();
        }
    }
}
