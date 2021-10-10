namespace WebApi.Ecommerce.Configurations
{
    public static class Settings
    {
        /// <summary>
        /// Atributo para controlar url para consulta de cep
        /// </summary>
        public static string ViaCep { get; set; }

        /// <summary>
        /// Atribut utilizado para definir cidade base
        /// </summary>
        public static string City { get; set; }

        /// <summary>
        /// Atribut utilizado para definir estado base
        /// </summary>
        public static string State { get; set; }

        /// <summary>
        /// Atributo utilizado para definir frete primeira modalidade - Mesma Cidade
        /// </summary>
        public static decimal StepOne { get; set; }

        /// <summary>
        /// Atributo utilizado para definir frete segunda modalidade - Outras Cidade
        /// </summary>
        public static decimal StepTwo { get; set; }

        /// <summary>
        /// Atributo utilizado para definir frete terceira modalidade - Outro Estado
        /// </summary>
        public static decimal StepThree { get; set; }
    }
}
