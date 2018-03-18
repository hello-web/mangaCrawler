using LiteDB;
using MangaCrawler.Crawler.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MangaCrawler
{
    static class Program
    {
        public static string CachePath { get => Path.Combine(Application.StartupPath, "Cache"); }
        public static string LitePath { get => Path.Combine(CachePath, "cache.db"); }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PrepareApp();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void PrepareApp()
        {
            if (!Directory.Exists(CachePath))
                Directory.CreateDirectory(CachePath);
            
            if (!File.Exists(LitePath))
            {
                var connStr = new ConnectionString()
                {
                    Filename = LitePath,
                    Flush = false,
                    Upgrade = true,
                    Mode = LiteDB.FileMode.Shared,
                };

                var liteDb = new LiteDatabase(connStr);
                var collCache = liteDb.GetCollection<CacheImg>();
                
                try
                {
                    collCache.Insert(new CacheImg());
                } catch { }
            }
        }
    }
}
