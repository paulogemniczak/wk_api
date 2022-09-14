using WK.Data.Repositories;
using WK.Domain;
using WK.Domain.InterfaceRepositories;

namespace WK.Data.IoC
{
  public static class Module
  {
    public static Dictionary<Type, Type> GetTypes()
    {
      Dictionary<Type, Type> dictionary = new()
      {
        {typeof(IUnitOfWork), typeof(UnitOfWork)},
        {typeof(ICategoryRepository), typeof(CategoryRepository)},
        {typeof(IProductRepository), typeof(ProductRepository)},
      };

      return dictionary;
    }
  }
}
