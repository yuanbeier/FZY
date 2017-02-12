using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using FZY.Users.Dto;

namespace FZY.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);

        Task RemoveFromRole(long userId, string roleName);

        Task<ListResultDto<UserListDto>> GetUsers();

        Task CreateUser(CreateUserInput input);
    }
}