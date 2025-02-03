using ECommMarket.App.Filters;

namespace ECommMarket.App.Models;

public class ProductViewModel
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public CategoryViewModel Category { get; set; }
    public virtual List<PhotoViewModel>? Photos { get; set; }

    [ImageValidator(ErrorMessage = "არავალიდური სურათის ფორმატი ან არავალიდური სურათი, მისაღები ფორმატებია : (.jpg\", \".jpeg\", \".png)")]
    public List<IFormFile> UploadedPhotos { get; set; }
}
