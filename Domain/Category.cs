using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
