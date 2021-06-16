using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries.CategoryQueries;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Queries.CategoryQueries
{
    public class EFGetCategoryQuery : IGetCategoryQuery
    {
        private readonly ProjekatASPContext _context;
        private readonly IMapper _mapper;

        public EFGetCategoryQuery(ProjekatASPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 24;

        public string Name => "Finding a Category using the Entity Framework";

        public CategoryDto Execute(int search)
        {
            var category = _context.Categories.Find(search);

            if (category == null)
                throw new EntityNotFoundException(search, typeof(Category));

            return _mapper.Map<CategoryDto>(category);
        }
    }
}
