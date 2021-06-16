using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class ChangeOrderStatusDto
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
