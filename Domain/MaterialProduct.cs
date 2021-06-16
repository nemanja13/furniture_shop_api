using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class MaterialProduct
    {
        public int MaterialId { get; set; }
        public int ProductId { get; set; }

        public virtual Material Material { get; set; }
        public virtual Product Product { get; set; }
    }
}
