using Core.DataAccess.Repositories;
using Core.Security.Entities;
using DataAccess.Abstract;
using DataAccess.Contexts;

namespace DataAccess.Concrete;

public class EfUserDal : EfRepositoryBase<User, int, BaseDbContext>, IUserDal
{
    public EfUserDal(BaseDbContext context) : base(context)
    {
    }
}
