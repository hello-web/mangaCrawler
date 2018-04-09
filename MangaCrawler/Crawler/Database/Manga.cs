using MangaCrawler.Crawler.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace MangaCrawler.Crawler.Database
{
    [Table("manga")]
    class Manga
    {
        [Key]
        [Computed]
        public ulong Id { get; set; }
        public uint IdProvider { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Thumb { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? CreateAt { get; set; }

        public static void AddManga(IManga manga)
        {
            var mangaE = new Manga()
            {
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                IdProvider = 1,
                Title = manga.Title,
                Url = manga.MangaLink,
            };

            using (var conn = new Connector().GetConnection())
            {
                conn.Insert(mangaE);
            }
        }
    }
}
