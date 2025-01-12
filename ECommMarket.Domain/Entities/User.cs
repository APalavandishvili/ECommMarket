namespace ECommMarket.Domain.Entities;

public class User : BaseEntity<int>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
