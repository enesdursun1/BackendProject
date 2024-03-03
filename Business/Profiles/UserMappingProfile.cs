using AutoMapper;
using Business.Dtos.Requests.User;
using Business.Dtos.Responses.User;
using Core.Entities.Dtos;
using Core.Security.Entities;

namespace Business.Profiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
                  
        CreateMap<User, GetListUserResponse>().ReverseMap();
        CreateMap<User, CreateUserRequest>().ReverseMap();
        CreateMap<User, CreatedUserResponse>().ReverseMap();
        CreateMap<User,  UpdateUserRequest>().ReverseMap();
        CreateMap<User, UpdatedUserResponse>().ReverseMap();
        CreateMap<User,  DeleteUserRequest>().ReverseMap();
        CreateMap<User, DeletedUserResponse>().ReverseMap();
        CreateMap<User, GetByIdUserResponse>().ReverseMap();
        CreateMap<User, GetByIdUserRequest>().ReverseMap();
        CreateMap<User, UserForLoginDto>().ReverseMap();
        CreateMap<User, UserForRegisterDto>().ReverseMap();




    }

}
