using Newtonsoft.Json;

namespace NationBuilder.Models
{
    public class NBWrapper
    {
        [JsonIgnore]
        public NBObject Record { get; set; }
    }
}
