using Application.Commands.ColorCommands;
using Application.Exceptions;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.ColorCommands
{
    public class EFDeleteColorCommand : IDeleteColorCommand
    {
        private readonly ProjekatASPContext _context;

        public EFDeleteColorCommand(ProjekatASPContext context)
        {
            _context = context;
        }

        public int Id => 5;

        public string Name => "Deleting Color using Entity Framework";

        public void Execute(int request)
        {
            var color = _context.Colors.Find(request);

            if (color == null)
            {
                throw new EntityNotFoundException(request, typeof(Color));
            }

            color.IsActive = false;
            color.IsDeleted = true;
            color.DeletedAt = DateTime.UtcNow;

            _context.SaveChanges();
        }
    }
}
