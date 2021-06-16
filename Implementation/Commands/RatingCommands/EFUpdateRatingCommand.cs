using Application;
using Application.Commands.RatingCommands;
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

namespace Implementation.Commands.RatingCommands
{
    public class EFUpdateRatingCommand : IUpdateRatingCommand
    {
        private readonly ProjekatASPContext _context;
        private readonly UpdateRatingValidator _validator;
        private readonly IApplicationActor _actor;
        private readonly IMapper _mapper;

        public EFUpdateRatingCommand(ProjekatASPContext context, UpdateRatingValidator validator, IMapper mapper, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
            _actor = actor;
        }

        public int Id => 19;

        public string Name => "Updating Rating using Entity Framework";

        public void Execute(RatingDto request)
        {
            request.UserId = _actor.Id;
            var rating = _context.Ratings.Find(request.Id);

            if (rating == null)
            {
                throw new EntityNotFoundException(request.Id.Value, typeof(Rating));
            }

            _validator.ValidateAndThrow(request);

            _mapper.Map(request, rating);
            _context.SaveChanges();
        }
    }
}
