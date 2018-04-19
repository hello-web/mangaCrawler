using MangaCrawler.Crawler.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Provider.MangaIndo
{
    class MangaIndoPage : Page
    {
        public override Task<bool> DownloadPage(string filename)
        {
            return Task.Run(() => true);
        }
    }
}
