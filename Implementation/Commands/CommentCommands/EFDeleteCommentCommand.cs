using Application;
using Application.Commands.CommentCommands;
using Application.Exceptions;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Commands.CommentCommands
{
    public class EFDeleteCommentCommand : IDeleteCommentCommand
    {
        private readonly ProjekatASPContext _context;
        private readonly IApplicationActor _actor;

        public EFDeleteCommentCommand(ProjekatASPContext context, IApplicationActor actor)
        {
            _context = context;
            _actor = actor;
        }

        public int Id => 8;

        public string Name => "Deleting Comment using Entity Framework";

        public void Execute(int request)
        {
            var comment = _context.Comments.FirstOrDefault(x => x.Id == request && x.UserId == _actor.Id);

            if (comment == null)
            {
                throw new EntityNotFoundException(request, typeof(Comment));
            }

            _context.Comments.Remove(comment);

            _context.SaveChanges();
        }
    }
}
