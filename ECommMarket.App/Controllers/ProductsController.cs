using ECommMarket.App.Models;
using EcommMarket.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using EcommMarket.Application.Dto;
using ECommMarket.App.Extensions;
using ECommMarket.App.Filters;
using EcommMarket.Application;

namespace ECommMarket.App.Controllers;

[Route("Products")]
public class ProductsController : Controller
{
    private readonly IProductService productService;
    private readonly IPhotoService photoService;
    private readonly ICategoryService categoryService;
    public ProductsController(IProductService productService, IPhotoService photoService, ICategoryService categoryService)
    {
        this.productService = productService;
        this.categoryService = categoryService;
        this.photoService = photoService;
    }

    [Route("")]
    public async Task<IActionResult> Index(CategoryType type = 0)
    {
        var products = await productService.GetAllAsync(type);
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

    [Authorization]
    [Route("Admin")]
    public async Task<IActionResult> CmsProducts()
    {
        var products = await productService.GetAllAsync(CategoryType.All);
        
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

    [Authorization]
    [Route("Admin/Add")]
    public async Task<IActionResult> AddProducts()
    {
        var categories = await categoryService.GetAllAsync();

        ViewBag.Categories = categories.Select(x => new CategoryViewModel()
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();

        return View("./Views/Cms/Products/AddProduct.cshtml", new ProductViewModel());
    }

    [Authorization]
    [Route("Admin/Products/Edit")]
    public async Task<IActionResult> EditProducts(int id)
    {
        var categories = await categoryService.GetAllAsync();
        var product = await productService.GetByIdAsync(id);
        var productItemViewModel = new ProductViewModel()
        {
            Id = product.Id,
            Category = new() { Id = product.Category.Id, Name = product.Category.Name },
            Description = product.Description,
            Price = product.Price,
            ProductName = product.ProductName,
            Quantity = product.Quantity,
            Photos = product.Photos!.Select(p => new PhotoViewModel()
            {
                Id = p.Id,
                PhotoUrl = p.PhotoUrl,
                PhotoName = p.PhotoName
            }).ToList()
        };

        ViewBag.Categories = categories.Select(x => new CategoryViewModel()
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();

        return View("./Views/Cms/Products/EditProduct.cshtml", productItemViewModel);
    }

    [Route("{id}")]
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
            Category = new() { Id = product.Category.Id, Name = product.Category.Name },
            Photos = product.Photos!.Select(p => new PhotoViewModel()
            {
                PhotoUrl = p.PhotoUrl,
                PhotoName = p.PhotoName
            }).ToList()
        };
        return View("./Views/Products/ProductItem.cshtml", productItemViewModel);
    }

    [Authorization]
    [Route("Admin/Products/Add")]
    public async Task<IActionResult> Add(ProductViewModel model)
    {
        var newPhotoList = await PhotoExtension.UploadPhotos(model.UploadedPhotos);

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
            Category = new() { Id = model.Category.Id, Name = model.Category.Name },
        };

        await productService.AddAsync(productDto);

        return RedirectToAction("CmsProducts");
    }

    [Authorization]
    [Route("Admin/Edit")]
    public async Task<IActionResult> Edit(int id, ProductViewModel productViewModel)
    {
        var product = await productService.GetByIdAsync(id);
        var newPhotos = await PhotoExtension.UploadPhotos(productViewModel.UploadedPhotos);
        await productService.Update(new ProductDto()
        {
            Id = productViewModel.Id,
            Description = productViewModel.Description,
            Price = productViewModel.Price,
            ProductName = productViewModel.ProductName,
            Quantity = productViewModel.Quantity,
            Category = new() { Id = productViewModel.Category.Id, Name = productViewModel.Category.Name },
            Photos = newPhotos.Select(p => new PhotoDto()
            {
                PhotoUrl = p.PhotoUrl,
                PhotoName = p.PhotoName
            }).ToList()
        });

        return RedirectToAction("CmsProducts");
    }

    [Authorization]
    [Route("Admin/Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await productService.Delete(id);
        
        return RedirectToAction("CmsProducts");
    }

    [Authorization]
    [Route("Admin/Delete/Photo")]
    public async Task<IActionResult> DeletePhoto(int photoId,int id)
    {
        var photo = await photoService.GetById(photoId);

        await PhotoExtension.DeletePhoto(photo.PhotoName);

        await photoService.Delete(photo.Id);

        return RedirectToAction("EditProducts", new { id = id});
    }
}
