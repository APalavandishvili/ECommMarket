namespace EcommMarket.Application.Dto;

public class NewsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Article { get; set; }
    public string Details { get; set; }
    public DateTime Timestamp { get; set; }
    public List<PhotoDto> Photos { get; set; }
}
