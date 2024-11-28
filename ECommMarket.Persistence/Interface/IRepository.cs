using ECommMarket.Domain.Entities;
using ECommMarket.Domain.Interfaces;

namespace ECommMarket.Persistence.Interface
{
    public interface IRepository<TEntity, TEntityId> 
        where TEntity : IEntityBase<TEntityId>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntityId> AddAsync(TEntity entity);
        Task Delete(TEntityId id);
        Task Update(TEntity entity);
    }
}
