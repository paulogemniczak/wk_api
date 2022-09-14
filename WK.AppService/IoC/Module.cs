using Methods.AppService.Services;
using WK.AppService.Interfaces;

namespace WK.AppService.IoC
{
    public static class Module
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            Dictionary<Type, Type> dictionary = new()
            {
                {typeof(ICategoryAppService), typeof(CategoryAppService)},
                {typeof(IProductAppService), typeof(ProductAppService)},
            };

            return dictionary;
        }
    }
}
