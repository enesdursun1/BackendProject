using AutoMapper;
using Business.Dtos.Requests.Product;
using Business.Dtos.Responses.Product;
using Entities.Concrete;

namespace Business.Profiles;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, GetListProductResponse>()
            .ForMember(destinationMember: p => p.CategoryName,
                       memberOptions: opt => opt.MapFrom(p => p.Category.Name))
             .ForMember(destinationMember: p => p.CategoryId,
                       memberOptions: opt => opt.MapFrom(p => p.Category.Id))
            .ReverseMap();



        CreateMap<Product, CreateProductRequest>().ReverseMap();
        CreateMap<Product, CreatedProductResponse>().ReverseMap();

        CreateMap<Product, UpdateProductRequest>().ReverseMap();
        CreateMap<Product, UpdatedProductResponse>().ReverseMap();

        CreateMap<Product, DeleteProductRequest>().ReverseMap();
        CreateMap<Product, DeletedProductResponse>().ReverseMap();

        CreateMap<Product, GetByIdProductRequest>().ReverseMap();

        CreateMap<Product, GetByIdProductResponse>()
            .ForMember(destinationMember: p => p.CategoryId,
            memberOptions: opt => opt.MapFrom(p => p.Category.Id))
             .ForMember(destinationMember: p => p.CategoryName,
             memberOptions: opt => opt.MapFrom(p => p.Category.Name))
            .ReverseMap();
    }

}