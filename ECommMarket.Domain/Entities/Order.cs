namespace ECommMarket.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int TransactionId { get; set; }
        public virtual ICollection<Product>? Items { get; set; }
    }
}
