using System.Collections.Generic;
using System.Linq;

using Gw2Models = Gw2Assist.Core.Models;

namespace Gw2Assist.UI.Services.GuildWars2.Wvw
{
    public class Objectives
    {
        private readonly Settings settingsService;

        private static Gw2Models.GuildWars2.Wvw.Match CachedWvwMatch;
        private static Dictionary<int, List<Gw2Models.GuildWars2.Wvw.Objective>> CachedWvwObjectives;

        public Objectives(Settings settingsService)
        {
            this.settingsService = settingsService;

            CachedWvwObjectives = Core.Cache.Repository.Instance.GetContainer<Core.Cache.Containers.Gw2WvwObjectives>().Contents;
        }

        public List<Gw2Models.GuildWars2.Wvw.Objective> GetAllByAvatarLocation(int worldId, int mapId, Anet.Drawing.Point3D avatarPosition)
        {
            var cachedWvwMatch = Core.Cache.Repository.Instance.GetContainer<Core.Cache.Containers.Gw2WvwMatch>();
            if (cachedWvwMatch.Contents == null) cachedWvwMatch.Refresh(Core.Cache.Repository.Instance.StoragePath, this.settingsService.Get<int>(Settings.Name.Gw2ResidentWorld));
            CachedWvwMatch = cachedWvwMatch.Contents;

            var wvwObjectives = new List<Gw2Models.GuildWars2.Wvw.Objective>();

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
                    obj.DistanceFromAvatar = obj.Coord.HorizontalDistanceFrom(new Anet.Drawing.Point3D(
                        // avatarPosition is taken from Mumble, refer to mumble context as it explains it uses metres and left handed coordinates.
                        Core.Ultilities.UnitConverter.MetersToInches(avatarPosition.X),
                        Core.Ultilities.UnitConverter.MetersToInches(avatarPosition.Z),
                        Core.Ultilities.UnitConverter.MetersToInches(avatarPosition.Y)
                        ));

                    wvwObjectives.Add(obj);
                }

                return wvwObjectives;
            }

            return new List<Gw2Models.GuildWars2.Wvw.Objective>();
        }
    }
}
