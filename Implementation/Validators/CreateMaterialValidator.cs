using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class CreateMaterialValidator : AbstractValidator<MaterialDto>
    {
        public CreateMaterialValidator(ProjekatASPContext context)
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("Material {PropertyName} must not be empty")
               .DependentRules(() =>
               {
                   RuleFor(x => x.Name)
                   .Must(name => !context.Materials.Any(m => m.Name == name))
                   .WithMessage("Material with name of {PropertyValue} already exists in database");
               });
        }
    }
}
