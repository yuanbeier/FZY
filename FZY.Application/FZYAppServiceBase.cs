using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using FZY.MultiTenancy;
using FZY.Sys;
using FZY.Users;
using Microsoft.AspNet.Identity;
using FZY.Common;

namespace FZY
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class FZYAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        public IRepository<FileRelation> FileRelationRepository { set; get; }

        protected FZYAppServiceBase()
        {
            LocalizationSourceName = FZYConsts.LocalizationSourceName;
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        #region 添加文件关系表

        /// <summary>
        /// 添加文件关系
        /// </summary>
        /// <param name="fileId">文件关系Id</param>
        /// <param name="keyId">标识Id</param>
        /// <param name="moduleType">文件类型</param>
        /// <returns></returns>
        protected async Task AddFileRelationAsync(string fileId, int keyId, ModuleType moduleType)
        {
            if (!string.IsNullOrEmpty(fileId))
            {
                string[] idStrings = fileId.Trim().TrimEnd(';').Split(';');
                for (int i = 0; i < idStrings.Length; i++)
                {
                    string[] file = idStrings[i].Split(',');
                    if (file.Length > 0)
                    {
                        FileRelation fileR = new FileRelation();
                        fileR.ModuleId = moduleType;
                        fileR.KeyId = keyId;
                        fileR.FileId = Guid.Parse(file[0]);
                        fileR.FileType = FileHelper.GetAttachType(FileHelper.GetExtension(file[1]));
                        fileR.FileName = file[1];
                        fileR.CreatorUserId = AbpSession.UserId;
                        fileR.CreationTime = DateTime.Now;
                        await FileRelationRepository.InsertAsync(fileR);
                    }
                }
            }
        }

        #endregion


        /// <summary>
        /// 应用程序根目录
        /// </summary>
        public string ApplicationRoot
        {
            get { return HttpContext.Current.Request.ApplicationPath; }
        }
    }
}