using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Validators
{
    public class CreateOrderValidator : AbstractValidator<OrderDto>
    {
        public CreateOrderValidator(ProjekatASPContext context)
        {
            RuleFor(x => x.OrderDate)
                .GreaterThan(DateTime.Today)
                .WithMessage("Order date must be in future.")
                .LessThan(DateTime.Now.AddDays(30))
                .WithMessage("Order date can not be more than 30 days from today");

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address is required.");

            RuleFor(x => x.PaymentMethod)
                .NotNull()
                .WithMessage("Payment Method is required.");

            RuleFor(x => x.UserId)
                .Must(id => context.Users.Any(user => user.Id == id))
                .WithMessage((dto, id) => $"User with an id of {id} does not exists in database.");

            RuleFor(x => x.OrderLines)
                .NotEmpty()
                .WithMessage("There must be at least one order line.")
                .Must(ol => ol.Select(x => x.ProductId).Distinct().Count() == ol.Count())
                .WithMessage("Duplicate products are not allowed.")
                .DependentRules(() =>
                {
                    RuleForEach(x => x.OrderLines).SetValidator(new CreateOrderLineValidator(context));
                });
        }
    }
}
