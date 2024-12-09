using EcommMarket.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommMarket.App.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService productService;
    public ProductsController(IProductService productService)
    {
        this.productService = productService;
    }

    public IActionResult ProductItem()
    {
        return View();
    }

    public async Task<IActionResult> ProductList()
    {
        var products = await productService.GetAllAsync();
        return View(products);
    }
}
