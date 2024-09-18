using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMarket.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }
        public virtual ICollection<Product>? Products{ get; set; }

    }
}
