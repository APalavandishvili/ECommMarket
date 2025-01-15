using ECommMarket.App.Models;
using EcommMarket.Application.Services;
using Microsoft.AspNetCore.Mvc;
using EcommMarket.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace ECommMarket.App.Controllers;


//[Route("[controller]")]
public class LoginController : Controller
{
    private readonly IUserService userService;
    public LoginController(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        return View("/Views/Cms/Login.cshtml");
    }

    public async Task<IActionResult> Login(LoginViewModel request)
    {
        var token = await userService.Login(new()
        {
            Password = request.Password,
            Username = request.Username
        });

        if (!token.IsNullOrEmpty())
        {
            Response.Cookies.Append("identifier", token);

            return RedirectToAction("CmsProducts", "Products");
        }
        else
        {
            return View("/Views/Products/ProductList.cshtml");
        }
    }
}
