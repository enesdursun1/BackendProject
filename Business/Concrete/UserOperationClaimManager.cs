using AutoMapper;
using Azure.Core;
using Business.Abstract;
using Business.Dtos.Requests.Category;
using Business.Dtos.Requests.UserOperationClaim;
using Business.Dtos.Responses.OperationClaim;
using Business.Dtos.Responses.UserOperationClaim;
using Core.Security.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete;
using System.Threading;

namespace Business.Concrete;

public class UserOperationClaimManager : IUserOperationClaimService
{
    private readonly IUserOperationClaimDal _userOperationClaimDal;
    private readonly IMapper _mapper;


    public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IMapper mapper)
    {
        _userOperationClaimDal = userOperationClaimDal;
        _mapper = mapper;

    }
    public async Task<CreatedUserOperationClaimResponse> AddAsync(CreateUserOperationClaimRequest createUserOperationClaimRequest)
    {
        UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(createUserOperationClaimRequest);

        UserOperationClaim createdUserOperationClaim = await _userOperationClaimDal.AddAsync(mappedUserOperationClaim);

        CreatedUserOperationClaimResponse createdUserOperationClaimResponse = _mapper.Map<CreatedUserOperationClaimResponse>(
            createdUserOperationClaim
        );
        return createdUserOperationClaimResponse;
    }
    public async Task<UpdatedUserOperationClaimResponse> UpdateAsync(UpdateUserOperationClaimRequest updateUserOperationClaimRequest)
    {
        UserOperationClaim? userOperationClaim = await _userOperationClaimDal.GetAsync(
               predicate: uoc => uoc.Id == updateUserOperationClaimRequest.Id
               
               
           );
      
        UserOperationClaim mappedUserOperationClaim = _mapper.Map(updateUserOperationClaimRequest, destination: userOperationClaim!);

        UserOperationClaim updatedUserOperationClaim = await _userOperationClaimDal.UpdateAsync(mappedUserOperationClaim);

        UpdatedUserOperationClaimResponse updatedUserOperationClaimResponse = _mapper.Map<UpdatedUserOperationClaimResponse>(
            updatedUserOperationClaim
        );
        return updatedUserOperationClaimResponse;
    }
    public async Task<DeletedUserOperationClaimResponse> DeleteAsync(DeleteUserOperationClaimRequest deleteUserOperationClaimRequest)
    {
        UserOperationClaim? userOperationClaim = await _userOperationClaimDal.GetAsync(
              predicate: uoc => uoc.Id == deleteUserOperationClaimRequest.Id
             
          );
    
        await _userOperationClaimDal.DeleteAsync(userOperationClaim!);

        DeletedUserOperationClaimResponse response = _mapper.Map<DeletedUserOperationClaimResponse>(userOperationClaim);
        return response;
    }

    public async Task<GetByIdUserOperationClaimResponse> GetByIdAsync(GetByIdUserOperationClaimRequest getByIdUserOperationClaimRequest)
    {
        UserOperationClaim? userOperationClaim = await _userOperationClaimDal.GetAsync(
              predicate: b => b.Id == getByIdUserOperationClaimRequest.Id
              
          );
       

        GetByIdUserOperationClaimResponse userOperationClaimResponse = _mapper.Map<GetByIdUserOperationClaimResponse>(userOperationClaim);
        return userOperationClaimResponse;
    }

    public async Task<IList<GetListUserOperationClaimResponse>> GetListAsync()
    {
        var data = await _userOperationClaimDal.GetListAsync();
        IList<GetListUserOperationClaimResponse> getListUserOperationClaimResponse = _mapper.Map<IList<GetListUserOperationClaimResponse>>(data);
        return getListUserOperationClaimResponse;
    }

   
}
