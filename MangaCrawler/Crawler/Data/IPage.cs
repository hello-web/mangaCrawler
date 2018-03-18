using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Data
{
    interface IPage
    {
        string PageLink { get; set; }
        int PageNum { get; set; }
    }
}
