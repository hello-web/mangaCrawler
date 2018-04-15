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

namespace MangaCrawler.App
{
    public class AppBinding
    {
        private IDictionary<string, IProvider> Providers;

        public AppBinding()
        {
            Providers = new Dictionary<string, IProvider>();
            Providers.Add("BacaManga", new MangaIndo());
        }

        private string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        //We expect an exception here, so tell VS to ignore
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

        public void GetMangaList(int id, IJavascriptCallback javascriptCallback)
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
                        var list = await provider.GetList();

                        var response = ToJson(list);
                        await javascriptCallback.ExecuteAsync(response);
                    }
                }
            });
        }
    }
}
