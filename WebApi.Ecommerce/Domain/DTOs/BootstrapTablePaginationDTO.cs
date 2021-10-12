using System.Collections.Generic;

namespace WebApi.Ecommerce.Domain.DTOs
{
    public class BootstrapTablePaginationDTO<PaginatedEntity>
    {
        public List<PaginatedEntity> Rows { get; set; }

        public int Total { get; set; }
    }
}
