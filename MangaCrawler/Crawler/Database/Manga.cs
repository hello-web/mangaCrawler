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
    class Manga : IManga, IUpdateThumb
    {
        [Key]
        public ulong Id { get; set; }
        public uint IdProvider { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbUrl { get; set; }
        public string Thumb { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? CreateAt { get; set; }

        public bool Save()
        {
            if (!CreateAt.HasValue) CreateAt = DateTime.Now;
            UpdateAt = DateTime.Now;

            using (var conn = Connector.GetConnection())
            {
                try
                {
                    conn.Insert(this);
                    return true;
                } catch
                {
                    return false;
                }
            }
        }
        public virtual Task<IEnumerable<IChapter>> GetChapters(bool update = false)
        {
            return Task.Run<IEnumerable<IChapter>>(() => new List<IChapter>());
        }
        public virtual Task<IDictionary<string, object>> GetMetas()
        {
            return Task.Run<IDictionary<string, object>>(() => new Dictionary<string, object>());
        }
        public async void SetThumbnail(string filename)
        {
            Thumb = filename;
            UpdateAt = DateTime.Now;

            using (var conn = Connector.GetConnection())
            {
                try
                {
                    await conn.UpdateAsync(this);
                } catch { }
            }
        } 
        public Image GetThumbnail()
        {
            if (string.IsNullOrEmpty(Thumb))
                return new Bitmap(230, 360);

            return GetImage();
        }
        private Image GetImage()
        {
            var path = Path.Combine(Program.CachePath, Thumb);

            return Image.FromFile(path);
        }
    }
}
