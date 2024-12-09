using ECommMarket.App.Models;
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

    public async Task<IActionResult> Index()
    {
        var products = await productService.GetAllAsync();
        var c = products.Select(x => new ProductViewModel()
        {
            Description = x.Description,
            Price = x.Price,
            ProductName = x.ProductName,
            Quantity = x.Quantity,
            Photos = x.Photos!.Select(p => new PhotoViewModel()
            {
                PhotoUrl = p.PhotoUrl,
                PhotoName = p.PhotoName
            }).ToList(),
        }).ToList();
        return View("./Views/Products/ProductList.cshtml", c);
    }
}
