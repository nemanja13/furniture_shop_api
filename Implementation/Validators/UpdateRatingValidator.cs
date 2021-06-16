using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class UpdateRatingValidator : AbstractValidator<RatingDto>
    {
        public UpdateRatingValidator(ProjekatASPContext context)
        {
            RuleFor(x => x.Mark)
                .GreaterThan(0)
                .WithMessage("Mark must be above 0.")
                .LessThanOrEqualTo(5)
                .WithMessage("Mark must be below 5.");
            RuleFor(x => x.ProductId)
                .Must((dto, id) => context.Ratings.Find(dto.Id).ProductId == id)
                .WithMessage((dto, id) => $"You can not change product.");
            RuleFor(x => x.UserId)
                .Must((dto, id) => context.Ratings.Where(r => r.UserId == id).Select(r => r.Id).ToList().Contains((int)dto.Id))
                .WithMessage((dto, id) => "You can not change this rating.");
        }
    }
}
