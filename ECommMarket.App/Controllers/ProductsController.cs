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

    public async Task<IActionResult> Index()
    {
        var products = await productService.GetAllAsync();
        var productViewModel = products.Select(x => new ProductViewModel()
        {
            Id = x.Id,
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
        return View("./Views/Products/ProductList.cshtml", productViewModel);
    }

    public async Task<IActionResult> ProductItem(int id)
    {
        var product = await productService.GetByIdAsync(id);
        var productItemViewModel = new ProductViewModel()
        {
            Id = product.Id,
            Description = product.Description,
            Price = product.Price,
            ProductName = product.ProductName,
            Quantity = product.Quantity,
            Photos = product.Photos!.Select(p => new PhotoViewModel()
            {
                PhotoUrl = p.PhotoUrl,
                PhotoName = p.PhotoName
            }).ToList()
        };
        return View("./Views/Products/ProductItem.cshtml", productItemViewModel);
    }
}
