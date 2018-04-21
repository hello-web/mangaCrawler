using AngleSharp.Dom.Html;
using MangaCrawler.Crawler.Data;
using MangaCrawler.Crawler.Database;
using MangaCrawler.Util;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Provider.MangaIndo
{
    class MangaIndoManga : Manga
    {
        public async override Task<IEnumerable<IChapter>> GetChapters(bool update = false)
        {
            if (update)
                await GetFromServer();
            
            return await GetFromDatabase();
        }
        public async override Task<IChapter> GetChapter(int id)
        {
            using (var conn = Connector.GetConnection())
            {
                try
                {
                    var sql = "SELECT * FROM chapter WHERE Id = @chapter";
                    var param = new { chapter = id };
                    var query = await conn.QuerySingleAsync<MangaIndoChapter>(sql, param);

                    return query;
                }
                catch
                {
                    throw;
                }
            }
        }
        private async Task<IEnumerable<IChapter>> GetFromDatabase()
        {
            using (var conn = Connector.GetConnection())
            {
                try
                {
                    var sql = "SELECT * FROM chapter WHERE IdManga = @manga ORDER BY Num";
                    var param = new { manga = this.Id };
                    var query = await conn.QueryAsync<MangaIndoChapter>(sql, param);

                    return query;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private async Task GetFromServer()
        {
            try
            {
                var existChapter = await GetFromDatabase();
                var crawler = new DomCrawler();
                var lstResult = new List<IChapter>();
                var stream = await HttpDownloader.GetAsync(HttpMethod.Get, Url);
                uint counter = 1;
                await crawler.LoadHtmlAsync(stream);

                try
                {
                    counter = existChapter.Max(x => x.Num) + 1;
                }
                catch { }

                var elements = crawler.Query("div.cl > ul > li");

                foreach (var elm in elements)
                {
                    crawler.LoadHtml(elm.InnerHtml);

                    var link = (IHtmlAnchorElement)crawler.QuerySingle("span.leftoff > a");

                    if (link != null)
                    {
                        var chapter = new MangaIndoChapter()
                        {
                            IdManga = this.Id,
                            Url = link.Href,
                            Title = link.InnerHtml,
                            Num = 0,
                        };
                        var ext = (from a in existChapter
                                   where a.Title == chapter.Title
                                   select a).FirstOrDefault();

                        if (ext == null)
                            lstResult.Add(chapter);
                    }
                }

                lstResult = lstResult.OrderBy(x => x.Title, new NaturalComparer()).ToList();

                foreach (var elm in lstResult)
                {
                    try
                    {
                        elm.Num = counter;
                        elm.Save();
                        counter++;
                    }
                    catch (Exception ex)
                    {
                        //
                    }
                }
            } catch { }
        }


        public async override Task<IDictionary<string, object>> GetMetas()
        {
            var crawler = new DomCrawler();
            var lstResult = new Dictionary<string, object>();
            var stream = await HttpDownloader.GetAsync(HttpMethod.Get, Url);
            await crawler.LoadHtmlAsync(stream);

            return new Dictionary<string, object>();
        }
    }
}
