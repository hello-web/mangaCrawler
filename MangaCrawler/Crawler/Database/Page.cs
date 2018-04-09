using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Database
{
    class Page
    {
        public ulong Id { get; set; }
        public ulong IdChapter { get; set; }
        public uint Num { get; set; }
        public string Url { get; set; }
        public bool IsDownloaded { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DownloadAt { get; set; }
    }
}
