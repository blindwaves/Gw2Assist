using System.Collections.Generic;

namespace Gw2Assist.Anet.GuildWars2.Api
{
    public interface IHttpRequest
    {
        IEnumerable<KeyValuePair<string, string>> Parameters { get; }
        string ResourcePath { get; }
    }
}
