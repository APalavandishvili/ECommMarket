using ECommMarket.Domain.Entities;
using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;
using Microsoft.EntityFrameworkCore;

namespace ECommMarket.Persistence.Repositories;

public class PhotoRepository(MarketDbContext context) : IPhotoRepository
{
    public async Task<int> AddAsync(Photo entity)
    {
        await context.Photos.AddAsync(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task Delete(int id)
    {
        var photo = await context.Photos.FirstOrDefaultAsync(p => p.Id == id);
        if (photo is not null)
        {
            context.Photos.Remove(photo);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Photo>> GetAllAsync()
    {
        return await context.Photos.Include(p => p.Products).Include(p => p.News).ToListAsync();
    }

    public async Task<Photo> GetByIdAsync(int id)
    {
        return await context.Photos.Include(p => p.Products).Include(p => p.News).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task Update(Photo entity)
    {
        Photo? photo = await context.Photos.Include(p => p.Products).Include(p => p.News).FirstOrDefaultAsync(p => p.Id == entity.Id);
        if (photo is null)
        {
            return;
        }

        photo.PhotoUrl = entity.PhotoUrl;
        photo.PhotoName = entity.PhotoName;
        photo.UpdateTimestamp = DateTime.Now;

        await context.SaveChangesAsync();
    }
}
