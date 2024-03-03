using AutoMapper;
using Business.Dtos.Requests.OperationClaim;
using Business.Dtos.Responses.OperationClaim;
using Core.Security.Entities;

namespace Business.Profiles;

public class OperationClaimMappingProfile : Profile
{
    public OperationClaimMappingProfile()
    {

        CreateMap<OperationClaim, GetListOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim,  CreateOperationClaimRequest>().ReverseMap();
        CreateMap<OperationClaim, CreatedOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim,  UpdateOperationClaimRequest>().ReverseMap();
        CreateMap<OperationClaim, UpdatedOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim,  DeleteOperationClaimRequest>().ReverseMap();
        CreateMap<OperationClaim, DeletedOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim, GetByIdOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim, GetByIdOperationClaimRequest>().ReverseMap();
  



    }

}
