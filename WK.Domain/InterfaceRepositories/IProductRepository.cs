using WK.Domain.Entities;
using WK.Domain.Filters;

namespace WK.Domain.InterfaceRepositories
{
    public interface IProductRepository : IRepository<Product, ProductFilter>
    {
        // add new methods here if that is necessary
    }
}
