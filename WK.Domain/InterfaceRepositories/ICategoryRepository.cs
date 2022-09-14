using WK.Domain.Entities;
using WK.Domain.Filters;

namespace WK.Domain.InterfaceRepositories
{
    public interface ICategoryRepository : IRepository<Category, CategoryFilter>
    {
        // add new methods here if that is necessary
    }
}
