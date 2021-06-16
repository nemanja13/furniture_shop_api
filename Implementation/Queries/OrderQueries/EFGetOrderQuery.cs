using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Queries.OrderQueries;
using AutoMapper;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.OrderQueries
{
    public class EFGetOrderQuery : IGetOrderQuery
    {
        private readonly ProjekatASPContext _context;
        private readonly IMapper _mapper;

        public EFGetOrderQuery(ProjekatASPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 30;

        public string Name => "Finding a Order using the Entity Framework";

        public OrderDto Execute(int search)
        {
            var order = _context.Orders.Include(x => x.User).Include(x => x.OrderLines).FirstOrDefault(x => x.Id == search);

            if (order == null)
                throw new EntityNotFoundException(search, typeof(Order));

            return _mapper.Map<OrderDto>(order);
        }
    }
}
