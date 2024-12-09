using ECommMarket.Domain.Entities;
using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;

namespace ECommMarket.Persistence.Repositories;

public class CartRepository(MarketDbContext context) : ICartRepository
{
    public Task<int> AddAsync(Cart entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Cart>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Cart> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Cart entity)
    {
        throw new NotImplementedException();
    }
}
