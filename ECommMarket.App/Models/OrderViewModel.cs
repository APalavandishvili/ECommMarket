using System.ComponentModel.DataAnnotations;

namespace ECommMarket.App.Models;

public class OrderViewModel
{
    public int Id { get; set; }

    public string? TransactionId { get; set; }

    [Required(ErrorMessage = "აუცილებელი ველი")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "აუცილებელი ველი")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "აუცილებელი ველი")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "გთხოვთ შეიყვანოთ ვალიდური იმეილის მისამართი")]
    public string Email { get; set; }

    [RegularExpression(@"^\+9955\d{8}$", ErrorMessage = "ტელეფონის ნომერი უნდა იწყებოდეს '+9955' და შედგებოდეს 11 ციფრისაგან")]
    public string PhoneNumber { get; set; }

    [StringLength(50, ErrorMessage = "ქალაქი არ შეიძლება იყოს 50 სიმბოლოზე მეტი")]
    public string City { get; set; }
    
    [StringLength(50, ErrorMessage = "მისამართი არ შეიძლება იყოს 50 სიმბოლოზე მეტი")]
    public string Address { get; set; }

    public DateTime Timestamp { get; set; }

    public List<ProductViewModel>? Products { get; set; }
}
