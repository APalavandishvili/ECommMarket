using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMarket.Domain.Entities;

public class Session : BaseEntity<int>
{
    public int UserId { get; set; }
    public string Token { get; set; }
}
