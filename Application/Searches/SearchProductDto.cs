using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class SearchProductDto : PagedSearch
    {
        public IEnumerable<int> CategoryIds { get; set; } = new List<int>();
        public IEnumerable<int> MaterialIds { get; set; } = new List<int>();
        public IEnumerable<int> ColorIds { get; set; } = new List<int>();
        public int? QuantityMin { get; set; }
        public int? QuantityMax { get; set; }
        public int? MarkMin { get; set; }
        public int? MarkMax { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
    }
}
