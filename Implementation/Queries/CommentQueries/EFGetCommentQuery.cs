using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Queries.CommentQueries;
using AutoMapper;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.CommentQueries
{
    public class EFGetCommentQuery : IGetCommentQuery
    {
        private readonly ProjekatASPContext _context;
        private readonly IMapper _mapper;

        public EFGetCommentQuery(ProjekatASPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 27;

        public string Name => "Finding a Comment using the Entity Framework";

        public CommentDto Execute(int search)
        {
            var comment = _context.Comments.Include(x => x.User).FirstOrDefault(x => x.Id == search);

            if (comment == null)
                throw new EntityNotFoundException(search, typeof(Comment));

            return _mapper.Map<CommentDto>(comment);
        }
    }
}
