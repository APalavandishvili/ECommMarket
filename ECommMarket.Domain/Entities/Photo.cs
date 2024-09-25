using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMarket.Domain.Entities
{
    public class Photo : BaseEntity
    {
        public string? PhotoName { get; set; }
        public string? PhotoUrl { get; set; }
        public int PhotoWidth { get; set; }
        public int PhotoHeight { get; set; }
        public Product? Product { get; set; }
    }
}
