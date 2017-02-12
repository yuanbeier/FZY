using System.Threading.Tasks;
using Abp.Application.Services;
using FZY.Roles.Dto;

namespace FZY.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
