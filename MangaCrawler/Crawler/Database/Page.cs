﻿using Dapper.Contrib.Extensions;
using MangaCrawler.Crawler.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Database
{
    [Table("page")]
    class Page : IPage
    {
        [Key]
        public ulong Id { get; set; }
        public ulong IdChapter { get; set; }
        public uint Num { get; set; }
        public string Url { get; set; }
        public string Path { get; set; }
        public bool IsDownload { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? DownloadAt { get; set; }

        public virtual Task<bool> DownloadPage(string filename)
        {
            return Task.Run(() => true);
        }
        public void Save()
        {
            CreateAt = DateTime.Now;

            using (var conn = Connector.GetConnection())
            {
                conn.Insert(this);
            }
        }
    }
}
