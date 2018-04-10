using Dapper.Contrib.Extensions;
using MangaCrawler.Crawler.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Database
{
    [Table("page")]
    abstract class Page : IPage
    {
        [Key]
        public ulong Id { get; set; }
        public ulong IdChapter { get; set; }
        public uint Num { get; set; }
        public string Url { get; set; }
        public string Path { get; set; }
        public bool IsDownloaded { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DownloadAt { get; set; }

        public abstract Task<bool> DownloadPage(string filename);
        public void Save()
        {
            UpdateAt = DateTime.Now;

            using (var conn = Connector.GetConnection())
            {
                conn.Insert(this);
            }
        }
    }
}
