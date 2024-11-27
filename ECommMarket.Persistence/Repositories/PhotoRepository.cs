using ECommMarket.Domain.Entities;
using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;

namespace ECommMarket.Persistence.Repositories;

public class PhotoRepository(MarketDbContext context) : RepositoryBase<Photo, int>(context), IPhotoRepository
{
}
