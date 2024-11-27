using ECommMarket.Domain.Entities;
using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;

namespace ECommMarket.Persistence.Repositories;

public class CartRepository(MarketDbContext context) : RepositoryBase<Cart, int>(context), ICartRepository
{
}
