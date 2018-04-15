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
                SchemeHandlerFactory = new AppScheme(Application.StartupPath, "http://local.com")
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

            browser.IsBrowserInitializedChanged += Browser_IsBrowserInitializedChanged;
            browser.TitleChanged += Browser_TitleChanged;
            browser.RegisterAsyncJsObject("CS", new AppBinding());

            form.Controls.Add(browser);
        }

        private void Browser_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            Action act = () => form.Text = e.Title;
            
            form.Invoke(act);
        }

        private void Browser_IsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs e)
        {
            if (e.IsBrowserInitialized)
                browser.ShowDevTools();
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
