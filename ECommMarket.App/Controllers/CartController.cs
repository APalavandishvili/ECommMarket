using EcommMarket.Application.Interfaces;
using EcommMarket.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ECommMarket.App.Models;

namespace ECommMarket.App.Controllers;

[Route("Cart")]
public class CartController : Controller
{
    private readonly IMemoryCache memoryCache;
    private readonly IProductService productService;
    public CartController(IMemoryCache memoryCache, IProductService productService)
    {
        this.memoryCache = memoryCache;
        this.productService = productService;
    }

    [Route("Add")]
    public IActionResult AddCartItem(int id, int quantity, bool isProductList)
    {
        AddOrUpdateCartList(id);
        return isProductList ? RedirectToAction("Index", "Products") : RedirectToAction("ProductItem", "Products", new {id = id});
    }

    [Route("Remove")]
    public IActionResult RemoveCartItem(int id)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
               .SetSlidingExpiration(TimeSpan.FromHours(3));

        var cartIdentifier = Request.Cookies["cartIdentifier"];

        if (memoryCache.TryGetValue(cartIdentifier, out List<int>? cacheValue))
        {
            cacheValue.Remove(id);

            memoryCache.Set(cartIdentifier, cacheValue, cacheEntryOptions);
        }

        return RedirectToAction("CartItems", "Cart");
    }

    [Route("Items")]
    public async Task<IActionResult> CartItems()
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
               .SetSlidingExpiration(TimeSpan.FromHours(3));
        
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
            return View("./Views/Cart/CartItems.cshtml", productsViewModel);
        }
        else
        {
            return RedirectToAction("Product", "Index");
        }
        
    }

    private void AddOrUpdateCartList(int productId)
    {
        var cartIdentifier = Request.Cookies["cartIdentifier"];
        if (cartIdentifier == null)
        {
            cartIdentifier = Guid.NewGuid().ToString();

            Response.Cookies.Append("cartIdentifier", cartIdentifier);
        }


        var cacheEntryOptions = new MemoryCacheEntryOptions()
               .SetSlidingExpiration(TimeSpan.FromHours(3));

        if (!memoryCache.TryGetValue(cartIdentifier, out List<int>? cacheValue))
        {
            cacheValue = new List<int>() { productId };

            memoryCache.Set(cartIdentifier, cacheValue, cacheEntryOptions);
        }
        else
        {
            cacheValue.Add(productId);

            memoryCache.Set(cartIdentifier, cacheValue, cacheEntryOptions);
        }
    }
}
