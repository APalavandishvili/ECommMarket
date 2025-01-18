using ECommMarket.Domain.Entities;
using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ECommMarket.Persistence.Repositories;

public class NewsRepository(MarketDbContext context) : INewsRepository
{
    public async Task<int> AddAsync(News entity)
    {
        await context.News.AddAsync(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task Delete(int id)
    {
        News? news = await context.News.Include(x => x.Photos).FirstOrDefaultAsync(p => p.Id == id);
        if (news is not null)
        {
            context.News.Remove(news);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<News>> GetAllAsync()
    {
        return await context.News.Include(x => x.Photos).ToListAsync();
    }

    public async Task<News> GetByIdAsync(int id)
    {
        return await context.News.Include(x => x.Photos).FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task Update(News entity)
    {
        News? news = await context.News.Include(x => x.Photos).FirstOrDefaultAsync(p => p.Id == entity.Id);
        if (news is null)
        {
            return;
        }

        news.Title = entity.Title;
        news.Details = entity.Details;
        news.Article = entity.Article;
        news.AuthorId = entity.AuthorId;
        news.UpdateTimestamp = DateTime.Now;
        news.Photos = entity.Photos;
        
        await context.SaveChangesAsync();
    }
}
