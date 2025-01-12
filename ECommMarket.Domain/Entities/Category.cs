using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMarket.Domain.Entities;

public class Category : BaseEntity<int>
{
    public string Name { get; set; }
}
