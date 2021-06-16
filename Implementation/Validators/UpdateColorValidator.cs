using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class UpdateColorValidator : AbstractValidator<ColorDto>
    {
        public UpdateColorValidator(ProjekatASPContext context)
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("Color {PropertyName} must not be empty")
               .DependentRules(() =>
               {
                   RuleFor(x => x.Name)
                  .Must((dto, name) => !context.Colors.Any(c => c.Name == name && c.Id != dto.Id))
                  .WithMessage(dto => $"Color with the name of {dto.Name} already exists in database.");
               });
        }
    }
}
