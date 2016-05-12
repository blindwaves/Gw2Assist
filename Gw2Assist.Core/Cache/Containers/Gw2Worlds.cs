using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;

using Newtonsoft.Json;

using Gw2Assist.Anet.GuildWars2.Api.V2.Core.Models;
using Gw2Assist.Anet.GuildWars2.Api.V2.Requests;

namespace Gw2Assist.Core.Cache.Containers
{
    public class Gw2Worlds : IContainer
    {
        public Dictionary<int, World> Contents { get; private set; }
        public string FileFullPath { get; private set; }
        public string Name { get { return this.GetType().Name; } }

        public async Task Create(string fullPath)
        {
            this.FileFullPath = fullPath;

            var identifiers = new Dictionary<string, IEnumerable<string>>();
            identifiers.Add("id", new List<string>() { "all" });

            var request = new Worlds();
            request.Identifiers = identifiers;

            // This will return all the IDs of the objectives only.
            var worlds = await Anet.GuildWars2.Api.V2.Core.Repository.GetAll<World>(request);

            using (var fstream = new FileStream(this.FileFullPath, FileMode.Create))
            using (var writer = new StreamWriter(fstream))
            {
                writer.WriteLine(JsonConvert.SerializeObject(worlds, Formatting.Indented));
            }
        }

        public void Refresh(string fullPath)
        {
            this.FileFullPath = fullPath;

            var gw2Worlds = new Dictionary<int, World>();
            List<World> worlds = null;

            using (var reader = new StreamReader(this.FileFullPath, Encoding.UTF8))
            {
                worlds = JsonConvert.DeserializeObject<List<World>>(reader.ReadToEnd());
            }

            foreach (var world in worlds)
            {
                gw2Worlds.Add(world.Id, world);
            }

            this.Contents = gw2Worlds;
        }
    }
}
