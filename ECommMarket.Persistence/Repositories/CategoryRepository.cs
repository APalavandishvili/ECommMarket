using ECommMarket.Domain.Entities;
using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMarket.Persistence.Repositories;

public class CategoryRepository(MarketDbContext context) : ICategoryRepository
{
    public async Task<int> AddAsync(Category entity)
    {
        await context.Categories.AddAsync(entity);
        return entity.Id;
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await context.Categories.ToListAsync();
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        return await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task Update(Category entity)
    {
        throw new NotImplementedException();
    }
}
