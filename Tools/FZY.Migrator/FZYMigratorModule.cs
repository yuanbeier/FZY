using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using FZY.EntityFramework;

namespace FZY.Migrator
{
    [DependsOn(typeof(FZYDataModule))]
    public class FZYMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<FZYDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}