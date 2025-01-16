using EcommMarket.Application.Dto;
using EcommMarket.Application.Interfaces;
using ECommMarket.App.Extensions;
using ECommMarket.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommMarket.App.Controllers;

public class NewsController : Controller
{
    private readonly INewsService newsService;
    public NewsController(INewsService newsService)
    {
        this.newsService = newsService;
    }
    public async Task<IActionResult> Index()
    {
        var news = await newsService.GetAllAsync();
        var newsViewModel = news.Select(x => new NewsViewModel()
        {
            Id = x.Id,
            Title = x.Title,
            Article = x.Article,
            Details = x.Details,
            Timestamp = x.Timestamp,
        }).ToList();
        return View("./Views/News/News.cshtml", newsViewModel);
    }

    public async Task<IActionResult> CmsIndex()
    {
        var news = await newsService.GetAllAsync();
        var newsViewModel = news.Select(x => new NewsViewModel()
        {
            Id = x.Id,
            Title = x.Title,
            Article = x.Article,
            Details = x.Details,
            Timestamp = x.Timestamp,
        }).ToList();
        return View("./Views/Cms/News/NewsList.cshtml", newsViewModel);
    }

    public async Task<IActionResult> NewsItem(int id)
    {
        var news = await newsService.GetByIdAsync(id);
        var newsItemViewModel = new NewsViewModel()
        {
            Id = news.Id,
            Title = news.Title,
            Article = news.Article,
            Details = news.Details,
            Timestamp = news.Timestamp,
        };
        return View("./Views/News/NewsItem.cshtml", newsItemViewModel);
    }

    public async Task<IActionResult> AddNews()
    {
        return View("./Views/Cms/News/AddNews.cshtml");
    }

    public async Task<IActionResult> Add(NewsViewModel model)
    {
        var addedPhotos = await PhotoExtension.UploadPhotos([model.Photo]);

        await newsService.AddAsync( new NewsDto()
        {
            Title = model.Title,
            Article = model.Article,
            Details = model.Details,
            Timestamp = model.Timestamp,
            Photos = addedPhotos.Select(addedPhotos => new PhotoDto()
            {
                PhotoName = addedPhotos.PhotoName,
                PhotoUrl = addedPhotos.PhotoUrl,
            }).ToList(),
        });
        return RedirectToAction("CmsIndex");
    }
}
