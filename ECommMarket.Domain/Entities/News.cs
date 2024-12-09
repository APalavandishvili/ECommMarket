namespace ECommMarket.Domain.Entities
{
    public class News : BaseEntity<int>
    {
        public string? Title { get; set; }
        public string? Article { get; set; }
        public string? Details { get; set; }
        public int AuthorId { get; set; }
    }
}
