using EcommMarket.Application.Dto;

namespace EcommMarket.Application.Interfaces;

public interface IOrderService
{
    Task<List<OrderDto>> GetAllAsync();
    Task<List<OrderDto>> GetAllByIdAsync(List<int> productIds);
    Task<OrderDto> GetByIdAsync(int id);
    Task<OrderDto> AddAsync(OrderDto entity);
    Task Delete(int id);
    Task Update(OrderDto entity);
    Task UpdatePhotos(List<PhotoDto> photos);
}
