using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Rating : Entity
    {
        public int Mark { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
