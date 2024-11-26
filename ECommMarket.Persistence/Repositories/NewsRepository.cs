using ECommMarket.Domain.Entities;
using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;
using Microsoft.EntityFrameworkCore;

namespace ECommMarket.Persistence.Repositories
{
    public class NewsRepository : AbstractRepository, INewsRepository
    {
        public NewsRepository(MarketDbContext context) : base(context)
        {
        }

        public async Task AddAsync(News entity)
        {
            context.News.Add(entity);
            await context.SaveChangesAsync();
        }

        public void Delete(News entity)
        {
            context.News.Remove(entity);
            context.SaveChanges();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var news = context.News.FirstOrDefault(x => x.Id == id);
            if (news == null)
            {
                Console.WriteLine($"Report with Id : {id} Was not found");
            }
            context.News.Remove(news);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<News>> GetAllAsync()
        {
            var news = await context.News.ToListAsync();
            return news;
        }

        public async Task<News> GetByIdAsync(int id)
        {
            var news = await context.News.FirstOrDefaultAsync(x => x.Id == id);
            return news;
        }

        public void Update(News entity)
        {
            var oldEntity = context.News.FirstOrDefault(x => x.Id == entity.Id);
            if (oldEntity != null)
            {
                oldEntity.UpdateBy = entity.UpdateBy;
                oldEntity.UpdateDate = entity.UpdateDate;
                oldEntity.CreationDate = entity.CreationDate;
                oldEntity.Description = entity.Description;
                oldEntity.Details = entity.Details;
                oldEntity.AuthorId = entity.AuthorId;
                oldEntity.NewsInfo = entity.NewsInfo;
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Report with Id : {entity.Id} Was not Found");
            }
        }
    }
}
