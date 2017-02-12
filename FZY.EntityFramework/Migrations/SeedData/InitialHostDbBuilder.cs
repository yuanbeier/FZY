using FZY.EntityFramework;
using EntityFramework.DynamicFilters;

namespace FZY.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly FZYDbContext _context;

        public InitialHostDbBuilder(FZYDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
