using Application.DataTransfer;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDto>()
                .ForMember(dto => dto.UserName, opt => opt.MapFrom(comment => comment.User.FirstName + " " + comment.User.LastName));
            CreateMap<CommentDto, Comment>();
        }
    }
}
