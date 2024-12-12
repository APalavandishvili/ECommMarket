using EcommMarket.Application.Interfaces;
using EcommMarket.Application.Dto;
using ECommMarket.App.Models;
using ECommMarket.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ECommMarket.App.Controllers;

public class PaymentController : Controller
{
    private readonly IMemoryCache memoryCache;
    private readonly IProductService productService;
    private readonly IOrderService orderService;
    public PaymentController(IOrderService orderService, IProductService productService, IMemoryCache memoryCache)
    {
        this.orderService = orderService;
        this.productService = productService;
        this.memoryCache = memoryCache;
    }

    public async Task<IActionResult> Index()
    {
        var cartIdentifier = Request.Cookies["cartIdentifier"];

        if (memoryCache.TryGetValue(cartIdentifier, out List<int>? cacheValue))
        {
            List<ProductDto> products = await productService.GetAllByIdAsync(cacheValue);
            List<ProductViewModel> productsViewModel = products.Select(x => new ProductViewModel()
            {
                Description = x.Description,
                Id = x.Id,
                Price = x.Price,
                ProductName = x.ProductName,
                Quantity = x.Quantity,
                Photos = new()
                {
                    new() { PhotoName =  x.Photos!.FirstOrDefault()!.PhotoName, PhotoUrl = x.Photos!.FirstOrDefault()!.PhotoUrl }
                }
            }).ToList();
            ViewBag.Products = productsViewModel;

            return View("/Views/Payment/Payment.cshtml");
        }
        return RedirectToAction("CartItems", "Cart");
    }
    public async Task<IActionResult> AddOrder(OrderViewModel model)
    {
        var cartIdentifier = Request.Cookies["cartIdentifier"];

        if (memoryCache.TryGetValue(cartIdentifier, out List<int>? cacheValue))
        {
            List<ProductDto> products = await productService.GetAllByIdAsync(cacheValue);

            var orderDto = new OrderDto()
            {
                Address = model.Address,
                City = model.City,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                TransactionId = Guid.NewGuid().ToString(),
                //Products = model.Products.Select(o => new ProductDto()
                //{
                //    Id = o.Id,
                //    Description = o.Description,
                //    Price = o.Price,
                //    Quantity = o.Quantity,
                //    ProductName = o.ProductName,
                //    Photos = o.Photos?.Select(p => new PhotoDto()
                //    {
                //        PhotoName = p.PhotoName,
                //        PhotoUrl = p.PhotoUrl,
                //    }).ToList(),
                //}).ToList()
                Products = products,
            };

            OrderDto savedOrder = await orderService.AddAsync(orderDto);
            OrderViewModel viewModel = new OrderViewModel()
            {
                Products = savedOrder.Products.Select(x => new ProductViewModel()
                {
                    Description = x.Description,
                    Id = x.Id,
                    Price = x.Price,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    Photos = new()
                {
                    new() { PhotoName =  x.Photos!.FirstOrDefault()!.PhotoName, PhotoUrl = x.Photos!.FirstOrDefault()!.PhotoUrl }
                }
                }).ToList(),
                Address = savedOrder.Address,
                City = savedOrder.City,
                Email = savedOrder.Email,
                FirstName = savedOrder.FirstName,
                LastName = savedOrder.LastName,
                PhoneNumber = savedOrder.PhoneNumber,
                TransactionId = savedOrder.TransactionId,
                Timestamp = savedOrder.Timestamp,


            };
            return View("/Views/Order/OrderResult.cshtml" , viewModel);
        }
        else
        {
            return View(model);
        }
    }
}
