using ECommMarket.Domain.Entities;
using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;
using Microsoft.EntityFrameworkCore;

namespace ECommMarket.Persistence.Repositories
{
    public class PhotoRepository : AbstractRepository, IPhotoRepository
    {
        public PhotoRepository(MarketDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Photo entity)
        {
            context.Photos.Add(entity);
            await context.SaveChangesAsync();
        }

        public void Delete(Photo entity)
        {
            context.Photos.Remove(entity);
            context.SaveChanges();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var photo = context.Photos.FirstOrDefault(p => p.Id == id);
            if (photo == null)
            {
                Console.WriteLine($"Photo With Id : {id} Was not Found");
            }
            context.Photos.Remove(photo);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Photo>> GetAllAsync()
        {
            var photos = await context.Photos.ToListAsync();
            return photos;
        }

        public async Task<Photo> GetByIdAsync(int id)
        {
            var photo = await context.Photos.FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }

        public void Update(Photo entity)
        {
            var oldEntity = context.Photos.FirstOrDefault(p => p.Id == entity.Id);
            if (oldEntity != null)
            {
                oldEntity.UpdateBy = entity.UpdateBy;
                oldEntity.UpdateDate = entity.UpdateDate;
                oldEntity.CreationDate = entity.CreationDate;
                oldEntity.PhotoName = entity.PhotoName;
                oldEntity.PhotoUrl = entity.PhotoUrl;
                oldEntity.PhotoHeight = entity.PhotoHeight;
                oldEntity.PhotoWidth = entity.PhotoWidth;
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Photo with Id : {entity.Id} Was not found");
            }
        }
    }
}
