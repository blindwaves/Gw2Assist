using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gw2Assist.Anet.GuildWars2.Api.V2.Requests
{
    public abstract class HttpRequest : Interfaces.IHttpRequest
    {
        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> Identifiers { get; set; }

        public abstract string ResourcePath { get; }

        public IEnumerable<KeyValuePair<string, string>> Parameters
        {
            get
            {
                var parameters = new Dictionary<string, string>();

                if (this.Identifiers != null)
                {
                    foreach(var identifier in this.Identifiers)
                    {
                        switch(identifier.Key)
                        {
                            case "id":
                                var identifierKey = "id";
                                var stringBuilder = new StringBuilder();
                                var validIdentifiers = identifier.Value.Where(i => !string.IsNullOrEmpty(i));

                                if (validIdentifiers.Count() > 1 ||
                                    (validIdentifiers.Count() == 1 && string.Compare(validIdentifiers.ElementAt(0), "all") == 0))
                                {
                                    identifierKey = "ids";
                                }

                                parameters.Add(identifierKey, string.Join(",", validIdentifiers));

                                break;
                            default:
                                parameters.Add(identifier.Key, identifier.Value.First());
                                break;
                        }
                    }
                }

                return parameters;
            }
        }
    }
}
