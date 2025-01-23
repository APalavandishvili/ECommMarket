using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ECommMarket.App.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class AuthorizationAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Cookies["identifier"];
        if(token is not null)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            if (jwtSecurityToken != null && jwtSecurityToken.ValidTo <= DateTime.UtcNow)
            {
                context.Result = new BadRequestObjectResult("Forbidden");
            }
        }
        else
        {
            context.Result = new BadRequestObjectResult("Forbidden");
        }
    }
}
