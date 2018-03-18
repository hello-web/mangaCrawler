using LiteDB;
using System;
using System.Collections.Generic;
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
    }
}
