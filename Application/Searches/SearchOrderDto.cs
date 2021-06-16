using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class SearchOrderDto : PagedSearch
    {
        public DateTime? OrderDateMin { get; set; }
        public DateTime? OrderDateMax { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public IEnumerable<int> UserIds { get; set; } = new List<int>();
        public IEnumerable<int> ProductIds { get; set; } = new List<int>();
        public int? OrderLineQuantityMin { get; set; }
        public int? OrderLineQuantityMax { get; set; }
        public int? OrderLinesMin { get; set; }
        public int? OrderLinesMax { get; set; }
        public decimal? TotalPriceMin { get; set; }
        public decimal? TotalPriceMax { get; set; }
    }
}
