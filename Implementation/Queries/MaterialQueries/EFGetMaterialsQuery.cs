using Application.DataTransfer;
using Application.Queries;
using Application.Queries.MaterialQueries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.MaterialQueries
{
    public class EFGetMaterialsQuery : IGetMaterialsQuery
    {
        private readonly ProjekatASPContext _context;
        private readonly IMapper _mapper;

        public EFGetMaterialsQuery(ProjekatASPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 29;

        public string Name => "Searching Materials using EntityFramework";

        public PagedResponse<MaterialDto> Execute(SearchMaterialDto search)
        {
            var query = _context.Materials.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
                query = query.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()));

            return query.GetPagedResponse<Material, MaterialDto>(search, _mapper);
        }
    }
}
