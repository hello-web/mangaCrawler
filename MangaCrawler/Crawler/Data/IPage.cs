using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Data
{
    public interface IPage
    {
        string PageLink { get; set; }
        string PagePath { get; set; }
        int PageNum { get; set; }

        bool DownloadPage(string filename);
    }
}
