using Core.DataAccess.Repositories;
using Core.Security.Entities;

namespace DataAccess.Abstract;

public interface IOperationClaimDal : IAsyncRepository<OperationClaim, int> ,IRepository<OperationClaim, int>
{
}
