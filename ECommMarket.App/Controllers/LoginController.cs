using ECommMarket.App.Models;
using EcommMarket.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommMarket.App.Controllers;

public class LoginController : Controller
{

    public async Task<IActionResult> Index(LoginViewModel request)
    {
        return View();
    }
}
