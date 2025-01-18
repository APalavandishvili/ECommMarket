namespace ECommMarket.App.Models;

public class NewsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Article { get; set; }
    public string Details { get; set; }
    public DateTime Timestamp { get; set; }
    public PhotoViewModel Photo { get; set; }
    public IFormFile UploadedPhoto { get; set; }

}
