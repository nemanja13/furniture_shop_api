using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class ProductDto : DtoBase
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Dimensions { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
        public int Quantity { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int? Mark { get; set; }
        public IEnumerable<int> ColorIds { get; set; } = new List<int>();
        public IEnumerable<string> Colors { get; set; } = new List<string>();
        public IEnumerable<int> MaterialIds { get; set; } = new List<int>();
        public IEnumerable<string> Materials { get; set; } = new List<string>();
        public IEnumerable<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}
