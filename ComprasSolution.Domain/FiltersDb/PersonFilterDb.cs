using ComprasSolution.Infra.Data.Repositories;

namespace ComprasSolution.Domain.FiltersDb
{
    public class PersonFilterDb : PagedBaseRequest
    {
        public string? Name { get; set; }
    }
}
