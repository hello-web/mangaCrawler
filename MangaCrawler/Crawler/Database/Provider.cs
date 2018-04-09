using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Database
{
    class Provider
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
    }
}
