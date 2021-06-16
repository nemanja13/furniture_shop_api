using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Queries.RatingQueries;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Queries.RatingQueries
{
    public class EFGetRatingQuery : IGetRatingQuery
    {
        private readonly ProjekatASPContext _context;
        private readonly IMapper _mapper;

        public EFGetRatingQuery(ProjekatASPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 34;

        public string Name => "Finding a Rating using the Entity Framework";

        public RatingDto Execute(int search)
        {
            var rating = _context.Ratings.Find(search);

            if (rating == null)
                throw new EntityNotFoundException(search, typeof(Rating));

            return _mapper.Map<RatingDto>(rating);
        }
    }
}
