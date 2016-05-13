using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gw2Assist.Anet.GuildWars2.Api
{
    public sealed class HttpServiceClient
    {
        private readonly Uri baseUri;

        public HttpServiceClient(Uri baseUri)
        {
            this.baseUri = baseUri;
        }

        /// <summary>
        /// Gets end point response.
        /// </summary>
        /// <param name="resourcePath">The end path of the resource relative to the base URI.</param>
        /// <param name="parameters">The GET parameters to pass to the server.</param>
        /// <returns>A JSON response string.</returns>
        public async Task<string> GetAsync(Interfaces.IHttpRequest request)
        {
            // http://codereview.stackexchange.com/a/91931
            var stringBuilder = new StringBuilder();
            var validParameters = request.Parameters.Where(p => !string.IsNullOrEmpty(p.Value));
            var formattedParameters = validParameters.Select(p => p.Key + "=" + p.Value);
            stringBuilder.Append(string.Join("&", formattedParameters));

            var endPoint = new Uri(this.baseUri, request.ResourcePath + "?" + stringBuilder.ToString());

            // http://stackoverflow.com/a/17459045
            var httpClient = new HttpClient();
            httpClient.BaseAddress = endPoint;
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(
                "application/json"
                ));

            try
            {
                var httpResponseMessage = await httpClient.GetAsync("");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return await httpResponseMessage.Content.ReadAsStringAsync();
                }
            }
            catch (System.AggregateException) { /* Absorb it. */ }

            return string.Empty;
        }
    }
}
