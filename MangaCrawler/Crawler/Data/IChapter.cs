﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Data
{
    interface IChapter
    {
        string Title { get; set; }
        string ThumbLink { get; set; }
        string ChapterLink { get; set; }
        int ChapterNum { get; set; }

        Image GetThumbnail();
        ICollection<IPage> GetPages();
    }
}