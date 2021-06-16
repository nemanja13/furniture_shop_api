using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries.ColorQueries;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Queries.ColorQueries
{
    public class EFGetColorQuery : IGetColorQuery
    {
        private readonly ProjekatASPContext _context;
        private readonly IMapper _mapper;

        public EFGetColorQuery(ProjekatASPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 25;

        public string Name => "Finding a Color using the Entity Framework";

        public ColorDto Execute(int search)
        {
            var color = _context.Colors.Find(search);

            if (color == null)
                throw new EntityNotFoundException(search, typeof(Color));

            return _mapper.Map<ColorDto>(color);
        }
    }
}
