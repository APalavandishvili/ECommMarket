using EcommMarket.Application.Interfaces;
using ECommMarket.App.Models;
using EcommMarket.Application.Services;
using Microsoft.AspNetCore.Mvc;
using EcommMarket.Application.Dto;

namespace ECommMarket.App.Controllers;

[Route("Order")]
public class OrderController : Controller
{
    private readonly IOrderService orderService;
    public OrderController(IOrderService orderService)
    {
        this.orderService = orderService;
    }

    [Route("Admin")]
    public async Task<IActionResult> CmsOrders()
    {
        var orders = await orderService.GetAllAsync();

        var orderViewModel = orders.Select(x => new OrderViewModel()
        {
            Id = x.Id,
            Address = x.Address,
            City = x.City,
            Email = x.Email,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Timestamp = x.Timestamp,
            PhoneNumber = x.PhoneNumber,
            TransactionId = x.TransactionId,
            Products = x.Products.Select(o => new ProductViewModel()
            {
                Id = o.Id,
                Description = o.Description,
                Price = o.Price,
                Quantity = o.Quantity,
                ProductName = o.ProductName,
                Photos = o.Photos?.Select(p => new PhotoViewModel()
                {
                    PhotoName = p.PhotoName,
                    PhotoUrl = p.PhotoUrl,
                }).ToList(),
            }).ToList()
        }).ToList();

        return View("./Views/Cms/Orders/OrderList.cshtml", orderViewModel);
    }

    [Route("Admin/Order/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await orderService.GetByIdAsync(id);
        var orderViewModel = new OrderViewModel()
        {
            Id = order.Id,
            Address = order.Address,
            City = order.City,
            Email = order.Email,
            FirstName = order.FirstName,
            LastName = order.LastName,
            Timestamp = order.Timestamp,
            PhoneNumber = order.PhoneNumber,
            TransactionId = order.TransactionId,
            Products = order.Products.Select(o => new ProductViewModel()
            {
                Id = o.Id,
                Description = o.Description,
                Price = o.Price,
                Quantity = o.Quantity,
                ProductName = o.ProductName,
                Photos = o.Photos?.Select(p => new PhotoViewModel()
                {
                    PhotoName = p.PhotoName,
                    PhotoUrl = p.PhotoUrl,
                }).ToList(),
            }).ToList()
        };
        
        return View("./Views/Cms/Orders/OrderItem.cshtml", orderViewModel);
    }

    [Route("Admin/Order/Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await orderService.Delete(id);
        return RedirectToAction("CmsOrders");
    }
}
