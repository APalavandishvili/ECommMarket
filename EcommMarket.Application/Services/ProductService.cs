using AutoMapper;
using EcommMarket.Application.Interfaces;
using EcommMarket.Application.ViewModels;
using ECommMarket.Persistence.Interface;

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
        return null;
    }

    public async Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ProductViewModel>> GetAllAsync()
    {
        var products = await productRepository.GetAllAsync();
        return mapper.Map<List<ProductViewModel>>(products);
    }

    public async Task<ProductViewModel> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task Update(ProductViewModel entity)
    {
        throw new NotImplementedException();
    }
}
