using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class UpdateMaterialValidator : AbstractValidator<MaterialDto>
    {
        public UpdateMaterialValidator(ProjekatASPContext context)
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("Material {PropertyName} must not be empty")
               .DependentRules(() =>
               {
                   RuleFor(x => x.Name)
                  .Must((dto, name) => !context.Materials.Any(m => m.Name == name && m.Id != dto.Id))
                  .WithMessage(dto => $"Material with the name of {dto.Name} already exists in database.");
               });
        }
    }
}
