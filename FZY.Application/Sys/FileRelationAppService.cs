using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using FZY.Common;
using FZY.Sys.Dto;

namespace FZY.Sys
{
    /// <summary>
    /// 文件关系服务
    /// </summary>
    public class FileRelationAppService : FZYAppServiceBase, IFileRelationAppService
    {

        private readonly IRepository<FileRelation> _iFileRelationRepository;

        public FileRelationAppService(IRepository<FileRelation> iFileRelationRepository)
        {
            _iFileRelationRepository = iFileRelationRepository;
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<FileRDto>> GetFileRDtoList(FileRInput input)
        {
            var query = _iFileRelationRepository.GetAll();
            if (input.ModuleType.HasValue)
            {
               query = query.Where(x => x.ModuleId == input.ModuleType);
            }
            if (input.KeyId.HasValue)
            {
               query = query.Where(x => x.KeyId == input.KeyId);
            }
          
            var listDto = (await query.ToListAsync()).MapTo<List<FileRDto>>();
            string applicationRootUrl = ApplicationRoot.EndsWith("/") ? ApplicationRoot : ApplicationRoot + "/";
            foreach (var dto in listDto)
            {
                string ext = FileHelper.GetExtension(dto.FileName).ToLower();
                dto.FileNameWithoutExten = dto.FileName.Substring(0, dto.FileName.IndexOf('.'));
                switch (ext)
                {
                    case ".xlsx":
                    case ".xls":
                        dto.ImageShowUrl = applicationRootUrl + "Images/Excel.png";
                        break;
                    case ".doc":
                    case ".docx":
                        dto.ImageShowUrl = applicationRootUrl + "Images/Word.png";
                        break;
                    case ".ppt":
                    case ".pptx":
                        dto.ImageShowUrl = applicationRootUrl + "Images/PPT.png";
                        break;
                    case ".txt":
                        dto.ImageShowUrl = applicationRootUrl + "Images/Word.png";
                        break;
                    case ".pdf":
                        dto.ImageShowUrl = applicationRootUrl + "Images/pdf.png";
                        break;
                }
                switch (dto.FileType)
                {
                    case FileType.Image:
                        dto.ImageShowUrl = applicationRootUrl + "Ashx/ThumbImage.ashx?FID=" +dto.FileId;
                        break;
                }
                dto.FileDownUrl = applicationRootUrl + "Ashx/DownloadFile.ashx?FID=" +dto.FileId + "&CID=" + dto.FileName;
            }
            return listDto;
        }
    }
}
