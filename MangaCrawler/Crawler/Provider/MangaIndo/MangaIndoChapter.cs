using AngleSharp.Dom.Html;
using MangaCrawler.Crawler.Data;
using MangaCrawler.Crawler.Database;
using Dapper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Provider.MangaIndo
{
    class MangaIndoChapter : Chapter
    {
        public override async Task<IEnumerable<IPage>> GetPages(bool update = false)
        {
            var serv = await GetFromDatabase();

            // Check database or need to update
            if (!serv.Any() || update)
            {
                await GetFromServer();

                return await GetFromDatabase();
            }

            return serv;
        }

        private async Task GetFromServer()
        {
            using (var conn = Connector.GetConnection())
            {
                var sql = "DELETE FROM page WHERE IdChapter = @chapter";
                var param = new { chapter = this.Id };
                var query = await conn.ExecuteAsync(sql, param);
            }

            //start crawling web
            uint counter = 1;
            var crawler = new DomCrawler();
            var stream = await HttpDownloader.GetAsync(HttpMethod.Get, Url);

            await crawler.LoadHtmlAsync(stream);

            var elements = crawler.Query("#readerarea img");

            foreach (IHtmlImageElement element in elements)
            {
                try
                {
                    var page = new MangaIndoPage()
                    {
                        IdChapter = this.Id,
                        Num = ++counter,
                        Url = element.Source,
                    };

                    page.Save();
                }
                catch (Exception ex)
                {
                    //
                }
            }
        }
        private async Task<IEnumerable<IPage>> GetFromDatabase()
        {
            using (var conn = Connector.GetConnection())
            {
                try
                {
                    var sql = "SELECT * FROM page WHERE IdChapter = @chapter";
                    var param = new { chapter = this.Id };
                    var query = await conn.QueryAsync<MangaIndoPage>(sql, param);

                    return query;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
