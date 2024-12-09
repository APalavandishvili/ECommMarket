using ECommMarket.Domain.Entities;
using ECommMarket.Domain.Interfaces;

namespace ECommMarket.Persistence.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<int> AddAsync(TEntity entity);
        Task Delete(int id);
        Task Update(TEntity entity);
    }
}
