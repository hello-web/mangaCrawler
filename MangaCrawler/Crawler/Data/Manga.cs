using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Data
{
    abstract class Manga : IManga
    {
        public abstract string Title { get; set; }
        public abstract string ThumbLink { get; set; }
        public abstract string MangaLink { get; set; }

        public Image GetThumbnail()
        {
            return new Bitmap(100, 200);
        }
    }
}
