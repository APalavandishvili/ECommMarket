using ECommMarket.Domain.Entities;

namespace ECommMarket.Persistence.Interface
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteByIdAsync(int id);
        void Update(TEntity entity);
    }
}
