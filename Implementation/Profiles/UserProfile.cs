using Application.DataTransfer;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.AllowedUseCases, opt => opt.MapFrom(user => user.UserUseCases.Select(uus => uus.UseCaseId)))
                .ForMember(dto => dto.Password, opt => opt.Ignore());
            CreateMap<UserDto, User>();
        }
    }
}
