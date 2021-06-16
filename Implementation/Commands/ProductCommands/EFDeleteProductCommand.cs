using Application.Commands.ProductCommands;
using Application.Exceptions;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.ProductCommands
{
    public class EFDeleteProductCommand : IDeleteProductCommand
    {
        private readonly ProjekatASPContext _context;

        public EFDeleteProductCommand(ProjekatASPContext context)
        {
            _context = context;
        }

        public int Id => 16;

        public string Name => "Deleting Product using Entity Framework";

        public void Execute(int request)
        {
            var product = _context.Products.Find(request);

            if (product == null)
            {
                throw new EntityNotFoundException(request, typeof(Product));
            }

            product.IsActive = false;
            product.IsDeleted = true;
            product.DeletedAt = DateTime.UtcNow;

            _context.SaveChanges();
        }
    }
}
