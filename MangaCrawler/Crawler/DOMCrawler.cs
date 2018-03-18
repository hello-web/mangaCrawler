using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler
{
    class DomCrawler
    {
        HtmlParser parser;
        IHtmlDocument document;

        public DomCrawler()
        {
            parser = new HtmlParser(new HtmlParserOptions()
            {
                IsScripting = true,
                IsStrictMode = false,
                IsEmbedded = true,
            });
        }

        public void LoadHtml(string html)
        {
            document = parser.Parse(html);
        }

        public void LoadHtml(Stream stream)
        {
            document = parser.Parse(stream);
        }

        public async Task LoadHtmlAsync(string html)
        {
            document = await parser.ParseAsync(html);
        }

        public async Task LoadHtmlAsync(Stream stream)
        {
            document = await parser.ParseAsync(stream);
        }

        public IHtmlCollection<IElement> Query(string selector)
        {
            return document.QuerySelectorAll(selector);
        }

        public IElement QuerySingle(string selector)
        {
            return document.QuerySelector(selector);
        }
    }
}
