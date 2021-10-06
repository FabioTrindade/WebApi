using Newtonsoft.Json;
using System;

namespace WebApi.Ecommerce.Commons
{
    public static class Utils
    {
        /// <summary>
        /// Retorna um objecto serializado, caso a deserialização de erro retorna objeto instanciado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string content)
        {
            var retorno = Activator.CreateInstance<T>();
            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
            }
            return retorno;
        }
    }
}
