using ECommMarket.Domain.Entities;
using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;

namespace ECommMarket.Persistence.Repositories;

public class ProductRepository(MarketDbContext context) : RepositoryBase<Product, int>(context) ,IProductRepository
{
}
