using System.Collections.Generic;
using System.Threading.Tasks;

using Gw2ApiRequest = Gw2Assist.Anet.GuildWars2.Api.V2.Requests;

namespace Gw2Assist.Core.Cache.Containers
{
    public class Gw2WvwMatch : Interfaces.IContainer
    {
        public Models.Wvw.Match Contents { get; private set; }
        public string FileFullPath { get; private set; }
        public string Name { get { return this.GetType().Name; } }

        public async Task Create(string storagePath)
        {
            // TODO: Implement settings and grab the world.
            var settingsWorldId = 1005;
            // TODO: NULL check for settings, for new app.

            var identifiers = new Dictionary<string, IEnumerable<string>>();
            identifiers.Add("world", new List<string>() { settingsWorldId.ToString() });

            var request = new Gw2ApiRequest.Wvw.Matches();
            request.Identifiers = identifiers;

            this.Contents = await Anet.GuildWars2.Api.V2.Repository.Get<Models.Wvw.Match>(request);
        }

        public async void Refresh(string storagePath)
        {
            await this.Create(storagePath);
        }
    }
}
