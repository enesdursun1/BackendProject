using Core.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Repositories
{
    public interface IRepository<TEntity, TEntityId> : IQuery<TEntity>
        where TEntity : Entity<TEntityId>
    {

        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);


        TEntity Get(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>
        , IIncludableQueryable<TEntity, object>>? include = null);

        IList<TEntity> GetList(Expression<Func<TEntity, bool>>? predicate = null,
                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                             Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);




    }
}
