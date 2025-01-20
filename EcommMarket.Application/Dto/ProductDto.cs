using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace EcommMarket.Application.Dto;

public class ProductDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public virtual List<PhotoDto>? Photos { get; set; }
    public CategoryDto Category { get; set; }
}
