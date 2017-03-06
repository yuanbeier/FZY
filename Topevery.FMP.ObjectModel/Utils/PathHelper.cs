using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace Topevery.FMP.ObjectModel
{
	/// <summary>
	/// PathHelper 的摘要说明。
	/// </summary>
    [ComVisible(false)]
	internal class PathHelper
	{
		public static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
		private PathHelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region Methods
		public static string ResolvePath(string path)
		{
			string result = null;
            if (!Utils.IsEmpty(path))
			{
				
				if(path.StartsWith("~/") || path.StartsWith("~\\"))
				{
					result = BaseDirectory + path.Substring(2);
				}
				else
				{
					result = path;
				}
			}
			return result;
		}
		
		public static string CombineIOPath(params string[] paths)
		{
            if (!Utils.IsEmpty(paths))
			{
				for(int i = 0; i < paths.Length; i++)
				{
					paths[i] = ResolvePath(paths[i]);
				}
				string result = string.Join("\\", paths);
                if (!Utils.IsEmpty(result))
				{
					result = result.Replace("/","\\");
					int index = result.IndexOf(@"\\");
					while(index != -1)
					{
						result = result.Replace(@"\\",@"\");
						index = result.IndexOf(@"\\");
					}
				}
				return result;
			}
			return null;
		}

		public static string CombineHttpPath(params string[] paths)
		{
            if (!Utils.IsEmpty(paths))
			{
				for(int i = 0; i < paths.Length; i++)
				{
					paths[i] = ResolvePath(paths[i]);
				}
				string result = string.Join("/", paths);
                if (!Utils.IsEmpty(result))
				{
					result = result.Replace("\\","/");
					/*int index = result.IndexOf(@"//");
					while(index != -1)
					{
						result = result.Replace(@"//",@"/");
						index = result.IndexOf(@"//");
					}*/
				}
				return result;
			}
			return null;
		}
		#endregion
	}
}
