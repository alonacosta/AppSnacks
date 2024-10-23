using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSnacks.Models
{
    public class ShoppingCartItems
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal ValueTotal { get; set; }
        public int ProductId { get; set; }
        public int ClientId { get; set; }

    }
}
