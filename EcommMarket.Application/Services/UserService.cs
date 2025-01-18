using EcommMarket.Application.Dto;
using EcommMarket.Application.Interfaces;
using ECommMarket.Persistence.Interface;

namespace EcommMarket.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    public async Task<string> Login(LoginDto request)
    {
        return await userRepository.Login(new ECommMarket.Domain.Entities.User()
        {
            Email = request.Username,
            Password = request.Password
        });
    }
}
