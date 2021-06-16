using Application.Commands.ProductCommands;
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
using System.IO;
using System.Linq;
using System.Text;

namespace Implementation.Commands.ProductCommands
{
    public class EFUpdateProductCommand : IUpdateProductCommand
    {

        private readonly ProjekatASPContext _context;
        private readonly UpdateProductValidator _validator;
        private readonly IMapper _mapper;

        public EFUpdateProductCommand(ProjekatASPContext context, UpdateProductValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 17;

        public string Name => "Updating Product using Entity Framework";

        public void Execute(ProductDto request)
        {
            var product = _context.Products.Include(p => p.ProductColors).Include(p => p.ProductMaterials).Include(p => p.Prices).FirstOrDefault(p => p.Id == request.Id);

            if (product == null)
            {
                throw new EntityNotFoundException(request.Id.Value, typeof(Product));
            }

            _validator.ValidateAndThrow(request);

            _mapper.Map(request, product);

            if(request.Image != null)
            {
                var guid = Guid.NewGuid();

                var extension = Path.GetExtension(request.Image.FileName);

                var newFileName = guid + "_" + request.Image.FileName;

                var path = Path.Combine("wwwroot", "Images", newFileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    request.Image.CopyTo(fileStream);
                }

                product.Image = newFileName;
            }

            if(request.Price != product.Prices.OrderByDescending(p => p.CreatedAt).Select(p => p.SalePrice).First())
            {
                _context.Prices.Add(new Price
                {
                    Product = product,
                    SalePrice = request.Price
                });
            }

            product.ProductColors.Where(pc => !request.ColorIds.Contains(pc.ColorId)).ToList().ForEach(pc => product.ProductColors.Remove(pc));
            var existingColorIds = product.ProductColors.Select(pc => pc.ColorId);
            _context.Colors.Where(c => request.ColorIds.Except(existingColorIds).Contains(c.Id)).ToList().ForEach(color => _context.ColorProducts.Add(new ColorProduct
            {
                Color = color,
                Product = product
            }));

            product.ProductMaterials.Where(pm => !request.MaterialIds.Contains(pm.MaterialId)).ToList().ForEach(pm => product.ProductMaterials.Remove(pm));
            var existingMaterialIds = product.ProductMaterials.Select(pm => pm.MaterialId);
            _context.Materials.Where(m => request.MaterialIds.Except(existingMaterialIds).Contains(m.Id)).ToList().ForEach(material => _context.MaterialProducts.Add(new MaterialProduct
            {
                Material = material,
                Product = product
            }));

            _context.SaveChanges();
        }
    }
}
