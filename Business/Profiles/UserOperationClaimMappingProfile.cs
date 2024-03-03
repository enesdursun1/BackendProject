using AutoMapper;
using Business.Dtos.Requests.UserOperationClaim;
using Business.Dtos.Responses.UserOperationClaim;
using Core.Security.Entities;

namespace Business.Profiles;

public class UserOperationClaimMappingProfile : Profile
{
    public UserOperationClaimMappingProfile()
    {

        CreateMap<UserOperationClaim, GetListUserOperationClaimResponse>().ReverseMap();
        CreateMap<UserOperationClaim,  CreateUserOperationClaimRequest>().ReverseMap();
        CreateMap<UserOperationClaim, CreatedUserOperationClaimResponse>().ReverseMap();
        CreateMap<UserOperationClaim,  UpdateUserOperationClaimRequest>().ReverseMap();
        CreateMap<UserOperationClaim, UpdatedUserOperationClaimResponse>().ReverseMap();
        CreateMap<UserOperationClaim,  DeleteUserOperationClaimRequest>().ReverseMap();
        CreateMap<UserOperationClaim, DeletedUserOperationClaimResponse>().ReverseMap();
        CreateMap<UserOperationClaim, GetByIdUserOperationClaimResponse>().ReverseMap();
        CreateMap<UserOperationClaim, GetByIdUserOperationClaimRequest>().ReverseMap();




    }

}
