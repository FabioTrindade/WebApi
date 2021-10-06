namespace WebApi.Ecommerce.Domain.DTOs
{
    public class ShippingDTO
    {
        // Constructor
        public ShippingDTO()
        {

        }

        public ShippingDTO(string city
            , string state
            , decimal amount
            , string observation)
        {
            City = city;
            State = state;
            Amount = amount;
            Observation = observation;
        }


        // Properties
        /// <summary>
        /// Atributo utilizado para definir a cidade
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir o  estado
        /// </summary>
        public string State { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir o valor do frete
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// Atributo utilizado para definir a observação
        /// </summary>
        public string Observation { get; private set; }
    }
}
