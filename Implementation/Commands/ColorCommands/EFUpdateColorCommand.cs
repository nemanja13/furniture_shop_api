using Application.Commands.ColorCommands;
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

namespace Implementation.Commands.ColorCommands
{
    public class EFUpdateColorCommand : IUpdateColorCommand
    {
        private readonly ProjekatASPContext _context;
        private readonly UpdateColorValidator _validator;
        private readonly IMapper _mapper;

        public EFUpdateColorCommand(ProjekatASPContext context, UpdateColorValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 6;

        public string Name => "Updating Color using Entity Framework";

        public void Execute(ColorDto request)
        {
            var color = _context.Colors.Find(request.Id);

            if (color == null)
            {
                throw new EntityNotFoundException(request.Id.Value, typeof(Color));
            }

            _validator.ValidateAndThrow(request);

            _mapper.Map(request, color);
            _context.SaveChanges();
        }
    }
}
