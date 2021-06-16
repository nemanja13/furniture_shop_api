using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Queries.UserQueries;
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
    public class EFGetUserQuery : IGetUserQuery
    {
        private readonly ProjekatASPContext _context;
        private readonly IMapper _mapper;

        public EFGetUserQuery(ProjekatASPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 35;

        public string Name => "Finding a User using the Entity Framework";

        public UserDto Execute(int search)
        {
            var user = _context.Users.Include(x => x.UserUseCases).FirstOrDefault(x => x.Id == search);

            if (user == null)
                throw new EntityNotFoundException(search, typeof(User));

            return _mapper.Map<UserDto>(user);
        }
    }
}
