using EcommMarket.Application.ViewModels;

namespace EcommMarket.Application.Interfaces;

public interface IProductService
{
    Task<List<ProductViewModel>> GetAllAsync();
    Task<ProductViewModel> GetByIdAsync(int id);
    Task<ProductViewModel> AddAsync(ProductViewModel entity);
    Task Delete(int id);
    Task Update(ProductViewModel entity);
}
