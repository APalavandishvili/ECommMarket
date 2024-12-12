using EcommMarket.Application.Dto;

namespace EcommMarket.Application.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync();
    Task<List<ProductDto>> GetAllByIdAsync(List<int> productIds);
    Task<ProductDto> GetByIdAsync(int id);
    Task<ProductDto> AddAsync(ProductDto entity);
    Task Delete(int id);
    Task Update(ProductDto entity);
    Task UpdatePhotos(List<PhotoDto> photos);

}
