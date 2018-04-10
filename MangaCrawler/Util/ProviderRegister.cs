using MangaCrawler.Crawler.Data;
using MangaCrawler.Crawler.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Util
{
    class ProviderRegister
    {
        public static void Register()
        {
            List<IProvider> listProvider = new List<IProvider>();

            listProvider.Add(new MangaIndo());

            foreach (var provider in listProvider)
                provider.Save();
        }
    }
}
