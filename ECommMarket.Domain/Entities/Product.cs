namespace ECommMarket.Domain.Entities
{
    public class Product : BaseEntity<int>
    {
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Cart>? Carts { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<Photo>? Photos { get; set; }
    }
}
