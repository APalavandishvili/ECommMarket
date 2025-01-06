using System.ComponentModel.DataAnnotations;

namespace ECommMarket.App.Models;

public class OrderViewModel
{
    public string TransactionId { get; set; }

    [Required(ErrorMessage = "აუცილებელი ველი")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "აუცილებელი ველი")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "აუცილებელი ველი")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }

    [Phone(ErrorMessage = "Invalid phone number format")]
    public string PhoneNumber { get; set; }

    [StringLength(100, ErrorMessage = "City name cannot exceed 100 characters.")]
    public string City { get; set; }
    
    [StringLength(100, ErrorMessage = "City name cannot exceed 100 characters.")]
    public string Address { get; set; }

    public DateTime Timestamp { get; set; }

    public List<ProductViewModel> Products { get; set; }
}
