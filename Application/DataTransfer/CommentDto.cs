using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class CommentDto : DtoBase
    {
        public int? UserId { get; set; }
        public int ProductId { get; set; }
        public string UserName { get; set; }
        public string CommentText { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
