using System.Collections.Generic;
using System.Threading.Tasks;

using Gw2ApiRequest = Gw2Assist.Anet.GuildWars2.Api.V2.Requests;

namespace Gw2Assist.Core.Cache.Containers
{
    public class Gw2WvwMatch : Interfaces.IContainer
    {
        public Models.GuildWars2.Wvw.Match Contents { get; private set; }
        public string FileFullPath { get; private set; }
        public string Name { get { return this.GetType().Name; } }

        private int worldId = 0;

        public Task Create(string storagePath)
        {
            return Task.FromResult(false);
        }

        public async void Refresh(string storagePath)
        {
            if (this.worldId == 0) return;

            var identifiers = new Dictionary<string, IEnumerable<string>>();
            identifiers.Add("world", new List<string>() { this.worldId.ToString() });

            var request = new Gw2ApiRequest.Wvw.Matches();
            request.Identifiers = identifiers;

            this.Contents = await Anet.GuildWars2.Api.V2.Repository.Get<Models.GuildWars2.Wvw.Match>(request);
        }

        public void Refresh(string storagePath, int worldId)
        {
            this.worldId = worldId;
            this.Refresh(storagePath);
        }
    }
}
