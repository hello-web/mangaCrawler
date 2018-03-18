﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Data
{
    interface IManga
    {
        string Title { get; set; }
        string ThumbLink { get; set; }
        string MangaLink { get; set; }

        Image GetThumbnail();
    }
}
