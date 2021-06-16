using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class UpdateCommentValidator : AbstractValidator<CommentDto>
    {
        public UpdateCommentValidator(ProjekatASPContext context)
        {
            RuleFor(x => x.CommentText)
                .NotEmpty()
                .WithMessage("Comment can not be empty.");
            RuleFor(x => x.UserId)
                .Must(id => context.Users.Any(user => user.Id == id))
                .WithMessage((dto, id) => $"User with an id of {id} does not exists in database.")
                .DependentRules(() => {
                    RuleFor(x => x.UserId)
                    .Must((dto, id) => context.Comments.Where(comment => comment.UserId == id).Select(comment => comment.Id).ToList().Contains((int)dto.Id))
                    .WithMessage((dto, id) => $"You can not update this comment.");
                });
            RuleFor(x => x.ProductId)
                .Must((dto, id) => context.Comments.Find(dto.Id).ProductId == id)
                .WithMessage((dto, id) => $"You can not change product.");
        }
    }
}
