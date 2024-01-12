namespace Core.DataAccess.Repositories;

public interface IQuery<TEntity>
{
    IQueryable<TEntity> Query();
}
