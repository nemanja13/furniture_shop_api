using Application.Commands.ColorCommands;
using Application.DataTransfer;
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
    public class EFCreateColorCommand : ICreateColorCommand
    {
        private readonly ProjekatASPContext _context;
        private readonly CreateColorValidator _validator;
        private readonly IMapper _mapper;

        public EFCreateColorCommand(ProjekatASPContext context, CreateColorValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 4;

        public string Name => "Creating New Color using EntityFramework";

        public void Execute(ColorDto request)
        {
            _validator.ValidateAndThrow(request);

            _context.Colors.Add(_mapper.Map<Color>(request));
            _context.SaveChanges();
        }
    }
}
