using Application.Commands.CategoryCommands;
using Application.DataTransfer;
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
    public class EFCreateCategoryCommand : ICreateCategoryCommand
    {
        private readonly ProjekatASPContext _context;
        private readonly CreateCategoryValidator _validator;
        private readonly IMapper _mapper;

        public EFCreateCategoryCommand(ProjekatASPContext context, IMapper mapper, CreateCategoryValidator validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public int Id => 1;

        public string Name => "Creating New Category using EntityFramework";

        public void Execute(CategoryDto request)
        {
            _validator.ValidateAndThrow(request);

            var category = _mapper.Map<Category>(request);

            _context.Categories.Add(category);
            _context.SaveChanges();
        }
    }
}
