using Microsoft.Extensions.Caching.Memory;

namespace ECommMarket.Application.Cache;

public class CartMemoryCache
{
    public MemoryCache Cache { get; } = new MemoryCache(
        new MemoryCacheOptions
        {
            SizeLimit = 2048
        });
}
