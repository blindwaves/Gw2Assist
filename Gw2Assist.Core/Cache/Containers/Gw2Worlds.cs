using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;

using Newtonsoft.Json;

using Gw2ApiRequest = Gw2Assist.Anet.GuildWars2.Api.V2.Requests;

namespace Gw2Assist.Core.Cache.Containers
{
    public class Gw2Worlds : Interfaces.IContainer
    {
        public Dictionary<int, Models.GuildWars2.World> Contents { get; private set; }
        public string FileFullPath { get; private set; }
        public string Name { get { return this.GetType().Name; } }

        public async Task Create(string storagePath)
        {
            this.FileFullPath = storagePath + "/" + this.Name + ".json";

            var identifiers = new Dictionary<string, IEnumerable<string>>();
            identifiers.Add("id", new List<string>() { "all" });

            var request = new Gw2ApiRequest.Worlds();
            request.Identifiers = identifiers;

            var worlds = await Anet.GuildWars2.Api.V2.Repository.GetAll<Models.GuildWars2.World>(request);

            using (var fstream = new FileStream(this.FileFullPath, FileMode.Create))
            using (var writer = new StreamWriter(fstream))
            {
                writer.WriteLine(JsonConvert.SerializeObject(worlds, Formatting.Indented));
            }
        }

        public void Refresh(string storagePath)
        {
            this.FileFullPath = storagePath + "/" + this.Name + ".json";

            var gw2Worlds = new Dictionary<int, Models.GuildWars2.World>();
            List<Models.GuildWars2.World> worlds = null;

            using (var reader = new StreamReader(this.FileFullPath, Encoding.UTF8))
            {
                worlds = JsonConvert.DeserializeObject<List<Models.GuildWars2.World>>(reader.ReadToEnd());
            }

            foreach (var world in worlds)
            {
                gw2Worlds.Add(world.Id, world);
            }

            this.Contents = gw2Worlds;
        }
    }
}
