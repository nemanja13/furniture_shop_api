using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Order : Entity
    {
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; } = new HashSet<OrderLine>();

    }

    public enum OrderStatus
    {
        Hold,
        Completed,
        Shipped,
        Canceled
    }

    public enum PaymentMethod
    {
        Cash,
        Checks,
        Card,
    }
}
