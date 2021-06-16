using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class RatingDto : DtoBase
    {
        public int Mark { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
