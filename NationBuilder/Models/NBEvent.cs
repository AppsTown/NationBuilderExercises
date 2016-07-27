using Newtonsoft.Json;

namespace NationBuilder.Models
{
    public class NBEvent : NBObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("intro")]
        public string Intro { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty("start_time")]
        public string StartTime { get; set; }

        [JsonProperty("end_time")]
        public string EndTime { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        public override string GetAllEndpoint(string slug)
        {
            return "sites/" + slug + "/pages/events/";
        }

        public override string CreateEndpoint(string slug)
        {
            return "sites/" + slug + "/pages/events/";
        }

        public override string UpdateEndpoint(string slug)
        {
            return "sites/" + slug + "/pages/events/";
        }

        public override string DeleteEndpoint(string slug)
        {
            return "sites/" + slug + "/pages/events/";
        }
    }

    public class NBEventWrapper : NBWrapper
    {
        [JsonProperty("event")]
        private NBEvent Event
        {
            get { return (NBEvent)Record; }
            set { Record = value; }
        }
    }
}
