using Application.DataTransfer;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.MaterialQueries
{
    public interface IGetMaterialsQuery : IQuery<SearchMaterialDto, PagedResponse<MaterialDto>>
    {
    }
}
