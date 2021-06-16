using Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.CommentQueries
{
    public interface IGetCommentQuery : IQuery<int, CommentDto>
    {
    }
}
