using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Data
{
    public interface IChapter
    {
        ulong Id { get; set; }
        ulong IdManga { get; set; }
        string Title { get; set; }
        string Thumb { get; set; }
        string ThumbUrl { get; set; }
        string Url { get; set; }
        uint Num { get; set; }
        DateTime? UpdateAt { get; set; }
        DateTime? CreateAt { get; set; }
        ICollection<IPage> Pages { get; set; }

        void Save();
        Image GetThumbnail();
        Task<ICollection<IPage>> GetPages();
    }
}
