using Newtonsoft.Json;

namespace WebApi.Ecommerce.Domain.Responses
{
    public class ZipCodeResponse
    {
        [JsonProperty("cep")]
        public string ZipCode { get; set; }

        [JsonProperty("logradouro")]
        public string Address { get; set; }

        [JsonProperty("complemento")]
        public string Complement { get; set; }

        [JsonProperty("bairro")]
        public string Neighborhood { get; set; }

        [JsonProperty("localidade")]
        public string City { get; set; }

        [JsonProperty("uf")]
        public string State { get; set; }

        [JsonProperty("ibge")]
        public string Ibge { get; set; }

        [JsonProperty("gia")]
        public string Gia { get; set; }

        [JsonProperty("ddd")]
        public string Ddd { get; set; }

        [JsonProperty("siafi")]
        public string Siafi { get; set; }
    }
}
