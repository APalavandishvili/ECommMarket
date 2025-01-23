using EcommMarket.Application.Interfaces;
using EcommMarket.Application.Services;
using ECommMarket.App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ECommMarket.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService)
        {
            _logger = logger;
            this.productService = productService;
            this.categoryService = categoryService;
        }

        [Route("")]
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

            return View(productViewModel);
        }

        [Route("Services")]
        public async Task<IActionResult> Services()
        {
            return View("/Views/Home/Services.cshtml");
        }

        [Route("Partners")]
        public async Task<IActionResult> Partners()
        {
            return View("/Views/Home/Partners.cshtml");
        }
    }
}
