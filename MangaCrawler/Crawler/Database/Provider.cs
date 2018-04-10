using Dapper;
using Dapper.Contrib.Extensions;
using MangaCrawler.Crawler.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Database
{
    [Table("provider")]
    abstract class Provider : IProvider
    {
        [Key]
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsEnabled { get; set; }

        public abstract Task<IEnumerable<IManga>> GetList();
        public void Save()
        {
            using (var conn = Connector.GetConnection())
            {
                conn.Insert(this);
            }
        }
        public void GetId()
        {
            using (var conn = Connector.GetConnection())
            {
                var sql = "SELECT id FROM provider WHERE name = @Name";
                var param = new { Name };
                var id = conn.QuerySingle<uint>(sql, param);

                Id = id;
            }
        }
    }
}
