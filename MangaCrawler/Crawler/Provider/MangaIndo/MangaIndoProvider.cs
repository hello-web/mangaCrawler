using MangaCrawler.Crawler.Data;
using MangaCrawler.Crawler.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using TProvider = MangaCrawler.Crawler.Database.Provider;
using AngleSharp.Dom.Html;
using System.Net.Http;

namespace MangaCrawler.Crawler.Provider.MangaIndo
{
    class MangaIndoProvider : TProvider
    {
        public MangaIndoProvider()
        {
            Name = "Manga Indo";
            Url = "https://mangaindo.net/";
            IsEnabled = true;

            GetId();
        }

        public override async Task<IEnumerable<IManga>> GetMangas(bool update = false)
        {
            if (update)
                await GetFromWebsite();

            return await GetFromDatabase();
        }

        public override async Task<IManga> GetManga(int Id)
        {
            using (var conn = Connector.GetConnection())
            {
                try
                {
                    var sql = "SELECT * FROM manga WHERE Id = @manga";
                    var param = new { manga = Id };
                    var query = await conn.QuerySingleAsync<MangaIndoManga>(sql, param);

                    return query;
                }
                catch
                {
                    throw;
                }
            }
        }

        private async Task<IEnumerable<IManga>> GetFromDatabase()
        {
            using (var conn = Connector.GetConnection())
            {
                try
                {
                    var sql = "SELECT * FROM manga WHERE IdProvider = @provider";
                    var param = new { provider = this.Id };
                    var query = await conn.QueryAsync<MangaIndoManga>(sql, param);

                    return query;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private async Task GetFromWebsite()
        {
            try
            {
                var crawler = new DomCrawler();
                var stream = await HttpDownloader.GetAsync(HttpMethod.Get, Url);
                await crawler.LoadHtmlAsync(stream);

                var elements = crawler.Query("div.ltsc > div.mng");

                foreach (var elm in elements)
                {
                    crawler.LoadHtml(elm.InnerHtml);

                    var thumb = (IHtmlImageElement)crawler.QuerySingle("div.thumb img");
                    var link = (IHtmlAnchorElement)crawler.QuerySingle("div.title > a");

                    if (thumb != null && link != null)
                    {
                        try
                        {
                            var manga = new MangaIndoManga()
                            {
                                IdProvider = this.Id,
                                Url = link.Href,
                                ThumbUrl = thumb.Source,
                                Title = link.InnerHtml
                            };

                            manga.Save();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch { }
        }
    }
}
