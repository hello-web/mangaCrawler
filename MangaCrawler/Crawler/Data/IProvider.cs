using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Data
{
    interface IProvider
    {
        uint Id { get; set; }
        string Name { get; set; }
        string Url { get; set; }
        bool IsEnabled { get; set; }

        Task<IEnumerable<IManga>> GetList();
        void Save();
    }
}
