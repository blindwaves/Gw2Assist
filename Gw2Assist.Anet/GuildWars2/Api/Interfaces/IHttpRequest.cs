using System.Collections.Generic;

namespace Gw2Assist.Anet.GuildWars2.Api.Interfaces
{
    public interface IHttpRequest
    {
        IEnumerable<KeyValuePair<string, string>> Parameters { get; }
        string ResourcePath { get; }
    }
}
