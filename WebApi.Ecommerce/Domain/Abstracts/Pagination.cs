namespace WebApi.Ecommerce.Domain.Abstracts
{
    public abstract class Pagination
    {
        public int PerPage { get; set; }
        public int CurrentPage { get; set; }
        public int LastPage { get; set; }
        public int Total { get; set; }
    }
}
