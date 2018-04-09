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
        string ProviderName { get; set; }
        string ProviderHome { get; set; }
        bool IsEnabled { get; set; }

        Task<ICollection<IManga>> GetList();
    }

    abstract class Provider : IProvider
    {
        public uint Id { get; set; }
        public string ProviderName { get; set; }
        public string ProviderHome { get; set; }
        public bool IsEnabled { get; set; }

        public abstract Task<ICollection<IManga>> GetList();
    }
}
