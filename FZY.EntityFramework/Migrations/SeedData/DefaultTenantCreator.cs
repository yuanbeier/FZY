using System.Linq;
using FZY.EntityFramework;
using FZY.MultiTenancy;

namespace FZY.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly FZYDbContext _context;

        public DefaultTenantCreator(FZYDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
