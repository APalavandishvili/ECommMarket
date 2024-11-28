using ECommMarket.Domain.Interfaces;
using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;
using Microsoft.EntityFrameworkCore;

namespace ECommMarket.Persistence.Repositories;

public class RepositoryBase<TEntity, TEntityId> : IRepository<TEntity, TEntityId>
    where TEntity : class, IEntityBase<TEntityId>
{
    protected readonly MarketDbContext db;
    public RepositoryBase(MarketDbContext db)
    {
        this.db = db;
    }

    public virtual async Task<TEntityId> AddAsync(TEntity entity)
    {
        await db.Set<TEntity>().AddAsync(entity);

        await db.SaveChangesAsync();

        return entity.Id;
    }

    public virtual async Task Delete(TEntityId id)
    {
        TEntity entity = await db.Set<TEntity>().FindAsync(id);

        if(entity is not null)
        {
            db.Remove(entity);
            await db.SaveChangesAsync();
        }
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        IQueryable<TEntity> query = db.Set<TEntity>();

        return await query.ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(int id)
    {
        IQueryable<TEntity> query = db.Set<TEntity>();

        TEntity entity = await query.FirstOrDefaultAsync(entity => entity.Id.Equals(id));

        return entity;
    }

    public virtual async Task Update(TEntity entity)
    {
        db.Set<TEntity>().Update(entity);

        await db.SaveChangesAsync();
    }
}
