using Application.DataTransfer;
using Application.Queries;
using Application.Queries.LogQueries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.LogQueries
{
    public class EFGetLogsQuery : IGetLogsQuery
    {
        private readonly ProjekatASPContext _context;
        private readonly IMapper _mapper;

        public EFGetLogsQuery(ProjekatASPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 38;

        public string Name => "Searching Logs using EntityFramework";

        public PagedResponse<LogDto> Execute(SearchLogDto search)
        {
            var query = _context.UseCaseLogs.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
                query = query.Where(x => x.Actor.ToLower().Contains(search.Keyword.ToLower()) ||
                                        x.UseCaseName.ToLower().Contains(search.Keyword.ToLower()));

            if (search.DateMin.HasValue)
                query = query.Where(x => x.CreatedAt >= search.DateMin);

            if (search.DateMax.HasValue)
                query = query.Where(x => x.CreatedAt <= search.DateMax);

            return query.GetPagedResponse<UseCaseLog, LogDto>(search, _mapper);
        }
    }
}
