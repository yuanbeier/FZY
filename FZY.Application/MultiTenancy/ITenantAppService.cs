using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using FZY.MultiTenancy.Dto;

namespace FZY.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        ListResultDto<TenantListDto> GetTenants();

        Task CreateTenant(CreateTenantInput input);
    }
}
