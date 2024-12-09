
using ECommMarket.Domain.Interfaces;

namespace ECommMarket.Domain.Entities
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime? UpdateTimestamp { get; set; }
        public int? UpdateBy { get; set; }
    }
}
