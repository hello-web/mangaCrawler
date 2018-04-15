using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            this.basePath = basePath;
            this.baseUrl = baseUrl;
            fileLookup = new Dictionary<string, string>();
            fileLookup.Add("/", "index.html");
        }

        private string GetFullpath(string filename)
        {
            var fname = filename[0] == '\\' ? filename.Substring(1) : filename;
            var wwwDir = Path.Combine(basePath, "www\\", fname);
            var cacheDir = Path.Combine(basePath, "cache\\", fname);

            if (File.Exists(wwwDir))
                return wwwDir;
            if (File.Exists(cacheDir))
                return cacheDir;
            
            return "";
        }

        private string GetPath(string url)
        {
            var patern = new Regex("#.+");
            var path = url.Replace(baseUrl, "");
            var cleanPath = patern.Replace(path, "");

            return cleanPath;
        }

        private string LookupFile(string path)
        {
            if (fileLookup.ContainsKey(path))
                return GetFullpath(fileLookup[path]);

            string cleanPath = path.Replace('/', '\\');
            string fullPath = GetFullpath(cleanPath);

            return fullPath;
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
