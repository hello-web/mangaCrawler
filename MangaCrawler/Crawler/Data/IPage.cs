using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Data
{
    public interface IPage
    {
        ulong Id { get; set; }
        ulong IdChapter { get; set; }
        string Url { get; set; }
        string Path { get; set; }
        uint Num { get; set; }
        bool IsDownloaded { get; set; }
        DateTime? UpdateAt { get; set; }
        DateTime? DownloadAt { get; set; }

        Task<bool> DownloadPage(string filename);
        void Save();
    }
}
