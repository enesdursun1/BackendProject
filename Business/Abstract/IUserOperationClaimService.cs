using Business.Dtos.Requests.Category;
using Business.Dtos.Requests.UserOperationClaim;
using Business.Dtos.Responses.UserOperationClaim;

namespace Business.Abstract;

public interface IUserOperationClaimService
{
    public Task<CreatedUserOperationClaimResponse> AddAsync(CreateUserOperationClaimRequest createUserOperationClaimRequest);
    public Task<IList<GetListUserOperationClaimResponse>> GetListAsync();
    public Task<GetByIdUserOperationClaimResponse> GetByIdAsync(GetByIdUserOperationClaimRequest getByIdUserOperationClaimRequest);
    public Task<UpdatedUserOperationClaimResponse> UpdateAsync(UpdateUserOperationClaimRequest updateUserOperationClaimRequest);
    public Task<DeletedUserOperationClaimResponse> DeleteAsync(DeleteUserOperationClaimRequest deleteUserOperationClaimRequest);
}
