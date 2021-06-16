using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Queries.ProductQueries;
using AutoMapper;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.ProductQueries
{
    public class EFGetProductQuery : IGetProductQuery
    {
        private readonly ProjekatASPContext _context;
        private readonly IMapper _mapper;

        public EFGetProductQuery(ProjekatASPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 32;

        public string Name => "Finding a Product using the Entity Framework";

        public ProductDto Execute(int search)
        {
            var product = _context.Products
                .Include(x => x.Prices)
                .Include(x => x.Comments)
                .ThenInclude(c => c.User)
                .Include(x => x.Ratings)
                .Include(x => x.Category)
                .Include(x => x.ProductColors)
                .ThenInclude(pc => pc.Color)
                .Include(x => x.ProductMaterials)
                .ThenInclude(pm => pm.Material)
                .FirstOrDefault(x => x.Id == search);

            if (product == null)
                throw new EntityNotFoundException(search, typeof(Product));

            return _mapper.Map<ProductDto>(product);
        }
    }
}
