using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class CreateOrderLineValidator : AbstractValidator<OrderLineDto>
    {
        public CreateOrderLineValidator(ProjekatASPContext context)
        {
            RuleFor(x => x.ProductId)
                .Must(id => context.Products.Any(x => x.Id == id))
                .WithMessage("Product with an id of {PropertyValue} doesn't exists.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Quantity)
                        .GreaterThan(0)
                        .WithMessage("Quantity must be greater than 0.")
                        .Must((dto, quantity) => context.Products.Find(dto.ProductId).Quantity >= quantity)
                        .WithMessage("Defined quantity ({PropertyValue}) is unavailable");
                });
        }
    }
}
