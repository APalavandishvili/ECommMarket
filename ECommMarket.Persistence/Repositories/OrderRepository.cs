using ECommMarket.Domain.Entities;
using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;

namespace ECommMarket.Persistence.Repositories;

public class OrderRepository(MarketDbContext context) : RepositoryBase<Order, int>(context), IOrderRepository
{
}
