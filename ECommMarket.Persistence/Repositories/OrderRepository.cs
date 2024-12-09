using ECommMarket.Domain.Entities;
using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;
using Microsoft.EntityFrameworkCore;

namespace ECommMarket.Persistence.Repositories;

public class OrderRepository(MarketDbContext context) : IOrderRepository
{
    public async Task<int> AddAsync(Order entity)
    {
        await context.Orders.AddAsync(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task Delete(int id)
    {
        Order? order = await context.Orders.FirstOrDefaultAsync(p => p.Id == id);
        if (order is not null)
        {
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await context.Orders.Include(x => x.Items).ToListAsync();
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await context.Orders.Include(x => x.Items).FirstOrDefaultAsync(o => o.Id == id);
    }

    public Task Update(Order entity)
    {
        throw new NotImplementedException();
    }
}
