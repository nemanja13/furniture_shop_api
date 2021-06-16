using Application.Commands.UserCommands;
using Application.DataTransfer;
using Application.Email;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Implementation.Commands.UserCommands
{
    public class EFCreateUserCommand : ICreateUserCommand
    {

        private readonly ProjekatASPContext _context;
        private readonly CreateUserValidator _validator;
        private readonly IMapper _mapper;
        private readonly IEmailSender _sender;

        public EFCreateUserCommand(ProjekatASPContext context, CreateUserValidator validator, IMapper mapper, IEmailSender sender)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
            _sender = sender;
        }

        public int Id => 20;

        public string Name => "Creating New User using EntityFramework";

        public void Execute(UserDto request)
        {
            _validator.ValidateAndThrow(request);

            var user = _mapper.Map<User>(request);

            user.Password = request.Password.MD5Encrypt();

            var userUseCases = new List<int> { 7, 8, 9, 13, 18, 19, 20, 22, 27, 32, 33, 35 };

            userUseCases.ForEach(useCase => _context.UserUseCases.Add(new UserUseCase
            {
                User = user,
                UseCaseId = useCase
            }));

            _context.Users.Add(user);
            _context.SaveChanges();

            _sender.Send(new SendEmail
            {
                Content = "<h1>You have successfully registered!</h1>",
                SendTo = request.Email,
                Subject = "Registration"
            });
        }
    }
}
