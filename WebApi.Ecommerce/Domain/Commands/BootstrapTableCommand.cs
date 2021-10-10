namespace WebApi.Ecommerce.Domain.Commands
{
    public class BootstrapTableCommand
    {
        // Properties
        public int Limit { get; set; }

        public int Offset { get; set; }

        public string Sort { get; set; }

        public string Order { get; set; }
    }
}
