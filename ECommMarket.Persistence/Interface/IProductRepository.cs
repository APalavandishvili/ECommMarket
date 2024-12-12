using ECommMarket.Domain.Entities;

namespace ECommMarket.Persistence.Interface;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetAllByIdAsync(List<int> productIds);
}
