using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using FZY.Sys.Dto;

namespace FZY.Sys
{
    /// <summary>
    /// 文件关系服务
    /// 袁贝尔
    /// 20160923
    /// </summary>
    public interface IFileRelationAppService:IApplicationService
    {
        /// <summary>
        /// 获取文件类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<FileRDto>> GetFileRDtoList(FileRInput input);
    }
}
