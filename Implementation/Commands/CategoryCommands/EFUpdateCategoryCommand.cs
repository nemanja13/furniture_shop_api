using Application.Commands.CategoryCommands;
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

namespace Implementation.Commands.CategoryCommands
{
    public class EFUpdateCategoryCommand : IUpdateCategoryCommand
    {
        private readonly ProjekatASPContext _context;
        private readonly UpdateCategoryValidator _validator;
        private readonly IMapper _mapper;

        public EFUpdateCategoryCommand(ProjekatASPContext context, IMapper mapper, UpdateCategoryValidator validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public int Id => 3;

        public string Name => "Updating Category using Entity Framework";

        public void Execute(CategoryDto request)
        {
            var category = _context.Categories.Find(request.Id);

            if(category == null)
            {
                throw new EntityNotFoundException(request.Id.Value, typeof(Category));
            }

            _validator.ValidateAndThrow(request);

            _mapper.Map(request, category);
            _context.SaveChanges();
        }
    }
}
