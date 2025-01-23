using ECommMarket.App.Models;
using Microsoft.AspNetCore.Mvc;
using EcommMarket.Application.Interfaces;
using ECommMarket.App.Filters;

namespace ECommMarket.App.Controllers;

[Route("Category")]
public class CategoryController : Controller
{
    private readonly ICategoryService categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    [Authorization]
    [Route("Admin")]
    public async Task<IActionResult> CmsCategories()
    {
        var categories = await categoryService.GetAllAsync();
        var model = categories.Select(x => new CategoryViewModel()
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();

        return View("./Views/Cms/Categories/CategoryList.cshtml", model);
    }

    [Authorization]
    [Route("Admin/Add")]
    public async Task<IActionResult> Add()
    {
        return View("./Views/Cms/Categories/AddCategory.cshtml");
    }

    [Authorization]
    [Route("Admin/Categories/Add")]
    public async Task<IActionResult> AddCategory(CategoryViewModel model)
    {
        await categoryService.AddAsync(new EcommMarket.Application.Dto.CategoryDto()
        {
            Name = model.Name,
        });

        return RedirectToAction("CmsCategories");
    }
}
