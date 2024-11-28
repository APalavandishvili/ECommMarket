using ECommMarket.Persistence.Data;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace ECommMarket.Persistence;

public static class Seeder
{
    public static Task Seed(this MarketDbContext db)
    {
        db.Photos.Add(new()
        {
            PhotoName = "Product 1 name",
            PhotoUrl = "shoe-1.png",
            Timestamp = DateTime.Now
        });

        db.Products.Add(new()
        {
            Description = "First Product Description",
            Price = 100,
            ProductName = "First Product Name",
            Quantity = 10,
            Timestamp = DateTime.Now,
              
        });
        return Task.CompletedTask;
    }
}
