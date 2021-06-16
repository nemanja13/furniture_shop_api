using Application.DataTransfer;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.OrderQueries
{
    public interface IGetOrdersQuery : IQuery<SearchOrderDto, PagedResponse<OrderDto>>
    {
    }
}
