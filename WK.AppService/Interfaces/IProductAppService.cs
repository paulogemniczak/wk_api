using WK.AppService.Dtos;
using WK.AppService.Filters;

namespace WK.AppService.Interfaces
{
    public interface IProductAppService
    {
        Task<ProductDto> Create(ProductDto obj);
        Task<bool> Update(ProductDto obj);
        Task<bool> Delete(int id);
        Task<ProductDto> GetById(int id);
        Task<IEnumerable<ProductDto>> List(ProductFilterDto filter);
    }
}
