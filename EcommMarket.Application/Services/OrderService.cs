using EcommMarket.Application.Interfaces;
using EcommMarket.Application.Dto;
using ECommMarket.Persistence.Interface;

namespace EcommMarket.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository orderRepository;
    private readonly IProductRepository productRepository;
    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        this.orderRepository = orderRepository;
        this.productRepository = productRepository;
    }

    public async Task<OrderDto> AddAsync(OrderDto entity)
    {
        var products = await productRepository.GetAllByIdAsync(entity.Products.Select(x => x.Id).ToList());
        
        var orderId = await orderRepository.AddAsync(new()
        {
            Address = entity.Address,
            City = entity.City,
            Email = entity.Email,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Timestamp = DateTime.Now,
            PhoneNumber = entity.PhoneNumber,
            TransactionId = entity.TransactionId,
            Items = products
        });

        return orderId != 0 ? entity : new();
    }

    public async Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<OrderDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<OrderDto>> GetAllByIdAsync(List<int> productIds)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderDto> GetByIdAsync(int id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        return new()
        {
            Address = order.Address,
            City = order.City,
            Email = order.Email,
            FirstName = order.FirstName,
            LastName = order.LastName,
            Timestamp = order.Timestamp,
            PhoneNumber = order.PhoneNumber,
            TransactionId = order.TransactionId,
            Products = order.Items.Any() ? order.Items.Select(o => new ProductDto()
            {
                Id = o.Id,
                Description = o.Description,
                Price = o.Price,
                Quantity = o.Quantity,
                ProductName = o.ProductName,
                Photos = o.Photos?.Select(p => new PhotoDto()
                {
                    PhotoName = p.PhotoName,
                    PhotoUrl = p.PhotoUrl,
                }).ToList(),
            }).ToList() : new()
        };
    }

    public async Task Update(OrderDto entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdatePhotos(List<PhotoDto> photos)
    {
        throw new NotImplementedException();
    }
}
