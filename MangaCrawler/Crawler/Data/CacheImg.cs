using LiteDB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Data
{
    public class CacheImg
    {
        public string UrlImage { get; set; }
        
        /**
         * Image name use GUID to save
         **/
        [BsonId]
        public string NameImage { get; set; }

        [BsonIgnore]
        public string PathImage
        {
            get
            {
                return Path.Combine(Program.CachePath, NameImage);
            }
        }

        public Image GetImage()
        {
            return Image.FromFile(PathImage);
        } 
    }

    public static class Cache
    {
        private static LiteDatabase db;
        private static ConnectionString connStr
        {
            get
            {
                var connStr = new ConnectionString()
                {
                    Filename = Program.LitePath,
                    Flush = false,
                    Upgrade = true,
                    Mode = LiteDB.FileMode.Shared,
                };

                return connStr;
            }
        }

        static Cache()
        {
            db = new LiteDatabase(connStr);
        }

        public static LiteCollection<CacheImg> GetCollectionCache()
        {
            return db.GetCollection<CacheImg>();
        }
    }
}
