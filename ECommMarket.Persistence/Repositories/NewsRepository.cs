using ECommMarket.Domain.Entities;
using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;

namespace ECommMarket.Persistence.Repositories;

public class NewsRepository(MarketDbContext context) : RepositoryBase<News, int>(context), INewsRepository
{
}
