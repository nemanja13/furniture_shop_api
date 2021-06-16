using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class UpdateCategoryValidator : AbstractValidator<CategoryDto>
    {
        public UpdateCategoryValidator(ProjekatASPContext context)
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("Category {PropertyName} must not be empty")
               .DependentRules(() =>
               {
                   RuleFor(x => x.Name)
                   .Must((dto, name) => !context.Categories.Any(c => c.Name == name && c.Id != dto.Id))
                   .WithMessage(dto => $"Category with the name of {dto.Name} already exists in database.");
               });
        }
    }
}
