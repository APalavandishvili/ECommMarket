using ECommMarket.Domain.Entities;
using ECommMarket.Persistence.Data;
using ECommMarket.Persistence.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommMarket.Persistence.Repositories;

public class UserRepository(MarketDbContext context) : IUserRepository
{

    public async Task<string> Login(User request)
    {
        var user = await context.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
        if(user is null)
        {
            return string.Empty;
        }

        var isPasswordCorrect = VerifyPassword(user.Email, HashPassword(request.Email, request.Password));

        if(!isPasswordCorrect)
        {
            return string.Empty;
        }

        var secret = "vxw(}@wEU9'|z]>BlH=qexx-NM62\"6fO&}1Kf_!uH<q)9";
        byte[] byteArray = Encoding.UTF8.GetBytes(secret);

        var handler = new JwtSecurityTokenHandler();
        
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(byteArray),
            SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(1),
            Subject = GenerateClaims(user),
        };
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static string HashPassword(string username,string password)
    {
        // Create an instance of PasswordHasher, which uses Argon2 by default
        var passwordHasher = new PasswordHasher<string>();

        // Hash the password
        string hashedPassword = passwordHasher.HashPassword(username, password);
        return hashedPassword;
    }

    private static bool VerifyPassword(string username, string password)
    {
        // Create an instance of PasswordHasher, which uses Argon2 by default
        var passwordHasher = new PasswordHasher<string>();

        // Hash the password
        string hashedPassword = passwordHasher.HashPassword(username, password);

        // Verify password
        return passwordHasher.VerifyHashedPassword(username, hashedPassword, password) == PasswordVerificationResult.Success;
    }
    
    private static ClaimsIdentity GenerateClaims(User user)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

        return ci;
    }
}
