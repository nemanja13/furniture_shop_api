using Application.Commands.MaterialCommands;
using Application.Exceptions;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.MaterialCommands
{
    public class EFDeleteMaterialCommand : IDeleteMaterialCommand
    {
        private readonly ProjekatASPContext _context;

        public EFDeleteMaterialCommand(ProjekatASPContext context)
        {
            _context = context;
        }

        public int Id => 11;

        public string Name => "Deleting Material using Entity Framework";

        public void Execute(int request)
        {
            var material = _context.Materials.Find(request);

            if (material == null)
            {
                throw new EntityNotFoundException(request, typeof(Material));
            }

            material.IsActive = false;
            material.IsDeleted = true;
            material.DeletedAt = DateTime.UtcNow;

            _context.SaveChanges();
        }
    }
}
