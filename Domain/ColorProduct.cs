using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ColorProduct
    {
        public int ColorId { get; set; }
        public int ProductId { get; set; }

        public virtual Color Color { get; set; }
        public virtual Product Product { get; set; }
    }
}
