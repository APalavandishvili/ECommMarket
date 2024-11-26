using ECommMarket.Domain.Entities;
using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;
using Microsoft.EntityFrameworkCore;

namespace ECommMarket.Persistence.Repositories
{
    public class CartRepository : AbstractRepository, ICartRepository
    {
        public CartRepository(MarketDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Cart entity)
        {
            context.Carts.Add(entity);
            await context.SaveChangesAsync();
        }

        public void Delete(Cart entity)
        {
            context.Carts.Remove(entity);
            context.SaveChanges();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var cart = context.Carts.FirstOrDefault(c => c.Id == id);
            if (cart == null)
            {
                Console.WriteLine($"Cart with Id : {id} Was not found");
            }
            context.Carts.Remove(cart);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            var carts = await context.Carts.ToListAsync();
            return carts;
        }

        public async Task<Cart> GetByIdAsync(int id)
        {
            var cart = await context.Carts.FirstOrDefaultAsync(c => c.Id == id);
            return cart;
        }

        public void Update(Cart entity)
        {
            var oldEntity = context.Carts.FirstOrDefault(c => c.Id == entity.Id);
            if (oldEntity != null)
            {
                oldEntity.UpdateBy = entity.UpdateBy;
                oldEntity.UpdateDate = entity.UpdateDate;
                oldEntity.CreationDate = entity.CreationDate;
                oldEntity.UserId = entity.UserId;
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Cart With Id : {entity.Id} Was not Found");
            }
        }
    }
}
