using MangaCrawler.Crawler.Data;
using MangaCrawler.Crawler.Provider;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CefSharp;
using System.Reflection;

namespace MangaCrawler.App
{
    public class AppBinding
    {
        private IDictionary<string, IProvider> Providers;

        public AppBinding()
        {
            Providers = new Dictionary<string, IProvider>();
            Providers.Add("BacaManga", new MangaIndoProvider());
        }

        private string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Get manga providers
        /// </summary>
        /// <param name="javascriptCallback"></param>
        public void GetProviders(IJavascriptCallback javascriptCallback)
        {
            var providers = new List<IProvider>();

            foreach (var keyPair in Providers)
            {
                providers.Add(keyPair.Value);
            }

            Task.Run(async () =>
            {
                using (javascriptCallback)
                {
                    var response = ToJson(providers);
                    await javascriptCallback.ExecuteAsync(response);
                }
            });
        }

        /// <summary>
        /// Get manga list from provider
        /// </summary>
        /// <param name="id">ID Provider</param>
        /// <param name="page">Page Number</param>
        /// <param name="javascriptCallback"></param>
        public void GetMangaList(int id, int page, bool update, IJavascriptCallback javascriptCallback)
        {
            var provider = (from a in Providers
                            where a.Value.Id == id
                            select a.Value).SingleOrDefault();
            
            Task.Run(async () => {
                using (javascriptCallback)
                {
                    if (provider == null)
                        await javascriptCallback.ExecuteAsync("");
                    else
                    {
                        var skipCnt = (page - 1) * 20;
                        var list = await provider.GetMangas(update);
                        var sliceList = list.Skip(skipCnt).Take(20);
                        var response = ToJson(sliceList);

                        await javascriptCallback.ExecuteAsync(response);
                    }
                }
            });
        }

        /// <summary>
        /// Get chapter list from manga
        /// </summary>
        /// <param name="id">ID Provider</param>
        /// <param name="id_manga">ID Manga</param>
        /// <param name="page">Page Number</param>
        /// <param name="javascriptCallback"></param>
        public void GetChapterList(int id, int id_manga, int page, bool update, IJavascriptCallback javascriptCallback)
        {
            Task.Run(async () =>
            {
                using (javascriptCallback)
                {
                    var provider = (from a in Providers
                                    where a.Value.Id == id
                                    select a.Value).SingleOrDefault();

                    if (provider == null)
                        await javascriptCallback.ExecuteAsync("");
                    else
                    {
                        var skipCnt = (page - 1) * 20;
                        var manga = await provider.GetManga(id_manga);
                        var chapters = await manga.GetChapters(update);
                        var sliceList = chapters.Skip(skipCnt).Take(20);
                        var response = ToJson(sliceList);

                        await javascriptCallback.ExecuteAsync(response);
                    }
                }
            });
        }

        /// <summary>
        /// Get page list from chapter
        /// </summary>
        /// <param name="id">ID Provider</param>
        /// <param name="id_manga">ID Manga</param>
        /// <param name="id_chapter">ID Chapter</param>
        /// <param name="javascriptCallback"></param>
        public void GetPageList(int id, int id_manga, int id_chapter, bool update, IJavascriptCallback javascriptCallback)
        {
            Task.Run(async () =>
            {
                using (javascriptCallback)
                {
                    var provider = (from a in Providers
                                    where a.Value.Id == id
                                    select a.Value).SingleOrDefault();

                    var manga = await provider?.GetManga(id_manga);
                    var chapter = await manga?.GetChapter(id_chapter);
                    var pages = await chapter?.GetPages(update);

                    if (pages != null)
                    {
                        var response = ToJson(pages);

                        await javascriptCallback.ExecuteAsync(response);
                    } else
                    {
                        await javascriptCallback.ExecuteAsync("");
                    }
                }
            });
        }
    }
}
