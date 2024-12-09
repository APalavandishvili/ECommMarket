    namespace ECommMarket.Domain.Entities
{
    public class Photo : BaseEntity<int>
    {
        public string PhotoName { get; set; }
        public string PhotoUrl { get; set; }
        public virtual Product Product { get; set; }
    }
}
