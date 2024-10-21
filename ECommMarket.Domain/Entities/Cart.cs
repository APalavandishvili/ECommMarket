namespace ECommMarket.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }
        public virtual ICollection<Product>? Products{ get; set; }

    }
}
