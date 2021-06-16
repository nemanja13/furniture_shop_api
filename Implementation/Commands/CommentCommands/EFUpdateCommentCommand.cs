using Application;
using Application.Commands.CommentCommands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.CommentCommands
{
    public class EFUpdateCommentCommand : IUpdateCommentCommand
    {
        private readonly ProjekatASPContext _context;
        private readonly UpdateCommentValidator _validator;
        private readonly IApplicationActor _actor;
        private readonly IMapper _mapper;

        public EFUpdateCommentCommand(ProjekatASPContext context, UpdateCommentValidator validator, IMapper mapper, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
            _actor = actor;
        }

        public int Id => 9;

        public string Name => "Updating Comment using Entity Framework";

        public void Execute(CommentDto request)
        {
            request.UserId = _actor.Id;
            var comment = _context.Comments.Find(request.Id);

            if (comment == null)
            {
                throw new EntityNotFoundException(request.Id.Value, typeof(Comment));
            }

            _validator.ValidateAndThrow(request);

            _mapper.Map(request, comment);
            _context.SaveChanges();
        }
    }
}
