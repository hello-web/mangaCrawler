using Dapper.Contrib.Extensions;
using MangaCrawler.Crawler.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Database
{
    [Table("chapter")]
    class Chapter
    {
        public ulong Id { get; set; }
        public ulong IdManga { get; set; }
        public string Title { get; set; }
        public uint Num { get; set; }
        public string Url { get; set; }
        public string ThumbUrl { get; set; }
        public string Thumb { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? CreateAt { get; set; }

        public static void AddChapter(IChapter chapter, IManga manga)
        {
            var chapterT = new Chapter()
            {
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                ThumbUrl = chapter.ThumbLink,
                Url = chapter.Url,
                Title = chapter.Title,
                Num = chapter.ChapterNum,
                IdManga = manga.Id,
            };

            using (var conn = new Connector().GetConnection())
            {
                conn.Insert(chapterT);
            }
        }
    }
}
