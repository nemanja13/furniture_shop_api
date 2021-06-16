using Application.DataTransfer;
using Application.Queries;
using Application.Queries.OrderQueries;
using Application.Searches;
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
    public class EFGetOrdersQuery : IGetOrdersQuery
    {
        private readonly ProjekatASPContext _context;
        private readonly IMapper _mapper;

        public EFGetOrdersQuery(ProjekatASPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 31;

        public string Name => "Searching Orders using EntityFramework";

        public PagedResponse<OrderDto> Execute(SearchOrderDto search)
        {
            var query = _context.Orders.Include(x => x.User).Include(x => x.OrderLines).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
                query = query.Where(x => x.OrderLines.Any(ol => ol.Product.Name.ToLower().Contains(search.Keyword.ToLower())) ||
                                            x.Address.ToLower().Contains(search.Keyword.ToLower()) ||
                                            x.User.FirstName.ToLower().Contains(search.Keyword.ToLower()) ||
                                            x.User.LastName.ToLower().Contains(search.Keyword.ToLower()));

            if(search.OrderDateMin.HasValue)
                query = query.Where(x => x.OrderDate >= search.OrderDateMin);

            if (search.OrderDateMax.HasValue)
                query = query.Where(x => x.OrderDate <= search.OrderDateMax);

            if (search.OrderLineQuantityMin.HasValue)
                query = query.Where(x => x.OrderLines.Any(ol => ol.Quantity >= search.OrderLineQuantityMin));

            if (search.OrderLineQuantityMax.HasValue)
                query = query.Where(x => x.OrderLines.Any(ol => ol.Quantity <= search.OrderLineQuantityMax));

            if (search.OrderLinesMin.HasValue)
                query = query.Where(x => x.OrderLines.Count() >= search.OrderLinesMin);

            if (search.OrderLinesMax.HasValue)
                query = query.Where(x => x.OrderLines.Count() <= search.OrderLinesMax);

            if (search.OrderStatus.HasValue)
                query = query.Where(x => x.OrderStatus == search.OrderStatus);

            if (search.PaymentMethod.HasValue)
                query = query.Where(x => x.PaymentMethod == search.PaymentMethod);

            if (search.UserIds.Count() > 0)
                query = query.Where(x => search.UserIds.Contains(x.UserId));

            if (search.ProductIds.Count() > 0)
                query = query.Where(x => x.OrderLines.Any(ol => search.ProductIds.Contains((int)ol.ProductId)));

            if(search.TotalPriceMin.HasValue)
                query = query.Where(x => x.OrderLines.Sum(ol => ol.Quantity * ol.Price) >= search.TotalPriceMin);

            if (search.TotalPriceMax.HasValue)
                query = query.Where(x => x.OrderLines.Sum(ol => ol.Quantity * ol.Price) <= search.TotalPriceMax);

            return query.GetPagedResponse<Order, OrderDto>(search, _mapper);
        }
    }
}
