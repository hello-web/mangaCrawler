using MangaCrawler.Crawler.Data;
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

        public Action<string, bool> AfterDownload { get; set; }

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
                        fs.Close();
                    }

                    AfterDownload?.Invoke(tmpPath, true);
                } else
                {
                    AfterDownload?.Invoke(null, false);
                }
            } catch
            {
                AfterDownload?.Invoke(null, false);
            }
        }
    }
}
