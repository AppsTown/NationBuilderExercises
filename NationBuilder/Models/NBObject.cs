using Newtonsoft.Json;

namespace NationBuilder.Models
{
    public abstract class NBObject
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        public abstract string GetAllEndpoint(string siteSlug);

        public abstract string CreateEndpoint(string siteSlug);

        public abstract string UpdateEndpoint(string siteSlug);

        public abstract string DeleteEndpoint(string siteSlug);
    }
}
