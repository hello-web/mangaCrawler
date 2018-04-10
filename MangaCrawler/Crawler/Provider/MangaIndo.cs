using AngleSharp.Dom.Html;
using MangaCrawler.Crawler.Data;
using MangaCrawler.Crawler.Database;
using MangaCrawler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AProvider = MangaCrawler.Crawler.Database.Provider;

namespace MangaCrawler.Crawler.Provider
{
    class MangaIndo : AProvider
    {
        private new string Name { get; set; } = "Manga Indo";
        private new string Url { get; set; } = "https://mangaindo.net/";

        public override async Task<ICollection<IManga>> GetList()
        {
            var crawler = new DomCrawler();
            var lstResult = new List<IManga>();
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
                    var manga = new MangaIndoManga()
                    {
                        Url = link.Href,
                        ThumbUrl = thumb.Source,
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
            var stream = await HttpDownloader.GetAsync(HttpMethod.Get, Url);
            uint counter = 1;
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
                        Url = link.Href,
                        Title = link.InnerHtml,
                        Num = 0,
                    };

                    lstResult.Add(chapter);
                }
            }
            
            lstResult = lstResult.OrderBy(x => x.Title, new NaturalComparer()).ToList();

            foreach (var elm in lstResult)
            {
                elm.Num = counter;
                counter++;
            }
            
            return lstResult;
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
        public override async Task<ICollection<IPage>> GetPages()
        {
            if (Pages == null)
            {
                //start crawling web
                uint counter = 1;
                var crawler = new DomCrawler();
                var stream = await HttpDownloader.GetAsync(HttpMethod.Get, Url);

                await crawler.LoadHtmlAsync(stream);

                var elements = crawler.Query("#readerarea img");

                Pages = new List<IPage>();

                foreach (IHtmlImageElement element in elements)
                {
                    Pages.Add(new MangaIndoPage()
                    {
                        Num = ++counter,
                        Url = element.Source,
                    });
                }
            }

            return Pages;
        }
    }

    class MangaIndoPage : Page
    {
        public async override Task<bool> DownloadPage(string filename)
        {
            return true;
        }
    }
}
