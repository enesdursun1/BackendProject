using Core.DataAccess.Repositories;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface ICategoryDal : IAsyncRepository<Category, int>, IRepository<Category, int>
{
}
