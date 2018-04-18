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
    class Provider : IProvider
    {
        [Key]
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsEnabled { get; set; }

        public virtual Task<IEnumerable<IManga>> GetMangas(bool update = false)
        {
            return Task.Run<IEnumerable<IManga>>(() => new List<IManga>());
        }
        public virtual Task<IManga> GetManga(int Id)
        {
            return Task.Run<IManga>(() => new Manga() );
        }
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
                try
                {
                    var sql = "SELECT id FROM provider WHERE name = @Name";
                    var param = new { Name };
                    var id = conn.QuerySingle<uint>(sql, param);

                    Id = id;
                } catch
                {
                    // Provider not registed yet
                }
            }
        }
    }
}
