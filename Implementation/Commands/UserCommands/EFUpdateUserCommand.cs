using Application.Commands.UserCommands;
using Application.DataTransfer;
using Application.Exceptions;
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
    public class EFUpdateUserCommand : IUpdateUserCommand
    {
        private readonly ProjekatASPContext _context;
        private readonly UpdateUserValidator _validator;
        private readonly IMapper _mapper;

        public EFUpdateUserCommand(ProjekatASPContext context, UpdateUserValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 22;

        public string Name => "Updating User using Entity Framework";

        public void Execute(UserDto request)
        {
            var user = _context.Users.Find(request.Id);
            var oldPassword = user.Password;

            if (user == null)
            {
                throw new EntityNotFoundException(request.Id.Value, typeof(User));
            }

            _validator.ValidateAndThrow(request);

            _mapper.Map(request, user);

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                user.Password = request.Password.MD5Encrypt();
            }
            else
            {
                user.Password = oldPassword;
            }


            _context.SaveChanges();
        }
    }
}
