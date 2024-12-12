using System.ComponentModel.DataAnnotations;

namespace EcommMarket.Application.Dto;

public class OrderDto
{
    public string TransactionId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string City { get; set; }

    public string Address { get; set; }

    public DateTime Timestamp { get; set; }

    public List<ProductDto> Products { get; set; }
}
