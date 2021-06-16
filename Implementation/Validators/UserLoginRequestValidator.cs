using Application.DataTransfer;
using Implementation.Commands;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginRequestValidator(ProjekatASPContext context)
        {
            RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage("Email must not be empty")
               .DependentRules(() => {
                   RuleFor(x => x.Email)
                   .Must(email => context.Users.Any(u => u.Email == email))
                   .WithMessage("User with an email of {PropertyValue} does not exist in database");
               });
            RuleFor(x => x.Password)
               .NotEmpty()
               .WithMessage("Password must not be empty")
               .DependentRules(() => {
                   RuleFor(x => x.Password)
                   .Must(password => context.Users.Any(u => u.Password == password.MD5Encrypt()))
                   .WithMessage("User with this password does not exist in database");
               });
        }
    }
}
