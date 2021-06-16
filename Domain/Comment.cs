using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Comment : Entity
    {
        public string CommentText { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
