using Application.DataTransfer;
using Application.Queries;
using Application.Queries.ProductQueries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.ProductQueries
{
    public class EFGetProductsQuery : IGetProductsQuery
    {
        private readonly ProjekatASPContext _context;
        private readonly IMapper _mapper;

        public EFGetProductsQuery(ProjekatASPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 33;

        public string Name => "Searching Products using EntityFramework";

        public PagedResponse<ProductDto> Execute(SearchProductDto search)
        {
            var query = _context.Products
                .Include(x => x.Prices)
                .Include(x => x.Comments)
                .ThenInclude(c => c.User)
                .Include(x => x.Ratings)
                .Include(x => x.Category)
                .Include(x => x.ProductColors)
                .ThenInclude(pc => pc.Color)
                .Include(x => x.ProductMaterials)
                .ThenInclude(pm => pm.Material)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()) ||
                                        x.FullName.ToLower().Contains(search.Keyword.ToLower()) ||
                                        x.Description.ToLower().Contains(search.Keyword.ToLower()) ||
                                        x.Dimensions.ToLower().Contains(search.Keyword.ToLower()));

            if (search.QuantityMin.HasValue)
                query = query.Where(x => x.Quantity >= search.QuantityMin);

            if (search.QuantityMax.HasValue)
                query = query.Where(x => x.Quantity <= search.QuantityMax);

            if (search.MarkMin.HasValue)
                query = query.Where(x => x.Ratings.Count() > 0 ? x.Ratings.Sum(r => r.Mark) / x.Ratings.Count() >= search.MarkMin : false);

            if (search.MarkMax.HasValue)
                query = query.Where(x => x.Ratings.Count() > 0 ? x.Ratings.Sum(r => r.Mark) / x.Ratings.Count() <= search.MarkMin : false);

            if (search.ColorIds.Count() > 0)
                query = query.Where(x => x.ProductColors.Any(pc => search.ColorIds.Contains(pc.ColorId)));

            if (search.MaterialIds.Count() > 0)
                query = query.Where(x => x.ProductMaterials.Any(pc => search.MaterialIds.Contains(pc.MaterialId)));

            if (search.CategoryIds.Count() > 0)
                query = query.Where(x => search.CategoryIds.Contains(x.CategoryId));

            if (search.PriceMin.HasValue)
                query = query.Where(x => x.Prices.OrderByDescending(p => p.CreatedAt).First().SalePrice >= search.PriceMin);

            if (search.PriceMax.HasValue)
                query = query.Where(x => x.Prices.OrderByDescending(p => p.CreatedAt).First().SalePrice <= search.PriceMax);

            return query.GetPagedResponse<Product, ProductDto>(search, _mapper);
        }
    }
}
