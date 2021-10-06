namespace WebApi.Ecommerce.Domain.Commands
{
    public class FilterCommand
    {
        // Constructor
        protected FilterCommand(int limit, int offset)
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
