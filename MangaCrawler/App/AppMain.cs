using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MangaCrawler.App
{
    class AppMain
    {
        private ChromiumWebBrowser browser;
        private Form form;
        private CefCustomScheme scheme;
        private CefSettings settings;

        private bool InitCEF()
        {
            Cef.EnableHighDPISupport();
            var exitCode = Cef.ExecuteProcess();

            if (exitCode >= 0)
                return false;

            scheme = new CefCustomScheme()
            {
                DomainName = "local.com",
                IsCorsEnabled = false,
                IsCSPBypassing = true,
                IsSecure = false,
                SchemeName = "http",
                SchemeHandlerFactory = new AppScheme()
            };

            settings = new CefSettings()
            {
                RemoteDebuggingPort = 54477,
                IgnoreCertificateErrors = true,
            };
            
            settings.RegisterScheme(scheme);

            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            Cef.Initialize(settings);
            return true;
        }

        public void Initialize()
        {
            if (!InitCEF())
                return;

            form = new Form();
            browser = new ChromiumWebBrowser("about:blank")
            {
                Dock = DockStyle.Fill,
            };
            form.Controls.Add(browser);
        }

        public AppMain()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        public void RunApp()
        {
            browser.Load("http://local.com");

            Application.Run(form);
            Cef.Shutdown();
        }
    }
}
