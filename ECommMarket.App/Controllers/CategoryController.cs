using ECommMarket.App.Models;
using EcommMarket.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommMarket.App.Controllers;

public class CategoryController : Controller
{
    //public async Task<IActionResult> CmsProducts()
    //{
    //    var products = await productService.GetAllAsync();

    //    var productViewModel = products.Select(x => new ProductViewModel()
    //    {
    //        Id = x.Id,
    //        Description = x.Description,
    //        Price = x.Price,
    //        ProductName = x.ProductName,
    //        Quantity = x.Quantity,
    //        Photos = x.Photos!.Select(p => new PhotoViewModel()
    //        {
    //            PhotoUrl = p.PhotoUrl,
    //            PhotoName = p.PhotoName
    //        }).ToList(),
    //    }).ToList();
    //    return View("./Views/Cms/Products/ProductList.cshtml", productViewModel);
    //}

    //public async Task<IActionResult> AddProducts()
    //{
    //    var categories = await categoryService.GetAllAsync();

    //    ViewBag.Categories = categories.Select(x => new CategoryViewModel()
    //    {
    //        Id = x.Id,
    //        Name = x.Name
    //    }).ToList();

    //    return View("./Views/Cms/Products/AddProduct.cshtml");
    //}
}
