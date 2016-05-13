using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;

using Newtonsoft.Json;

using Gw2ApiRequest = Gw2Assist.Anet.GuildWars2.Api.V2.Requests;

namespace Gw2Assist.Core.Cache.Containers
{
    public class Gw2WvwObjectives : Interfaces.IContainer
    {
        public Dictionary<int, List<Core.Models.Wvw.Objective>> Contents { get; private set; }
        public string FileFullPath { get; private set; }
        public string Name { get { return this.GetType().Name; } }

        public async Task Create(string storagePath)
        {
            this.FileFullPath = storagePath + "/" + this.Name + ".json";

            var identifiers = new Dictionary<string, IEnumerable<string>>();
            identifiers.Add("id", new List<string>() { "all" });

            var request = new Gw2ApiRequest.Wvw.Objectives();
            request.Identifiers = identifiers;

            // This will return all the IDs of the objectives only.
            var wvwObjectives = await Anet.GuildWars2.Api.V2.Repository.GetAll<Models.Wvw.Objective>(request);

            // Grab the IDs and sent it back to retrieve all the objectives info.
            var objectiveIds = new List<string>();
            foreach (var obj in wvwObjectives)
            {
                objectiveIds.Add(obj.Id);
            }

            identifiers = new Dictionary<string, IEnumerable<string>>();
            identifiers.Add("id", objectiveIds);

            request = new Gw2ApiRequest.Wvw.Objectives();
            request.Identifiers = identifiers;

            // This will contain all the objectives information.
            wvwObjectives = await Anet.GuildWars2.Api.V2.Repository.GetAll<Models.Wvw.Objective>(request);

            using (var fstream = new FileStream(this.FileFullPath, FileMode.Create))
            using (var writer = new StreamWriter(fstream))
            {
                writer.WriteLine(JsonConvert.SerializeObject(wvwObjectives, Formatting.Indented));
            }
        }

        public void Refresh(string storagePath)
        {
            this.FileFullPath = storagePath + "/" + this.Name + ".json";

            var wvWObjectives = new Dictionary<int, List<Models.Wvw.Objective>>();
            List<Models.Wvw.Objective> objectives = null;

            using (var reader = new StreamReader(this.FileFullPath, Encoding.UTF8))
            {
                objectives = JsonConvert.DeserializeObject<List<Models.Wvw.Objective>>(reader.ReadToEnd());
            }

            foreach (var obj in objectives)
            {
                if (!wvWObjectives.ContainsKey(obj.MapId))
                {
                    wvWObjectives.Add(obj.MapId, new List<Models.Wvw.Objective>());
                }
                wvWObjectives[obj.MapId].Add(obj);
            }

            this.Contents = wvWObjectives;
        }
    }
}
