using Core.DataAccess.Repositories;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete;

public class EfCategoryDal : EfRepositoryBase<Category, int, BaseDbContext>, ICategoryDal
{
    public EfCategoryDal(BaseDbContext context) : base(context)
    {
    }
}
