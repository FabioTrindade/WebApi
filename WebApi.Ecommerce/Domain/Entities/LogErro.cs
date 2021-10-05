using WebApi.Ecommerce.Domain.Abstracts;

namespace WebApi.Ecommerce.Domain.Entities
{
    public class LogErro : Entity
    {
        public LogErro()
        {
        }

        public LogErro(string method
            , string path
            , string erro
            , string erroCompleto
            , string query)
        {
            Method = method;
            Path = path;
            Erro = erro;
            ErroCompleto = erroCompleto;
            Query = query;
        }


        // Properties
        public string Method { get; private set; }

        public string Path { get; private set; }

        public string Erro { get; private set; }

        public string ErroCompleto { get; private set; }

        public string Query { get; private set; }


        // Modifier
        public void SetMethod(string method)
        {
            this.Method = method;
        }

        public void SetPath(string path)
        {
            this.Path = path;
        }

        public void SetErro(string erro)
        {
            this.Erro = erro;
        }

        public void SetErroCompleto(string erroCompleto)
        {
            this.ErroCompleto = erroCompleto;
        }

        public void SetQuery(string query)
        {
            this.Query = query;
        }
    }
}
