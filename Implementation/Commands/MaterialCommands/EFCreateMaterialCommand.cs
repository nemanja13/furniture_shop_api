using Application.Commands.MaterialCommands;
using Application.DataTransfer;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.MaterialCommands
{
    public class EFCreateMaterialCommand : ICreateMaterialCommand
    {
        private readonly ProjekatASPContext _context;
        private readonly CreateMaterialValidator _validator;
        private readonly IMapper _mapper;

        public EFCreateMaterialCommand(ProjekatASPContext context, CreateMaterialValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 10;

        public string Name => "Creating New Material using EntityFramework";

        public void Execute(MaterialDto request)
        {
            _validator.ValidateAndThrow(request);

            _context.Materials.Add(_mapper.Map<Material>(request));
            _context.SaveChanges();
        }
    }
}
