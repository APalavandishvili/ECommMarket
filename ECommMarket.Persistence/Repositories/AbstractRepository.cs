using ECommMarket.Persistence.Data;

namespace ECommMarket.Persistence.Repositories
{
    public class AbstractRepository
    {
        protected readonly MarketDbContext context;
        public AbstractRepository(MarketDbContext context)
        {
            this.context = context;
        }
    }
}
