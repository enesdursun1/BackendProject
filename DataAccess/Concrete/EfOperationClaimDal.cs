using Core.DataAccess.Repositories;
using Core.Security.Entities;
using DataAccess.Abstract;
using DataAccess.Contexts;

namespace DataAccess.Concrete;

public class EfOperationClaimDal: EfRepositoryBase<OperationClaim, int, BaseDbContext> ,IOperationClaimDal
{
    public EfOperationClaimDal(BaseDbContext context) : base(context)
    {
    }
}
