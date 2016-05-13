using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Gw2Assist.Anet.GuildWars2.Api.V2.Requests;

namespace Gw2Assist.Anet.GuildWars2.Api.V2
{
    public static class Repository
    {
        private static readonly Uri BaseUri = new Uri("https://api.guildwars2.com/v2/");
        private static readonly JsonSerializer JsonSerializer = new JsonSerializer();
        private static readonly HttpServiceClient ServiceClient = new HttpServiceClient(BaseUri);

        public static async Task<T> Get<T>(HttpRequest request)
        {
            string response = await ServiceClient.GetAsync(request);
            return JsonSerializer.Deserialize<T>(response);
        }

        public static async Task<IQueryable<T>> GetAll<T>(HttpRequest request)
        {
            string response = await ServiceClient.GetAsync(request);
            return JsonSerializer.Deserialize<List<T>>(response).AsQueryable();
        }
    }
}
