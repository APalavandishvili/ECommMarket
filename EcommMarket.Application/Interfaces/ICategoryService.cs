using EcommMarket.Application.Dto;

namespace EcommMarket.Application.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();
    Task<int> AddAsync(CategoryDto category);
}
