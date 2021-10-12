using System.Text.Json.Serialization;

namespace WebApi.Ecommerce.Domain.Abstracts
{
    public abstract class Paginated
    {
        [JsonIgnore]
        public int Total { get; private set; }
    }
}
