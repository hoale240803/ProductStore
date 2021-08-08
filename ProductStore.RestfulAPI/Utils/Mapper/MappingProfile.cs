using AutoMapper;
using ProductStore.RestfulAPI.ViewModel;
using ProductStore.RestfulAPI.Data.Products;
using ProductStore.RestfulAPI.Data;

namespace ProductStore.RestfulAPI.Utils.Mapper
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
            //CreateMap<ProductsVM, Products>();
            //CreateMap<ProductsVM, Products>();

            //CreateMap<ProductsVM, Products>();
            //CreateMap<ProductsVM, Products>();

            //CreateMap<ProductsVM, Products>();
            //CreateMap<ProductsVM, Products>();
            //CreateMap<ProductsVM, Products>();
        }
    }

}