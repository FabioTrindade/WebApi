using Newtonsoft.Json;

namespace WebApi.Ecommerce.Domain.Responses
{
    public class ZipCodeResponse
    {
        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("localidade")]
        public string Localidade { get; set; }

        [JsonProperty("uf")]
        public string Uf { get; set; }

        [JsonProperty("ibge")]
        public long Ibge { get; set; }

        [JsonProperty("gia")]
        public long Gia { get; set; }

        [JsonProperty("ddd")]
        public int Ddd { get; set; }

        [JsonProperty("siafi")]
        public long Siafi { get; set; }
    }
}
