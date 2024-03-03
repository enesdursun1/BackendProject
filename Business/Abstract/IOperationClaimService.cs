using Business.Dtos.Requests.OperationClaim;
using Business.Dtos.Responses.OperationClaim;

namespace Business.Abstract;

public interface IOperationClaimService
{
    public Task<CreatedOperationClaimResponse> AddAsync(CreateOperationClaimRequest createOperationClaimRequest);
    public Task<IList<GetListOperationClaimResponse>> GetListAsync();
    public Task<GetByIdOperationClaimResponse> GetByIdAsync(GetByIdOperationClaimRequest getByIdOperationClaimRequest);
    public Task<UpdatedOperationClaimResponse> UpdateAsync(UpdateOperationClaimRequest updateOperationClaimRequest);
    public Task<DeletedOperationClaimResponse> DeleteAsync(DeleteOperationClaimRequest deleteOperationClaimRequest);
}
