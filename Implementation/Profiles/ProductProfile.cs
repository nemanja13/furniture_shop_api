using Application.DataTransfer;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dto => dto.CategoryName, opt => opt.MapFrom(product => product.Category.Name))
                .ForMember(dto => dto.ImagePath, opt => opt.MapFrom(product => product.Image))
                .ForMember(dto => dto.Image, opt => opt.Ignore())
                .ForMember(dto => dto.Colors, opt => opt.MapFrom(product => product.ProductColors.Select(pc => pc.Color.Name)))
                .ForMember(dto => dto.Materials, opt => opt.MapFrom(product => product.ProductMaterials.Select(pc => pc.Material.Name)))
                .ForMember(dto => dto.Mark, opt => opt.MapFrom(product => product.Ratings.Count() > 0 ? product.Ratings.Sum(r => r.Mark)/product.Ratings.Count() : 0))
                .ForMember(dto => dto.Price, opt => opt.MapFrom(product => product.Prices.OrderByDescending(p => p.CreatedAt).Select(p => p.SalePrice).First()))
                .ForMember(dto => dto.OldPrice, opt => opt.MapFrom(product => product.Prices.OrderByDescending(p => p.CreatedAt).Select(p => p.SalePrice).ElementAtOrDefault(1)))
                .ForMember(dto => dto.Comments, opt => opt.MapFrom(product => product.Comments.Select(c => new CommentDto
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    ProductId = c.ProductId,
                    CommentText = c.CommentText,
                    CreatedAt = c.CreatedAt,
                    UserName = c.User.FirstName + " " + c.User.LastName
                }).ToList()));

            CreateMap<ProductDto, Product>()
                .ForMember(product => product.Image, opt => opt.Ignore());
        }
    }
}
