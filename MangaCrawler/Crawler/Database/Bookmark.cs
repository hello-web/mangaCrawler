using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Database
{
    class Bookmark
    {
        public long Id { get; set; }
        public long IdManga { get; set; }
        public byte Flag { get; set; }
    }

    enum BookmarkFlag
    {
        PlanToRead,
        StillRead,
        FinishRead,
        Favorite,
    }
}
