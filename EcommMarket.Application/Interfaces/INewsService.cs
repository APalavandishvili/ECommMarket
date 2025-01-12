using EcommMarket.Application.Dto;
namespace EcommMarket.Application.Interfaces
{
    public interface INewsService
    {
        Task<List<NewsDto>> GetAllAsync();
        Task<NewsDto> GetByIdAsync(int id);
        Task<NewsDto> AddAsync(NewsDto entity);
        Task Delete(int id);
        Task Update(NewsDto entity);
    }
}
