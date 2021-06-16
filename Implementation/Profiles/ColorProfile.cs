using Application.DataTransfer;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Profiles
{
    public class ColorProfile : Profile
    {
        public ColorProfile()
        {
            CreateMap<ColorDto, Color>();
            CreateMap<Color, ColorDto>();
        }
    }
}
