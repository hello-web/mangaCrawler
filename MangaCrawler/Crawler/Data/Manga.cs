using LiteDB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Data
{
    abstract class Manga : IManga
    {
        public string Title { get; set; }
        public string ThumbLink { get; set; }
        public string MangaLink { get; set; }

        public abstract ICollection<IChapter> GetChapters();
        public abstract IDictionary<string, object> GetMetas();

        public Image GetThumbnail()
        {
            var coll = Cache.GetCollectionCache();
            var cache = coll.FindOne(x => x.UrlImage == ThumbLink);

            if (cache == null)
            {
                //Schedule for Download Image
                Job.JobScheduler.PushJob(new Job.JobDescription()
                {
                    AfterDownload = AfterDownload,
                    UrlDownload = ThumbLink,
                });

                return new Bitmap(230, 360);
            } else
            {
                return cache.GetImage();
            }
        }

        private void AfterDownload(string filename, bool is_success)
        {
            if (is_success)
            {
                var coll = Cache.GetCollectionCache();
                var ext = Path.GetExtension(ThumbLink);
                var newName = filename + ext;

                File.Move(filename, newName);

                coll.Insert(new CacheImg()
                {
                    UrlImage = ThumbLink,
                    NameImage = newName,
                });
            }
        }
    }
}
