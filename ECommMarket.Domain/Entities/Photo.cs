﻿    namespace ECommMarket.Domain.Entities
{
    public class Photo : BaseEntity<int>
    {
        public string PhotoName { get; set; }
        public string PhotoUrl { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
