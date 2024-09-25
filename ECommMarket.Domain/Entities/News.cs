using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMarket.Domain.Entities
{
    public class News : BaseEntity
    {
        public string? NewsInfo { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public int AuthorId { get; set; }
    }
}
