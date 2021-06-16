using Application.DataTransfer;
using Application.Queries;
using Application.Queries.ColorQueries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.ColorQueries
{
    public class EFGetColorsQuery : IGetColorsQuery
    {
        private readonly ProjekatASPContext _context;
        private readonly IMapper _mapper;

        public EFGetColorsQuery(ProjekatASPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 26;

        public string Name => "Searching Colors using EntityFramework";

        public PagedResponse<ColorDto> Execute(SearchColorDto search)
        {
            var query = _context.Colors.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()));

            return query.GetPagedResponse<Color, ColorDto>(search, _mapper);
        }
    }
}
