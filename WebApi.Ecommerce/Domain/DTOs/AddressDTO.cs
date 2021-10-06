namespace WebApi.Ecommerce.Domain.DTOs
{
    public class AddressDTO
    {
        public AddressDTO(string zipCode
            , string address
            , string complement
            , string neighborhood
            , string city
            , string state
            , string ddd)
        {
            ZipCode = zipCode;
            Address = address;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Ddd = ddd;
        }

        public string ZipCode { get; private set; }

        public string Address { get; private set; }

        public string Complement { get; private set; }

        public string Neighborhood { get; private set; }

        public string City { get; private set; }

        public string State { get; private set; }

        public string Ddd { get; private set; }
    }
}
