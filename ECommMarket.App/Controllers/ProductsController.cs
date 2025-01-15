using ECommMarket.App.Models;
using EcommMarket.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using EcommMarket.Application.Dto;
using ECommMarket.Domain.Entities;

namespace ECommMarket.App.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService productService;
    private readonly ICategoryService categoryService;
    public ProductsController(IProductService productService, ICategoryService categoryService)
    {
        this.productService = productService;
        this.categoryService = categoryService;
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

    public async Task<IActionResult> CmsProducts()
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
        return View("./Views/Cms/Products/ProductList.cshtml", productViewModel);
    }

    public async Task<IActionResult> AddProducts()
    {
        var categories = await categoryService.GetAllAsync();

        ViewBag.Categories = categories.Select(x => new CategoryViewModel()
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();

        return View("./Views/Cms/Products/AddProduct.cshtml");
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


    public async Task<IActionResult> Add(ProductViewModel model)
    {
        var newPhotoList = await UploadPhotos(model.UploadedPhotos);

        var productDto = new ProductDto()
        {
            Description = model.Description,
            Price = model.Price,
            ProductName = model.ProductName,
            Quantity = model.Quantity,
            Photos = newPhotoList.Select(p => new PhotoDto()
            {
                PhotoName = p.PhotoName,
                PhotoUrl = p.PhotoUrl
            }).ToList(),
            Category = model.CategoryId
        };

        var product = await productService.AddAsync(productDto);

        return RedirectToAction("CmsProducts");
    }

    private static async Task<List<PhotoViewModel>> UploadPhotos(List<IFormFile> photos)
    {
        List<PhotoViewModel> newPhotos = new List<PhotoViewModel>();

        foreach (var photo in photos)
        {
            if (photo.Length > 0)
            {
                // Generate a unique filename for each photo to avoid conflicts
                var fileName = Path.GetFileName(photo.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", @"images\products", fileName);

                // Ensure the folder exists
                var directory = Path.GetDirectoryName(path);
                
                // Save the file to the server
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }
                newPhotos.Add(new PhotoViewModel()
                {
                    PhotoUrl = @"/images/products/" + fileName,
                    PhotoName = fileName
                });
            }
        }

        return newPhotos;
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await productService.GetByIdAsync(id);
        await productService.Update(new EcommMarket.Application.Dto.ProductDto()
        {
            Id = product.Id,
            Description = product.Description,
            Price = product.Price,
            ProductName = product.ProductName,
            Quantity = product.Quantity,
            Photos = product.Photos!.Select(p => new PhotoDto()
            {
                PhotoUrl = p.PhotoUrl,
                PhotoName = p.PhotoName
            }).ToList()
        });

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        await productService.Delete(id);
        
        return RedirectToAction("Index");
    }
}
