using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommMarket.Application.ViewModels;

public class ProductViewModel
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public virtual List<PhotoViewModel>? Photos { get; set; }
}
