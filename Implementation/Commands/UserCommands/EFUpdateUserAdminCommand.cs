using Application;
using Application.Commands.UserCommands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Commands.UserCommands
{
    public class EFUpdateUserAdminCommand : IUpdateUserAdminCommand
    {

        private readonly ProjekatASPContext _context;
        private readonly UpdateUserAdminValidator _validator;
        private readonly IMapper _mapper;
        private readonly IApplicationActor _actor;

        public EFUpdateUserAdminCommand(ProjekatASPContext context, UpdateUserAdminValidator validator, IMapper mapper, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
            _actor = actor;
        }

        public int Id => 37;

        public string Name => $"Updating User by Admin {_actor.Identity} using Entity Framework";

        public void Execute(UserDto request)
        {
            var user = _context.Users.Include(u => u.UserUseCases).FirstOrDefault(u => u.Id == request.Id);
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

            user.UserUseCases.Where(uuc => !request.AllowedUseCases.Contains(uuc.UseCaseId)).ToList().ForEach(uc => user.UserUseCases.Remove(uc));
            var existingUserUseCaseIds = user.UserUseCases.Select(uuc => uuc.UseCaseId);
            request.AllowedUseCases.Except(existingUserUseCaseIds).ToList().ForEach(useCaseId => _context.UserUseCases.Add(new UserUseCase
            {
                User = user,
                UseCaseId = useCaseId
            }));

            _context.SaveChanges();
        }
    }
}
