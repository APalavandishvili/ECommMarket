namespace ECommMarket.App.Models;

public class ProductViewModel
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public virtual List<PhotoViewModel>? Photos { get; set; }
}
