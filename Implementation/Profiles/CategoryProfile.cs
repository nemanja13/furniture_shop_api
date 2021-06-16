using Application.DataTransfer;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
