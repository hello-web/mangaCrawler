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
    class Chapter : IChapter, IUpdateThumb
    {
        [Key]
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

        public virtual Task<IEnumerable<IPage>> GetPages()
        {
            return Task.Run<IEnumerable<IPage>>(() => new List<IPage>());
        }

        public void Save()
        {
            if (!CreateAt.HasValue) CreateAt = DateTime.Now;
            UpdateAt = DateTime.Now;

            using (var conn = Connector.GetConnection())
            {
                conn.Insert(this);
            }
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
            if (Thumb == null)
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
