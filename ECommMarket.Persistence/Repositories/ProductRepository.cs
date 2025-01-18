using ECommMarket.Domain.Entities;
using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;
using Microsoft.EntityFrameworkCore;

namespace ECommMarket.Persistence.Repositories;

public class ProductRepository(MarketDbContext context) : IProductRepository
{
    public async Task<int> AddAsync(Product entity)
    {
        await context.Products.AddAsync(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task Delete(int id)
    {
        var product = await context.Products.Include(x => x.Photos).FirstOrDefaultAsync(p => p.Id == id);
        if(product is not null)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await context.Products.Include(p => p.Photos).ToListAsync();
    }

    public async Task<List<Product>> GetAllByIdAsync(List<int> productIds)
    {
        return context.Products.Include(p => p.Photos).Where(x => productIds.Contains(x.Id)).ToList();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await context.Products.Include(p => p.Photos).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task Update(Product entity)
    {
        var product = await context.Products.Include(p => p.Photos).FirstOrDefaultAsync(p => p.Id == entity.Id);
        if(product is null)
        {
            return;
        }

        product.Photos = entity.Photos;
        product.Quantity = entity.Quantity;
        product.Price = entity.Price;
        product.Description = entity.Description;
        product.UpdateTimestamp = DateTime.Now;
        product.ProductName = entity.ProductName;

        await context.SaveChangesAsync();
    }
}
