using Dapper.Contrib.Extensions;
using MangaCrawler.Crawler.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Database
{
    [Table("chapter")]
    abstract class Chapter : IChapter
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
        public ICollection<IPage> Pages { get; set; }

        public abstract Task<ICollection<IPage>> GetPages();

        public void Save()
        {
            if (!CreateAt.HasValue) CreateAt = DateTime.Now;
            UpdateAt = DateTime.Now;

            using (var conn = new Connector().GetConnection())
            {
                conn.Insert(this);
            }
        }

        public Image GetThumbnail()
        {
            var coll = Cache.GetCollectionCache();
            var cache = coll.FindOne(x => x.UrlImage == ThumbUrl);

            if (cache == null)
            {
                //Schedule for Download Image
                Job.JobScheduler.PushJob(new Job.JobDescription()
                {
                    AfterDownload = AfterDownload,
                    UrlDownload = ThumbUrl,
                });

                return new Bitmap(230, 360);
            }
            else
            {
                return cache.GetImage();
            }
        }

        private void AfterDownload(string filename, bool is_success)
        {
            if (is_success)
            {
                var coll = Cache.GetCollectionCache();
                var ext = Path.GetExtension(ThumbUrl);
                var newName = filename + ext;

                File.Move(filename, newName);

                coll.Insert(new CacheImg()
                {
                    UrlImage = ThumbUrl,
                    NameImage = newName,
                });
            }
        }
    }
}
