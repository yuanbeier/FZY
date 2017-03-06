using System;
using System.Web;
using System.Collections;
using System.IO;

namespace Topevery.FMP.ObjectModel.Web.Handlers
{
	internal class Mime 
	{
        private const string MimeFileName = "MIME.config";
		private static System.Collections.Hashtable ht = null;
        static Mime() 
		{
			ht = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MimeFileName);
            Stream s = null;
            if (File.Exists(fileName))
            {
                s = File.OpenRead(fileName);
            }
            else
            {
                System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
                s = a.GetManifestResourceStream(a.GetName().Name + ".Services.Web.Handlers.MIME.config");
            }

			if (s == null) 
			{
				throw new Exception("Can't find MIME mapping!");
			}

			System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
			xd.Load(s);

			System.Xml.XmlNodeList xnl = xd.SelectNodes("mimes/mime");

			string tempExt = null;
			foreach (System.Xml.XmlNode xn in xnl) 
			{
				tempExt = xn.Attributes["ext"].InnerText.ToLower();
				if (!ht.Contains(tempExt)) 
				{
					ht.Add(tempExt, xn.Attributes["type"].InnerText);
				}
			}
		}

        public static string GetContentTypeNotDefault(string ext)
        {
            if (!string.IsNullOrEmpty(ext))
            {
                return ht[ext] as string;
            }
            return null;
        }

		public static string GetContenType(string ext) 
		{
			if (ext == null) 
			{
				return @"application/octet-stream";
			}

			string contentType = ht[ext] as string;
			if (contentType == null) 
			{
				return @"application/octet-stream";
			}
			return contentType;
		}
	}
}
