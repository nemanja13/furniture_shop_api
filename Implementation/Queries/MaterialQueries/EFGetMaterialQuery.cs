using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Queries.MaterialQueries;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Queries.MaterialQueries
{
    public class EFGetMaterialQuery : IGetMaterialQuery
    {
        private readonly ProjekatASPContext _context;
        private readonly IMapper _mapper;

        public EFGetMaterialQuery(ProjekatASPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 28;

        public string Name => "Finding a Material using the Entity Framework";

        public MaterialDto Execute(int search)
        {
            var material = _context.Materials.Find(search);

            if (material == null)
                throw new EntityNotFoundException(search, typeof(Material));

            return _mapper.Map<MaterialDto>(material);
        }
    }
}
