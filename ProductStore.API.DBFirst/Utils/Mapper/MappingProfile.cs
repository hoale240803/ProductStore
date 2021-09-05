using AutoMapper;
using ProductStore.API.DBFirst.DataModels;
using ProductStore.API.DBFirst.ViewModels.Media;
using ProductStore.API.DBFirst.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductStore.API.DBFirst.Utils.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            //CreateMap<ProductsVM, Products>().ReverseMap();

            //CreateMap<ProductsEntity, ProductsFull>().ForMember(pf => pf.Id, pe => pe.MapFrom(src => src.Id))
            //    .ForMember(pf => pf.Id, pe => pe.MapFrom(src => src.Id))
            //    .ForMember(pf => pf.Name, pe => pe.MapFrom(src => src.Name))
            //    .ForMember(pf => pf.Alias, pe => pe.MapFrom(src => src.Alias))
            //    .ForMember(pf => pf.MoreImages, pe => pe.MapFrom(src => src.MoreImages))
            //    .ForMember(pf => pf.Price, pe => pe.MapFrom(src => src.Price))
            //    .ForMember(pf => pf.OriginalPrice, pe => pe.MapFrom(src => src.OriginalPrice))
            //    .ForMember(pf => pf.Stock, pe => pe.MapFrom(src => src.Stock))
            //     .ForMember(pf => pf.ViewCount, pe => pe.MapFrom(src => src.ViewCount))
            //     .ForMember(pf => pf.Description, pe => pe.MapFrom(src => src.Description))
            //     .ForMember(pf => pf.Details, pe => pe.MapFrom(src => src.Details))
            //     .ForMember(pf => pf.CreateBy, pe => pe.MapFrom(src => src.CreateBy))
            //     .ForMember(pf => pf.CreatedDate, pe => pe.MapFrom(src => src.CreatedDate))
            //    .ForMember(pf => pf.CategoryID, pe => pe.MapFrom(src => src.CategoryID))
            //    .ForMember(pf => pf.SeoAlias, pe => pe.MapFrom(src => src.SeoAlias))
            //.ForMember(pf => pf.SeoDescription, pe => pe.MapFrom(src => src.SeoDescription))
            //.ForMember(pf => pf.SeoTitle, pe => pe.MapFrom(src => src.SeoTitle))
            //.ForMember(pf => pf.LanguageId, pe => pe.MapFrom(src => src.LanguageId))
            //.ForMember(pf => pf.IsFeatured, pe => pe.MapFrom(src => src.IsFeatured))
            //.ForMember(pf => pf.Image, pe => pe.MapFrom(src => src.Image)).ReverseMap();

            //CreateMap<ProductCategoryEntity, ProductsFull>().ReverseMap();

            //CreateMap<OrderDetailsVM, OrderDetails>();

            //CreateMap<OrdersVM, Orders>();
            CreateMap<Product, ProductDTO>()
              .ForMember(dest => dest.MediaDTO, opt =>
                 opt.MapFrom(src => src.Media.Select(x =>
                 new MediaDTO()
                 {
                     Id = x.Id,
                     Type = x.Type,
                     ExternalUrl = x.ExternalUrl,
                     Name = x.Name,
                     FileId = x.FileId,
                 }
                 ))
              );
            CreateMap<ProductDTO, Product>()
              .ForMember(dest => dest.Media, opt =>
                 opt.MapFrom(src => src.MediaDTO.Select(x =>
                 new Media()
                 {
                     Id = x.Id,
                     Type = x.Type,
                     ExternalUrl = x.ExternalUrl,
                     Name = x.Name,
                     FileId = x.FileId,
                 }
                 ))
               );



            //     .ForMember(dest => dest.MyProperty, opt => opt.MapFrom(
            //src => new B1(src.Prop3, src.Prop4)));
        }
    }
}