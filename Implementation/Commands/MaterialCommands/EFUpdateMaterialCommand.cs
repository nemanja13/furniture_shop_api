using Application.Commands.MaterialCommands;
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

namespace Implementation.Commands.MaterialCommands
{
    public class EFUpdateMaterialCommand : IUpdateMaterialCommand
    {
        private readonly ProjekatASPContext _context;
        private readonly UpdateMaterialValidator _validator;
        private readonly IMapper _mapper;

        public EFUpdateMaterialCommand(ProjekatASPContext context, UpdateMaterialValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 12;

        public string Name => "Updating Material using Entity Framework";

        public void Execute(MaterialDto request)
        {
            var material = _context.Materials.Find(request.Id);

            if (material == null)
            {
                throw new EntityNotFoundException(request.Id.Value, typeof(Material));
            }

            _validator.ValidateAndThrow(request);

            _mapper.Map(request, material);
            _context.SaveChanges();
        }
    }
}
