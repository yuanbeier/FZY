using System.IO;

namespace FZY.Common
{
    public enum FileType
    {
        /// <summary>
        /// 图片
        /// </summary>
        Image,
        /// <summary>
        /// 文件
        /// </summary>
        File,
        /// <summary>
        /// 视频
        /// </summary>
        Video,
        /// <summary>
        /// 声音
        /// </summary>
        Sound,
        /// <summary>
        /// 未知类型
        /// </summary>
        None
    }

    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 附件类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static FileType GetAttachType(string type)
        {
            FileType attachType = FileType.None;
            switch (type.ToLower())
            {
                case ".jpg":
                case ".gif":
                case ".png":
                case ".bmp":
                case ".jpeg":
                    attachType = FileType.Image;
                    break;
                case ".wav":
                case ".mp3":
                case ".wma":
                case ".ram":
                case ".mts":
                    attachType = FileType.Sound;
                    break;
                case ".mpeg":
                case ".rm":
                case ".wmv":
                case ".avi":
                case ".mp4":
                    attachType = FileType.Video;
                    break;
                case ".xlxs":
                case ".xls":
                case ".doc":
                case ".docx":
                case ".ppt":
                case ".pptx":
                case ".txt":
                case ".pdf":
                    attachType = FileType.File;
                    break;
            }

            return attachType;
        }

        /// <summary>
        /// 获取文件后缀名
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetExtension(string file)
        {
            return Path.GetExtension(file);
        }
    }
}
