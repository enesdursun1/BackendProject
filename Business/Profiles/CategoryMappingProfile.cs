using AutoMapper;
using Business.Dtos.Requests.Category;
using Business.Dtos.Responses.Category;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Profiles;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {

        CreateMap<Category, GetListCategoryResponse>().ReverseMap();



        CreateMap<Category, CreateCategoryRequest>().ReverseMap();
        CreateMap<Category, CreatedCategoryResponse>().ReverseMap();

        CreateMap<Category, UpdateCategoryRequest>().ReverseMap();
        CreateMap<Category, UpdatedCategoryResponse>().ReverseMap();

        CreateMap<Category, DeleteCategoryRequest>().ReverseMap();
        CreateMap<Category, DeletedCategoryResponse>().ReverseMap();

        CreateMap<Category, GetByIdCategoryResponse>().ReverseMap();
        CreateMap<Category, GetByIdCategoryRequest>().ReverseMap();




    }

}
