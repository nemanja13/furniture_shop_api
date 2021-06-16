using Application.DataTransfer;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Profiles
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<Rating, RatingDto>();
            CreateMap<RatingDto, Rating>();
        }
    }
}
