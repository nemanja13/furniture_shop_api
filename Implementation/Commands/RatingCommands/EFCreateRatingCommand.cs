using Application;
using Application.Commands.RatingCommands;
using Application.DataTransfer;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.RatingCommands
{
    public class EFCreateRatingCommand : ICreateRatingCommand
    {
        private readonly ProjekatASPContext _context;
        private readonly CreateRatingValidator _validator;
        private readonly IApplicationActor _actor;
        private readonly IMapper _mapper;

        public EFCreateRatingCommand(ProjekatASPContext context, CreateRatingValidator validator, IMapper mapper, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
            _actor = actor;
        }

        public int Id => 18;

        public string Name => "Creating New Rating using EntityFramework";

        public void Execute(RatingDto request)
        {
            request.UserId = _actor.Id;
            _validator.ValidateAndThrow(request);

            _context.Ratings.Add(_mapper.Map<Rating>(request));
            _context.SaveChanges();
        }
    }
}
