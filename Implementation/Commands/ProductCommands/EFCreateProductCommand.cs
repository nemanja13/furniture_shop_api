using Application.Commands.ProductCommands;
using Application.DataTransfer;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Implementation.Commands.ProductCommands
{
    public class EFCreateProductCommand : ICreateProductCommand
    {
        private readonly ProjekatASPContext _context;
        private readonly CreateProductValidator _validator;
        private readonly IMapper _mapper;

        public EFCreateProductCommand(ProjekatASPContext context, CreateProductValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 15;

        public string Name => "Creating New Product using EntityFramework";

        public void Execute(ProductDto request)
        {
            _validator.ValidateAndThrow(request);

            var product = _mapper.Map<Product>(request);

            var guid = Guid.NewGuid();

            var extension = Path.GetExtension(request.Image.FileName);

            var newFileName = guid + "_" + request.Image.FileName;

            var path = Path.Combine("wwwroot", "Images", newFileName);

            using(var fileStream = new FileStream(path, FileMode.Create))
            {
                request.Image.CopyTo(fileStream);
            }

            product.Image = newFileName;

            foreach(var colorId in request.ColorIds)
            {
                _context.ColorProducts.Add(new ColorProduct
                {
                    ColorId = colorId,
                    Product = product
                });
            }

            foreach (var materialId in request.MaterialIds)
            {
                _context.MaterialProducts.Add(new MaterialProduct
                {
                    MaterialId = materialId,
                    Product = product
                });
            }

            _context.Prices.Add(new Price
            {
                Product = product,
                SalePrice = request.Price
            });

            _context.Products.Add(product);
            _context.SaveChanges();
        }
    }
}
