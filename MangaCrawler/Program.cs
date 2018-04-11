﻿using CefSharp;
using CefSharp.WinForms;
using LiteDB;
using MangaCrawler.Crawler.Data;
using MangaCrawler.Crawler.Database;
using MangaCrawler.Util;
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
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                // There arguments here
                if (args[0] == "--register")
                    ProviderRegister.Register();

                Application.Exit();
                return;
            }

            Cef.EnableHighDPISupport();
            var exitCode = Cef.ExecuteProcess();

            if (exitCode >= 0)
                return;

            var settings = new CefSettings();
            
            Cef.Initialize(settings);
            var form = new Form();
            var browser = new ChromiumWebBrowser("www.google.com")
            {
                Dock = DockStyle.Fill,
            };
            form.Controls.Add(browser);
            Application.Run(form);
            browser.Load("http://www.google.com");
            Cef.Shutdown();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());
        }
    }
}
