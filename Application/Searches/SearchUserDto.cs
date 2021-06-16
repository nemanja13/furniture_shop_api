using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class SearchUserDto : PagedSearch
    {
        public int? OrdersMin { get; set; }
        public int? OrdersMax { get; set; }
        public IEnumerable<int> UseCases { get; set; } = new List<int>();
    }
}
