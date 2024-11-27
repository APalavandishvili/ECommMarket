namespace ECommMarket.Domain.Entities
{
    public class Order : BaseEntity<int>
    {
        public int TransactionId { get; set; }
        public virtual ICollection<Product>? Items { get; set; }
    }
}
