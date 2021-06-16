using Application.Commands.UserCommands;
using Application.Exceptions;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.UserCommands
{
    public class EFDeleteUserCommand : IDeleteUserCommand
    {
        private readonly ProjekatASPContext _context;

        public EFDeleteUserCommand(ProjekatASPContext context)
        {
            _context = context;
        }

        public int Id => 21;

        public string Name => "Deleting User using Entity Framework";

        public void Execute(int request)
        {
            var user = _context.Users.Find(request);

            if (user == null)
            {
                throw new EntityNotFoundException(request, typeof(User));
            }

            user.IsActive = false;
            user.IsDeleted = true;
            user.DeletedAt = DateTime.UtcNow;

            _context.SaveChanges();
        }
    }
}
