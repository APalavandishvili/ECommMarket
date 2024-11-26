using ECommMarket.Domain.Entities;
using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;
using Microsoft.EntityFrameworkCore;

namespace ECommMarket.Persistence.Repositories
{
    public class ProductRepository : AbstractRepository, IProductRepository
    {
        public ProductRepository(MarketDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Product entity)
        {
            context.Products.Add(entity);
            await context.SaveChangesAsync();
        }

        public void Delete(Product entity)
        {
            context.Products.Remove(entity);
            context.SaveChanges();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var product = context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                Console.WriteLine($"Product with Id : {id} Was not Found");
            }
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public void Update(Product entity)
        {
            var oldEntity = context.Products.FirstOrDefault(p => p.Id == entity.Id);
            if(oldEntity != null)
            {
                oldEntity.UpdateBy = entity.UpdateBy;
                oldEntity.UpdateDate = entity.UpdateDate;
                oldEntity.CreationDate = entity.CreationDate;
                oldEntity.ProductName = entity.ProductName;
                oldEntity.Quantity = entity.Quantity;
                oldEntity.Price = entity.Price;
                oldEntity.Description = entity.Description;
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Product With Id : {entity.Id} Was not Found");
            }
        }
    }
}
