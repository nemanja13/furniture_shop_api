using Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.CategoryQueries
{
    public interface IGetCategoryQuery : IQuery<int, CategoryDto>
    {
    }
}
