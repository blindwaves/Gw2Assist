using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;

using Newtonsoft.Json;

using Gw2Assist.Anet.GuildWars2.Api.V2.Core.Models.Wvw;
using Gw2Assist.Anet.GuildWars2.Api.V2.Requests.Wvw;

namespace Gw2Assist.Core.Cache.Containers
{
    public class Gw2WvwObjectives : IContainer
    {
        public Dictionary<int, List<Objective>> Contents { get; private set; }
        public string FileFullPath { get; private set; }
        public string Name { get { return this.GetType().Name; } }

        public async Task Create(string fullPath)
        {
            this.FileFullPath = fullPath;

            var identifiers = new Dictionary<string, IEnumerable<string>>();
            identifiers.Add("id", new List<string>() { "all" });

            var request = new Objectives();
            request.Identifiers = identifiers;

            // This will return all the IDs of the objectives only.
            var objectives = await Anet.GuildWars2.Api.V2.Core.Repository.GetAll<Objective>(request);

            // Grab the IDs and sent it back to retrieve all the objectives info.
            var objectiveIds = new List<string>();
            foreach (var obj in objectives)
            {
                objectiveIds.Add(obj.Id);
            }

            identifiers = new Dictionary<string, IEnumerable<string>>();
            identifiers.Add("id", objectiveIds);

            request = new Objectives();
            request.Identifiers = identifiers;

            // This will contain all the objectives information.
            objectives = await Anet.GuildWars2.Api.V2.Core.Repository.GetAll<Objective>(request);

            using (var fstream = new FileStream(this.FileFullPath, FileMode.Create))
            using (var writer = new StreamWriter(fstream))
            {
                writer.WriteLine(JsonConvert.SerializeObject(objectives, Formatting.Indented));
            }
        }

        public void Refresh(string fullPath)
        {
            this.FileFullPath = fullPath;

            var wvWObjectives = new Dictionary<int, List<Objective>>();
            List<Objective> objectives = null;

            using (var reader = new StreamReader(this.FileFullPath, Encoding.UTF8))
            {
                objectives = JsonConvert.DeserializeObject<List<Objective>>(reader.ReadToEnd());
            }

            foreach (var obj in objectives)
            {
                if (!wvWObjectives.ContainsKey(obj.MapId))
                {
                    wvWObjectives.Add(obj.MapId, new List<Objective>());
                }
                wvWObjectives[obj.MapId].Add(obj);
            }

            this.Contents = wvWObjectives;
        }
    }
}
