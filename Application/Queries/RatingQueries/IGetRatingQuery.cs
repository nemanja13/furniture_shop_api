using Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.RatingQueries
{
    public interface IGetRatingQuery : IQuery<int, RatingDto>
    {
    }
}
