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
    class Page
    {
        public ulong Id { get; set; }
        public ulong IdChapter { get; set; }
        public uint Num { get; set; }
        public string Url { get; set; }
        public string Path { get; set; }
        public bool IsDownloaded { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DownloadAt { get; set; }

        public static void AddPage(IPage page, IChapter chapter)
        {
            var pageT = new Page()
            {
                UpdateAt = DateTime.Now,
                IdChapter = chapter.Id,
                Num = page.PageNum,
                Url = page.PageLink,
                Path = page.PagePath,
            };

            using (var conn = new Connector().GetConnection())
            {
                conn.Insert(pageT);
            }
        }
    }
}
