using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class CreateColorValidator : AbstractValidator<ColorDto>
    {
        public CreateColorValidator(ProjekatASPContext context)
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("Color {PropertyName} must not be empty")
               .DependentRules(() =>
               {
                   RuleFor(x => x.Name)
                   .Must(name => !context.Colors.Any(c => c.Name == name))
                   .WithMessage("Color with name of {PropertyValue} already exists in database");
               });
        }
    }
}
