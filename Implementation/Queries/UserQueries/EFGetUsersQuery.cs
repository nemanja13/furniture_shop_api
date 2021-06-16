using Application.DataTransfer;
using Application.Queries;
using Application.Queries.UserQueries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.UserQueries
{
    public class EFGetUsersQuery : IGetUsersQuery
    {
        private readonly ProjekatASPContext _context;
        private readonly IMapper _mapper;

        public EFGetUsersQuery(ProjekatASPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 36;

        public string Name => "Searching Users using EntityFramework";

        public PagedResponse<UserDto> Execute(SearchUserDto search)
        {
            var query = _context.Users.Include(x => x.Orders).Include(x => x.UserUseCases).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
                query = query.Where(x => x.FirstName.ToLower().Contains(search.Keyword.ToLower()) ||
                                        x.LastName.ToLower().Contains(search.Keyword.ToLower()) ||
                                        x.Email.ToLower().Contains(search.Keyword.ToLower()));

            if (search.OrdersMin.HasValue)
                query = query.Where(x => x.Orders.Count() >= search.OrdersMin);

            if (search.OrdersMax.HasValue)
                query = query.Where(x => x.Orders.Count() <= search.OrdersMax);

            if (search.UseCases.Count() > 0)
                query = query.Where(x => x.UserUseCases.Any(uuc => search.UseCases.Contains(uuc.UseCaseId)));

            return query.GetPagedResponse<User, UserDto>(search, _mapper);
        }
    }
}
