using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Dimensions { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; } = new HashSet<OrderLine>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<Price> Prices { get; set; } = new HashSet<Price>();
        public virtual ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
        public virtual ICollection<ColorProduct> ProductColors { get; set; } = new HashSet<ColorProduct>();
        public virtual ICollection<MaterialProduct> ProductMaterials { get; set; } = new HashSet<MaterialProduct>();
    }
}
