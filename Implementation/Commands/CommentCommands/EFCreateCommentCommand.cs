using Application;
using Application.Commands.CommentCommands;
using Application.DataTransfer;
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
    public class EFCreateCommentCommand : ICreateCommentCommand
    {
        private readonly ProjekatASPContext _context;
        private readonly CreateCommentValidator _validator;
        private readonly IApplicationActor _actor;
        private readonly IMapper _mapper;

        public EFCreateCommentCommand(ProjekatASPContext context, CreateCommentValidator validator, IMapper mapper, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
            _actor = actor;
        }

        public int Id => 7;

        public string Name => "Creating New Comment using EntityFramework";

        public void Execute(CommentDto request)
        {
            request.UserId = _actor.Id;
            _validator.ValidateAndThrow(request);

            _context.Comments.Add(_mapper.Map<Comment>(request));
            _context.SaveChanges();
        }
    }
}
