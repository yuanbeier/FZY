using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace FZY.EntityFramework.Repositories
{
    public abstract class FZYRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<FZYDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected FZYRepositoryBase(IDbContextProvider<FZYDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class FZYRepositoryBase<TEntity> : FZYRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected FZYRepositoryBase(IDbContextProvider<FZYDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
