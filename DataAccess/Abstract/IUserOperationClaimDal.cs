using Core.DataAccess.Repositories;
using Core.Security.Entities;

namespace DataAccess.Abstract;

public interface IUserOperationClaimDal : IAsyncRepository<UserOperationClaim, int> ,IRepository<UserOperationClaim, int>
{
    Task<IList<OperationClaim>> GetOperationClaimsByUserIdAsync(int userId);

}
