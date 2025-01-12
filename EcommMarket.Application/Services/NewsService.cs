using EcommMarket.Application.Dto;
using EcommMarket.Application.Interfaces;
using ECommMarket.Domain.Entities;
using ECommMarket.Persistence.Interface;

namespace EcommMarket.Application.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository newsRepository;
        public NewsService(INewsRepository newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public async Task<NewsDto> AddAsync(NewsDto entity)
        {
            await newsRepository.AddAsync(new()
            {
                Title = entity.Title,
                Article = entity.Article,
                Details = entity.Details,
                Timestamp = DateTime.Now,
            });
            return entity;
        }

        public async Task Delete(int id)
        {
            await newsRepository.Delete(id);
        }

        public async Task<List<NewsDto>> GetAllAsync()
        {
            IEnumerable<News> news = await newsRepository.GetAllAsync();
            List<NewsDto> result = news.Select(x => new NewsDto()
            {
                Id = x.Id,
                Title = x.Title,
                Article = x.Article,
                Details = x.Details,
                Timestamp = x.Timestamp,
            }).ToList();

            return result;
        }
        public async Task<NewsDto> GetByIdAsync(int id)
        {
            News news = await newsRepository.GetByIdAsync(id);
            NewsDto result = new NewsDto()
            {
                Id = news.Id,
                Title = news.Title,
                Article = news.Article,
                Details = news.Details,
                Timestamp = news.Timestamp,
            };
            return result;
        }

        public async Task Update(NewsDto entity)
        {
            News news = await newsRepository.GetByIdAsync(entity.Id);
            news.Title = entity.Title;
            news.Article = entity.Article;
            news.Details = entity.Details;
            news.Timestamp = entity.Timestamp;

            await newsRepository.Update(news);
        }
    }
}
