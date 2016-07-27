using Newtonsoft.Json;

namespace NationBuilder.Models
{
    public abstract class NBObject
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        public abstract string GetAllEndpoint(string slug);

        public abstract string CreateEndpoint(string slug);

        public abstract string UpdateEndpoint(string slug);

        public abstract string DeleteEndpoint(string slug);
    }
}
