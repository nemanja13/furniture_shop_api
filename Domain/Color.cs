using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Color : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<ColorProduct> ColorProducts { get; set; } = new HashSet<ColorProduct>();
    }
}
