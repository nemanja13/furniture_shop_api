using Application.DataTransfer;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Profiles
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile()
        {
            CreateMap<MaterialDto, Material>();
            CreateMap<Material, MaterialDto>();
        }
    }
}
