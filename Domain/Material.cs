using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Material : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<MaterialProduct> MaterialProducts { get; set; } = new HashSet<MaterialProduct>();
    }
}
