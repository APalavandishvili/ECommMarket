namespace ECommMarket.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateBy { get; set; }
    }
}
