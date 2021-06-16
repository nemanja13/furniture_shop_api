using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Price : Entity
    {
        public decimal SalePrice { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
