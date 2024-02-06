using Core.DataAccess.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract;

public interface IUserDal : IAsyncRepository<User,int> ,IRepository<User,int>
{
}
