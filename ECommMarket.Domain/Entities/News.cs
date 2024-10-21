namespace ECommMarket.Domain.Entities
{
    public class News : BaseEntity
    {
        public string? NewsInfo { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public int AuthorId { get; set; }
    }
}
