using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Database
{
    class Chapter
    {
        public ulong Id { get; set; }
        public ulong IdManga { get; set; }
        public string Title { get; set; }
        public uint Num { get; set; }
        public string Url { get; set; }
        public string Thumb { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}
