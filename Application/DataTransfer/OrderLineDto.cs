using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class OrderLineDto : DtoBase
    {
        public int? ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}
