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
        string PageLink { get; set; }
        string PagePath { get; set; }
        uint PageNum { get; set; }

        bool DownloadPage(string filename);
    }

    abstract class Page : IPage
    {
        public ulong Id { get; set; }
        public string PageLink { get; set; }
        public string PagePath { get; set; }
        public uint PageNum { get; set; }

        public abstract bool DownloadPage(string filename);
    }
}
