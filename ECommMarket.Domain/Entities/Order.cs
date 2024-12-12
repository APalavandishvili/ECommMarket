namespace ECommMarket.Domain.Entities
{
    public class Order : BaseEntity<int>
    {
        public string TransactionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Product>? Items { get; set; }
    }
}
