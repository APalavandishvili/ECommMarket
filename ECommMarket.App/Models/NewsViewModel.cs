using ECommMarket.App.Filters;

namespace ECommMarket.App.Models;

public class NewsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Article { get; set; }
    public string Details { get; set; }
    public DateTime Timestamp { get; set; }
    public PhotoViewModel Photo { get; set; }
    [ImageValidator(ErrorMessage = "არავალიდური სურათის ფორმატი ან არავალიოდური სურათი (.jpg\", \".jpeg\", \".png)")]
    public IFormFile UploadedPhoto { get; set; }

}
