using System.Data.Common;
using Abp.Zero.EntityFramework;
using FZY.Authorization.Roles;
using FZY.MultiTenancy;
using FZY.Users;
using System.Data.Entity;
using FZY.Sys;
using FZY.WebSite;

namespace FZY.EntityFramework
{
    public class FZYDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        public IDbSet<HomePic> HomePic { set; get; }

        public IDbSet<FileRelation> FileRelation { set; get; }

        public IDbSet<Product> Product { set; get;}

        public IDbSet<Category> Category { set; get; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public FZYDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in FZYDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of FZYDbContext since ABP automatically handles it.
         */
        public FZYDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public FZYDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}
