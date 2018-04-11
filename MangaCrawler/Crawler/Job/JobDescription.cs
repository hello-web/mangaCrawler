using MangaCrawler.Crawler.Data;
using MangaCrawler.Crawler.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Job
{
    class JobDescription
    {
        public string UrlDownload { get; set; }
        public IUpdateThumb Entity { get; set; }
        
        public async void Download()
        {
            var tmpName = Guid.NewGuid().ToString();
            var tmpPath = Path.Combine(Program.CachePath, tmpName);

            try
            {
                var download = await HttpDownloader.GetResponseAsync(HttpMethod.Get, UrlDownload);
                
                if (download.IsSuccessStatusCode)
                {
                    using (var fs = new FileStream(tmpPath, FileMode.CreateNew, FileAccess.Write))
                    {
                        await download.Content.CopyToAsync(fs);
                        await fs.FlushAsync();
                    }

                    UpdateDatabase(tmpName);
                    return;
                }
            } catch { }

            UpdateDatabase("");
        }

        public void UpdateDatabase(string filename)
        {
            Entity.SetThumbnail(filename);
        }
    }
}
