using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Database
{
    [Table("download")]
    class Download
    {
        public long Id { get; set; }
        public long IdChapter { get; set; }
        public bool IsDownload { get; set; }
    }

    enum DownloadStatus
    {
        Stopped,
        Paused,
        Downloading,
        Finished,
    }
}
