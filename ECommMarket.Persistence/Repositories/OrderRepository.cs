using ECommMarket.Domain.Entities;
using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;
using Microsoft.EntityFrameworkCore;

namespace ECommMarket.Persistence.Repositories
{
    public class OrderRepository : AbstractRepository, IOrderRepository
    {
        public OrderRepository(MarketDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Order entity)
        {
            context.Orders.Add(entity);
            await context.SaveChangesAsync();
        }

        public void Delete(Order entity)
        {
            context.Orders.Remove(entity);
            context.SaveChanges();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var order = context.Orders.FirstOrDefault(o => o.Id == id);
            if(order == null)
            {
                Console.WriteLine($"Order with Id : {id} Was not Found");
            }
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var orders = await context.Orders.ToListAsync();
            return orders;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }

        public void Update(Order entity)
        {
            var oldEntity = context.Orders.FirstOrDefault(o => o.Id == entity.Id);
            if (oldEntity != null)
            {
                oldEntity.UpdateBy = entity.UpdateBy;
                oldEntity.UpdateDate = entity.UpdateDate;
                oldEntity.CreationDate = entity.CreationDate;
                oldEntity.TransactionId = entity.TransactionId;
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Order With Id : {entity.Id} Was not Found");
            }
        }
    }
}
