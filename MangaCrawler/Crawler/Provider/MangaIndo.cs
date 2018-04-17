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
using AProvider = MangaCrawler.Crawler.Database.Provider;

namespace MangaCrawler.Crawler.Provider
{
    class MangaIndoProvider : AProvider
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

        public override async Task<IManga> GetManga(int Id, bool update = false)
        {
            using (var conn = Connector.GetConnection())
            {
                try
                {
                    var sql = "SELECT * FROM manga WHERE Id = @manga";
                    var param = new { manga = Id };
                    var query = await conn.QuerySingleAsync<MangaIndoManga>(sql, param);

                    return query;
                } catch
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
                } catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private async Task GetFromWebsite()
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
    }

    class MangaIndoManga : Manga
    {
        public async override Task<IEnumerable<IChapter>> GetChapters(bool update = false)
        {
            var existChapter = await GetFromDatabase();

            if (!update)
                return existChapter;

            var crawler = new DomCrawler();
            var lstResult = new List<IChapter>();
            var stream = await HttpDownloader.GetAsync(HttpMethod.Get, Url);
            uint counter = 1;
            await crawler.LoadHtmlAsync(stream);

            try
            {
                counter = existChapter.Max(x => x.Num) + 1;
            } catch { }

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
                } catch (Exception ex)
                {
                    //
                }
            }
            
            return await GetFromDatabase();
        }
        private async Task<IEnumerable<IChapter>> GetFromDatabase()
        {
            using (var conn = Connector.GetConnection())
            {
                try
                {
                    var sql = "SELECT * FROM chapter WHERE IdManga = @manga";
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

        public async override Task<IDictionary<string, object>> GetMetas()
        {
            var crawler = new DomCrawler();
            var lstResult = new Dictionary<string, object>();
            var stream = await HttpDownloader.GetAsync(HttpMethod.Get, Url);
            await crawler.LoadHtmlAsync(stream);

            return new Dictionary<string, object>();
        }
    }

    class MangaIndoChapter : Chapter
    {
        public override async Task<IEnumerable<IPage>> GetPages()
        {
            var existsPages = await GetFromDatabase();

            if (existsPages.LongCount() > 0)
                return existsPages;

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
                } catch (Exception ex)
                {
                    //
                }
            }

            return await GetFromDatabase();
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
                } catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }

    class MangaIndoPage : Page
    {
        public override Task<bool> DownloadPage(string filename)
        {
            return Task.Run(() => true);
        }
    }
}
