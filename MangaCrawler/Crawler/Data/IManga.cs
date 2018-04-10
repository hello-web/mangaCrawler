using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Data
{
    public interface IManga
    {
        ulong Id { get; set; }
        uint IdProvider { get; set; }
        string Title { get; set; }
        string ThumbUrl { get; set; }
        string Thumb { get; set; }
        string Url { get; set; }
        DateTime? UpdateAt { get; set; }
        DateTime? CreateAt { get; set; }

        bool Save();
        Image GetThumbnail();
        Task<IEnumerable<IChapter>> GetChapters();
        Task<IDictionary<string, object>> GetMetas();
    }
}
