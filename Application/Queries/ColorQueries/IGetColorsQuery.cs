using Application.DataTransfer;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.ColorQueries
{
    public interface IGetColorsQuery : IQuery<SearchColorDto, PagedResponse<ColorDto>>
    {
    }
}
