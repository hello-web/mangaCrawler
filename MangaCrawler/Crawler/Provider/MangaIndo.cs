using AngleSharp.Dom.Html;
using MangaCrawler.Crawler.Data;
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

                IHtmlImageElement thumb = (IHtmlImageElement)crawler.QuerySingle("div.thumb img");
                IHtmlAnchorElement link = (IHtmlAnchorElement)crawler.QuerySingle("div.title > a");

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
        public override ICollection<IChapter> GetChapters()
        {
            return new List<IChapter>();
        }

        public override IDictionary<string, object> GetMetas()
        {
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
