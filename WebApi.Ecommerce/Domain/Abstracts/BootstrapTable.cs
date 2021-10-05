namespace WebApi.Ecommerce.Domain.Abstracts
{
    public abstract class BootstrapTable
    {
        // Constructor
        protected BootstrapTable(int limit, int offset)
        {
            Limit = limit;
            Offset = offset;
        }

        // Properties
        public int Limit { get; set; }

        public int Offset { get; set; }

        public string Sort { get; set; }

        public string Order { get; set; }
    }
}
