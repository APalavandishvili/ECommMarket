using EcommMarket.Application.Dto;
using EcommMarket.Application.Interfaces;
using EcommMarket.Application.Services;
using ECommMarket.App.Extensions;
using ECommMarket.App.Filters;
using ECommMarket.App.Models;
using ECommMarket.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommMarket.App.Controllers;

[Route("News")]
public class NewsController : Controller
{
    private readonly INewsService newsService;
    private readonly IPhotoService photoService;
    public NewsController(INewsService newsService, IPhotoService photoService)
    {
        this.newsService = newsService;
        this.photoService = photoService;
    }

    [Route("")]
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
            Photo = new PhotoViewModel()
            {
                PhotoName = x.Photos.PhotoName,
                PhotoUrl = x.Photos.PhotoUrl,
            },
        }).ToList();
        return View("./Views/News/News.cshtml", newsViewModel);
    }

    [Authorization]
    [Route("Admin/News")]
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
            Photo = new PhotoViewModel()
            {
                PhotoName = x.Photos.PhotoName,
                PhotoUrl = x.Photos.PhotoUrl,
            },
        }).ToList();
        return View("./Views/Cms/News/NewsList.cshtml", newsViewModel);
    }

    [Route("{id}")]
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
            Photo = new PhotoViewModel()
            {
                PhotoName = news.Photos.PhotoName,
                PhotoUrl = news.Photos.PhotoUrl,
            },
        };
        return View("./Views/News/NewsItem.cshtml", newsItemViewModel);
    }

    [Authorization]
    [Route("Admin/Add")]
    public async Task<IActionResult> AddNews()
    {
        return View("./Views/Cms/News/AddNews.cshtml");
    }

    [Authorization]
    [Route("Admin/News/Add")]
    public async Task<IActionResult> Add(NewsViewModel model)
    {
        var addedPhotos = await PhotoExtension.UploadPhotos([model.UploadedPhoto]);

        await newsService.AddAsync(new NewsDto()
        {
            Title = model.Title,
            Article = model.Article,
            Details = model.Details,
            Timestamp = model.Timestamp,
            Photos = new PhotoDto()
            {
                PhotoName = addedPhotos.First().PhotoName,
                PhotoUrl = addedPhotos.First().PhotoUrl,
            },
        });
        return RedirectToAction("CmsIndex");
    }

    [Authorization]
    [Route("Admin/Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        var news = await newsService.GetByIdAsync(id);
        var newsItemViewModel = new NewsViewModel()
        {
            Id = news.Id,
            Title = news.Title,
            Article = news.Article,
            Details = news.Details,
            Timestamp = news.Timestamp,
            Photo = new PhotoViewModel()
            {
                PhotoName = news.Photos.PhotoName,
                PhotoUrl = news.Photos.PhotoUrl,
            },
        };

        return View("./Views/Cms/News/EditNews.cshtml", newsItemViewModel);
    }

    [Authorization]
    [Route("Admin/Update")]
    public async Task<IActionResult> Update(int id, NewsViewModel model)
    {
        var addedPhotos = await PhotoExtension.UploadPhotos([model.UploadedPhoto]);
        var news = await newsService.GetByIdAsync(model.Id);

        news.Title = model.Title;
        news.Article = model.Article;
        news.Details = model.Details;
        news.Timestamp = model.Timestamp;
        news.Photos = new PhotoDto()
        {
            PhotoName = addedPhotos.First().PhotoName,
            PhotoUrl = addedPhotos.First().PhotoUrl,
        };

        await newsService.Update(news);

        return RedirectToAction("CmsIndex");
    }

    [Authorization]
    [Route("Admin/News/Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await newsService.Delete(id);

        return RedirectToAction("CmsIndex");
    }

    [Authorization]
    [Route("Admin/DeletePhoto")]
    public async Task DeletePhoto(string photoName)
    {
        await PhotoExtension.DeletePhoto(photoName);
    }
}
