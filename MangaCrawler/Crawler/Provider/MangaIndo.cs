using AngleSharp.Dom.Html;
using MangaCrawler.Crawler.Data;
using MangaCrawler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Provider
{
    class MangaIndo
    {
        private string homeurl = "https://mangaindo.net/";

        public async Task<List<IManga>> GetList()
        {
            var crawler = new DomCrawler();
            var lstResult = new List<IManga>();
            var stream = await HttpDownloader.GetAsync(HttpMethod.Get, homeurl);
            await crawler.LoadHtmlAsync(stream);

            var elements = crawler.Query("div.ltsc > div.mng");

            foreach (var elm in elements)
            {
                crawler.LoadHtml(elm.InnerHtml);

                var thumb = (IHtmlImageElement)crawler.QuerySingle("div.thumb img");
                var link = (IHtmlAnchorElement)crawler.QuerySingle("div.title > a");

                if (thumb != null && link != null)
                {
                    var manga = new MangaIndoManga()
                    {
                        MangaLink = link.Href,
                        ThumbLink = thumb.Source,
                        Title = link.InnerHtml
                    };

                    lstResult.Add(manga);
                }
            }

            return lstResult;
        }
    }

    class MangaIndoManga : Manga
    {
        public async override Task<ICollection<IChapter>> GetChapters()
        {
            var crawler = new DomCrawler();
            var lstResult = new List<IChapter>();
            var stream = await HttpDownloader.GetAsync(HttpMethod.Get, MangaLink);
            var counter = 1;
            await crawler.LoadHtmlAsync(stream);

            var elements = crawler.Query("div.cl > ul > li");

            foreach (var elm in elements)
            {
                crawler.LoadHtml(elm.InnerHtml);

                var link = (IHtmlAnchorElement)crawler.QuerySingle("span.leftoff > a");

                if (link != null)
                {
                    var chapter = new MangaIndoChapter()
                    {
                        ChapterLink = link.Href,
                        Title = link.InnerHtml,
                        ChapterNum = 0,
                    };

                    lstResult.Add(chapter);
                }
            }

            //TODO:Need rearrange chapter here
            lstResult = lstResult.OrderBy(x => x.Title, new NaturalComparer()).ToList();

            foreach (var elm in lstResult)
            {
                elm.ChapterNum = counter;
                counter++;
            }
            
            return lstResult;
        }

        public async override Task<IDictionary<string, object>> GetMetas()
        {
            var crawler = new DomCrawler();
            var lstResult = new Dictionary<string, object>();
            var stream = await HttpDownloader.GetAsync(HttpMethod.Get, MangaLink);
            await crawler.LoadHtmlAsync(stream);

            return new Dictionary<string, object>();
        }
    }

    class MangaIndoChapter : Chapter
    {
        public override ICollection<IPage> GetPages()
        {
            return new List<IPage>();
        }
    }
}
