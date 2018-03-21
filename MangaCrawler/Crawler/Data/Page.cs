using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Data
{
    abstract class Page : IPage
    {
        public string PageLink { get; set; }
        public string PagePath { get; set; }
        public int PageNum { get; set; }

        public abstract bool DownloadPage(string filename);
    }
}
