using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class UpdateProductValidator : AbstractValidator<ProductDto>
    {
        public UpdateProductValidator(ProjekatASPContext context)
        {
            RuleFor(p => p.Name).NotEmpty()
                .WithMessage("Product {PropertyName} must not be empty.").
                DependentRules(() =>
                {
                    RuleFor(p => p.Name)
                    .Must((dto, name) => !context.Products.Any(p => p.Name == name && p.Id != dto.Id))
                    .WithMessage(dto => $"Product with name {dto.Name} already exists in database.");
                });

            RuleFor(p => p.FullName).NotEmpty()
               .WithMessage("Product Full Name must not be empty.");

            RuleFor(p => p.Price).NotEmpty()
                .WithMessage("Product {PropertyName} must not be empty.").
                DependentRules(() =>
                {
                    RuleFor(p => p.Price)
                    .GreaterThan(0)
                    .WithMessage("Product {PropertyName} must be greater than 0.");
                });

            RuleFor(p => p.Quantity).NotEmpty()
                .WithMessage("Product {PropertyName} must not be empty.").
                DependentRules(() =>
                {
                    RuleFor(p => p.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Product {PropertyName} must be greater than 0.");
                });

            RuleFor(p => p.Description).NotEmpty()
                .WithMessage("Product {PropertyName} must not be empty.");

            RuleFor(p => p.Dimensions).NotEmpty()
                .WithMessage("Product {PropertyName} must not be empty.");

            RuleFor(p => p.CategoryId).NotEmpty()
                .WithMessage("You must enter a category").
                DependentRules(() =>
                {
                    RuleFor(p => p.CategoryId)
                    .Must(id => context.Categories.Any(c => c.Id == id))
                    .WithMessage((dto, id) => $"Category with an id of {id} does not exists in database.");
                });

            RuleFor(p => p.ColorIds).NotEmpty()
                .WithMessage("You must assign a color to the product")
                .DependentRules(() => {
                    RuleFor(p => p.ColorIds)
                    .Must(ids => ids.Distinct().Count() == ids.Count())
                    .WithMessage("You have entered double values ​​for the color");
                    RuleForEach(p => p.ColorIds).Must(id => context.Colors.Any(c => c.Id == id))
                    .WithMessage((dto, id) => $"Color with an id of {id} does not exists in database.");
                });

            RuleFor(p => p.MaterialIds).NotEmpty()
                .WithMessage("You must assign a material to the product")
                .DependentRules(() => {
                    RuleFor(p => p.MaterialIds)
                    .Must(ids => ids.Distinct().Count() == ids.Count())
                    .WithMessage("You have entered double values ​​for the material");
                    RuleForEach(p => p.MaterialIds).Must(id => context.Materials.Any(m => m.Id == id))
                    .WithMessage((dto, id) => $"Material with an id of {id} does not exists in database.");
                });
        }
    }
}
