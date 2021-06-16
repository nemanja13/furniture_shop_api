using Application.DataTransfer;
using Application.Queries;
using Application.Queries.CategoryQueries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.CategoryQueries
{
    public class EFGetCategoriesQuery : IGetCategoriesQuery
    {
        private readonly ProjekatASPContext _context;
        private readonly IMapper _mapper;

        public EFGetCategoriesQuery(ProjekatASPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 23;

        public string Name => "Searching Categories using EntityFramework";

        public PagedResponse<CategoryDto> Execute(SearchCategoryDto search)
        {
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()));

            return query.GetPagedResponse<Category, CategoryDto>(search, _mapper);
        }
    }
}
