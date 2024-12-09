using AutoMapper;
using EcommMarket.Application.Interfaces;
using EcommMarket.Application.ViewModels;
using ECommMarket.Persistence.Interface;
using Microsoft.VisualBasic;

namespace EcommMarket.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;
    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async Task<ProductViewModel> AddAsync(ProductViewModel entity)
    {
        await productRepository.AddAsync(new()
        {
            Description = entity.Description,
            Price = entity.Price,
            ProductName = entity.ProductName,
            Quantity = entity.Quantity,
            Timestamp = DateTime.Now,
        });

        return entity;
    }

    public async Task Delete(int id)
    {
        await productRepository.Delete(id);
    }

    public async Task<List<ProductViewModel>> GetAllAsync()
    {
        IEnumerable<ECommMarket.Domain.Entities.Product> enumerable = await productRepository.GetAllAsync();
        var products = enumerable;
        return [];
    }

    public async Task<ProductViewModel> GetByIdAsync(int id)
    {
        ECommMarket.Domain.Entities.Product product = await productRepository.GetByIdAsync(id);
        return mapper.Map<ProductViewModel>(product);
    }

    public async Task Update(ProductViewModel entity)
    {
        ECommMarket.Domain.Entities.Product product = await productRepository.GetByIdAsync(entity.Id);
        product.Quantity = entity.Quantity;
        product.Price = entity.Price;
        product.Description = entity.Description;
        product.ProductName = entity.ProductName;
        product.UpdateTimestamp = DateTime.Now;

        await productRepository.Update(product);
    }

    public async Task UpdatePhotos(int productId, List<PhotoViewModel> photos)
    {
        ECommMarket.Domain.Entities.Product product = await productRepository.GetByIdAsync(productId);
        foreach (PhotoViewModel photo in photos)
        {
            product.Photos.Add(new()
            {
                PhotoName = photo.PhotoName,
                PhotoUrl = photo.PhotoUrl,
            });
        }

        await productRepository.Update(product);
    }

    public Task UpdatePhotos(List<PhotoViewModel> photos)
    {
        throw new NotImplementedException();
    }
}
