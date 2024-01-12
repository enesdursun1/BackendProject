using Core.DataAccess.Repositories;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete;

public class EfProductDal : EfRepositoryBase<Product, int, BaseDbContext>, IProductDal
{
    public EfProductDal(BaseDbContext context) : base(context)
    {
    }
}
