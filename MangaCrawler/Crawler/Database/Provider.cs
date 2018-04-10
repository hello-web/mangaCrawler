using Dapper.Contrib.Extensions;
using MangaCrawler.Crawler.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Database
{
    abstract class Provider : IProvider
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsEnabled { get; set; }

        public abstract Task<ICollection<IManga>> GetList();
        public void Save()
        {
            using (var conn = new Connector().GetConnection())
            {
                conn.Insert(this);
            }
        }
    }
}
