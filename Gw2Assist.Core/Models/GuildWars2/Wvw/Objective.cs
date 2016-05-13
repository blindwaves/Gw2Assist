using Gw2Models = Gw2Assist.Anet.GuildWars2.Api.V2.Models;

namespace Gw2Assist.Core.Models.GuildWars2.Wvw
{
    public class Objective : Gw2Models.Wvw.Objective
    {
        public double DistanceFromAvatar { get; set; }
    }
}
