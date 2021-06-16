using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.DataTransfer
{
    public class OrderDto : DtoBase
    {
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string UserInfo { get; set; }
        public int UserId { get; set; }
        public IEnumerable<OrderLineDto> OrderLines { get; set; } = new List<OrderLineDto>();
        public decimal? TotalPrice { get; set; }
    }
}
