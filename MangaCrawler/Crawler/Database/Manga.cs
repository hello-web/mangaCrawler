using MangaCrawler.Crawler.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Drawing;
using System.IO;

namespace MangaCrawler.Crawler.Database
{
    [Table("manga")]
    abstract class Manga : IManga
    {
        [Key]
        [Computed]
        public ulong Id { get; set; }
        public uint IdProvider { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbUrl { get; set; }
        public string Thumb { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? CreateAt { get; set; }

        public void Save()
        {
            if (!CreateAt.HasValue) CreateAt = DateTime.Now;
            UpdateAt = DateTime.Now;

            using (var conn = new Connector().GetConnection())
            {
                conn.Insert(this);
            }
        }
        public abstract Task<ICollection<IChapter>> GetChapters();
        public abstract Task<IDictionary<string, object>> GetMetas();
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


                coll.Insert(new CacheImg()
                {
                    UrlImage = ThumbUrl,
                    NameImage = newName,
                });
            }
        }
    }
}
