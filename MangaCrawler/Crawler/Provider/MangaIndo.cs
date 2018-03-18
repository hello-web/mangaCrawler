using AngleSharp.Dom.Html;
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

        public async Task GetList()
        {
            var crawler = new DomCrawler();
            var lstResult = new List<MangaIndoManga>();
            var stream = await HttpDownloader.GetAsync(HttpMethod.Get, homeurl);
            await crawler.LoadHtmlAsync(stream);

            var elements = crawler.Query("div.ltsc > div.mng");

            foreach (var elm in elements)
            {
                crawler.LoadHtml(elm.InnerHtml);

                IHtmlImageElement thumb = (IHtmlImageElement)crawler.QuerySingle("div.thumb img");
                IHtmlAnchorElement link = (IHtmlAnchorElement)crawler.QuerySingle("div.title > a");

                if (thumb != null && link != null)
                {
                    var manga = new MangaIndoManga()
                    {
                        Link = link.Href,
                        ThumbLink = thumb.Source,
                        Title = link.InnerHtml
                    };

                    lstResult.Add(manga);
                }
            }

            return;
        }
    }

    class MangaIndoManga
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string ThumbLink { get; set; }
    }
}
