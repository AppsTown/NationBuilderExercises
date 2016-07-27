using Newtonsoft.Json;

namespace NationBuilder.Models
{
    public class NBPerson : NBObject
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        public override string GetAllEndpoint(string slug)
        {
            return "people/";
        }

        public override string CreateEndpoint(string slug)
        {
            return "people/";
        }

        public override string UpdateEndpoint(string slug)
        {
            return "people/";
        }

        public override string DeleteEndpoint(string slug)
        {
            return "people/";
        }
    }

    public class NBPersonWrapper : NBWrapper
    {
        [JsonProperty("person")]
        private NBPerson Person
        {
            get { return (NBPerson)Record; }
            set { Record = value; }
        }
    }
}