using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class CreateRatingValidator : AbstractValidator<RatingDto>
    {
        public CreateRatingValidator(ProjekatASPContext context)
        {
            RuleFor(x => x.Mark)
                .GreaterThan(0)
                .WithMessage("Mark must be above 0.")
                .LessThanOrEqualTo(5)
                .WithMessage("Mark must be below 5.");
            RuleFor(x => x.ProductId)
                .Must(id => context.Products.Any(product => product.Id == id))
                .WithMessage((dto, id) => $"Product with an id of {id} does not exists in database.");
            RuleFor(x => x.UserId)
                .Must(id => context.Users.Any(user => user.Id == id))
                .WithMessage((dto, id) => $"User with an id of {id} does not exists in database.")
                .DependentRules(() => {
                    RuleFor(x => x.UserId)
                    .Must((dto, id) => !context.Ratings.Any(r => r.UserId == id && r.ProductId == dto.ProductId))
                    .WithMessage("You have already rated this product");
                });
        }
    }
}
