using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class CreateCommentValidator : AbstractValidator<CommentDto>
    {
        public CreateCommentValidator(ProjekatASPContext context)
        {
            RuleFor(x => x.CommentText)
                .NotEmpty()
                .WithMessage("Comment can not be empty.");
            RuleFor(x => x.ProductId)
                .Must(id => context.Products.Any(product => product.Id == id))
                .WithMessage((dto, id) => $"Product with an id of {id} does not exists in database.");
            RuleFor(x => x.UserId)
                .Must(id => context.Users.Any(user => user.Id == id))
                .WithMessage((dto, id) => $"User with an id of {id} does not exists in database.");
        }
    }
}
