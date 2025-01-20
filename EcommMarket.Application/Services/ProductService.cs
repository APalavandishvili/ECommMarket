using AutoMapper;
using EcommMarket.Application.Interfaces;
using EcommMarket.Application.Dto;
using ECommMarket.Persistence.Interface;
using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;
using ECommMarket.Domain.Entities;

namespace EcommMarket.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;
    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    public async Task<ProductDto> AddAsync(ProductDto entity)
    {
        var category = await categoryRepository.GetByIdAsync(entity.Category.Id);

        await productRepository.AddAsync(new()
        {
            Description = entity.Description,
            Price = entity.Price,
            ProductName = entity.ProductName,
            Quantity = entity.Quantity,
            Timestamp = DateTime.Now,
            Photos = entity.Photos!.Select(p => new Photo()
            {
                Id = p.Id,
                PhotoName = p.PhotoName,
                PhotoUrl = p.PhotoUrl,
            }).ToList(),
            Category = category
        });

        return entity;
    }

    public async Task Delete(int id)
    {
        await productRepository.Delete(id);
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        IEnumerable<Product> enumerable = await productRepository.GetAllAsync();
        List<ProductDto> products = enumerable.Select(x => new ProductDto()
        {
            Id = x.Id,
            Description = x.Description,
            ProductName = x.ProductName,
            Quantity = x.Quantity,
            Price = x.Price,
            Category = new() { Id = x.Category.Id, Name = x.Category.Name },
            Photos = x.Photos?.Select(p => new PhotoDto()
            {
                Id = p.Id,
                PhotoName = p.PhotoName,
                PhotoUrl = p.PhotoUrl,
            }).ToList(),
        }).ToList();

        return products;
    }

    public async Task<List<ProductDto>> GetAllByIdAsync(List<int> productIds)
    {
        List<Product> enumerable = await productRepository.GetAllByIdAsync(productIds);
        List<ProductDto> products = enumerable.Select(x => new ProductDto()
        {
            Id = x.Id,
            Description = x.Description,
            ProductName = x.ProductName,
            Quantity = x.Quantity,
            Price = x.Price,
            Category = new() { Id = x.Category.Id, Name = x.Category.Name },
            Photos = x.Photos?.Select(p => new PhotoDto()
            {
                Id = p.Id,
                PhotoName = p.PhotoName,
                PhotoUrl = p.PhotoUrl,
            }).ToList(),
        }).ToList();

        return products;
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        ECommMarket.Domain.Entities.Product product = await productRepository.GetByIdAsync(id);
        return new ProductDto()
        {
            Id = product.Id,
            Description = product.Description,
            ProductName = product.ProductName,
            Quantity = product.Quantity,
            Price = product.Price,
            Category = new() { Id = product.Category.Id, Name = product.Category.Name },
            Photos = product.Photos!.Select(p => new PhotoDto()
            {
                Id = p.Id,
                PhotoName = p.PhotoName,
                PhotoUrl = p.PhotoUrl,
            }).ToList()
        };
    }

    public async Task Update(ProductDto entity)
    {
        Product product = await productRepository.GetByIdAsync(entity.Id);
        var category = await categoryRepository.GetByIdAsync(entity.Category.Id);

        product.Quantity = entity.Quantity;
        product.Price = entity.Price;
        product.Description = entity.Description;
        product.ProductName = entity.ProductName;
        product.UpdateTimestamp = DateTime.Now;
        product.Category = category;
        foreach(var photo in entity.Photos)
        {
            product.Photos.Add(new Photo()
            {
                PhotoName = photo.PhotoName,
                PhotoUrl = photo.PhotoUrl
            });
        }

        await productRepository.Update(product);
    }

    public async Task UpdatePhotos(int productId, List<PhotoDto> photos)
    {
        ECommMarket.Domain.Entities.Product product = await productRepository.GetByIdAsync(productId);
        foreach (PhotoDto photo in photos)
        {
            product.Photos.Add(new()
            {
                PhotoName = photo.PhotoName,
                PhotoUrl = photo.PhotoUrl,
            });
        }

        await productRepository.Update(product);
    }

    public Task UpdatePhotos(List<PhotoDto> photos)
    {
        throw new NotImplementedException();
    }
}
