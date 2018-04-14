using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.App
{
    class AppScheme : ISchemeHandlerFactory
    {
        string baseUrl;
        string basePath;
        IDictionary<string, string> fileLookup;

        public AppScheme(string basePath, string baseUrl)
        {
            this.basePath = Path.Combine(basePath, "www\\");
            this.baseUrl = baseUrl;
            fileLookup = new Dictionary<string, string>();
            fileLookup.Add("/", "index.html");
        }

        private string GetFullpath(string filename)
        {
            if (filename[0] == '\\')
                return Path.Combine(basePath, filename.Substring(1));

            return Path.Combine(basePath, filename);
        }

        private string GetPath(string url)
        {
            return url.Replace(baseUrl, "");
        }

        private string LookupFile(string path)
        {
            if (fileLookup.ContainsKey(path))
                return GetFullpath(fileLookup[path]);

            string cleanPath = path.Replace('/', '\\');
            string fullPath = GetFullpath(cleanPath);

            if (File.Exists(fullPath))
                return fullPath;

            return "";
        }

        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            string url = request.Url;
            string path = GetPath(url);
            string pathFile = LookupFile(path);

            if (pathFile != "")
            {
                string extension = new FileInfo(pathFile).Extension;
                string mimeType = ResourceHandler.GetMimeType(extension);

                return ResourceHandler.FromFilePath(pathFile, mimeType);
            }

            return ResourceHandler.FromString("");
        }
    }
}
