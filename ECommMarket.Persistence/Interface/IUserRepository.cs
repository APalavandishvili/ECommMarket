using ECommMarket.Domain.Entities;

namespace ECommMarket.Persistence.Interface;

public interface IUserRepository
{
    Task<string> Login(User request);
}
