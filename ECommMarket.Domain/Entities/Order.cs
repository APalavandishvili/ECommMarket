using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMarket.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int TransactionId { get; set; }
        public virtual ICollection<Product>? Items { get; set; }
    }
}
