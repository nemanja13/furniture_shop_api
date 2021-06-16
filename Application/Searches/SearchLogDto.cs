using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class SearchLogDto : PagedSearch
    {
        public DateTime? DateMin { get; set; }
        public DateTime? DateMax { get; set; }
    }
}
