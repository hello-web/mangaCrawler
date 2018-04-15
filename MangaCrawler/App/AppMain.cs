using CefSharp;
using CefSharp.WinForms;
using MangaCrawler.Crawler.Job;
using System;
using System.Collections.Generic;
using System.IO;
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

            CefSharpSettings.WcfEnabled = true;
            CefSharpSettings.WcfTimeout = TimeSpan.FromSeconds(10);
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
            browser.JavascriptObjectRepository.ResolveObject += JavascriptObjectRepository_ResolveObject;
            browser.JavascriptObjectRepository.ObjectBoundInJavascript += JavascriptObjectRepository_ObjectBoundInJavascript;
            browser.RegisterJsObject("CS", new AppBinding());
            form.Load += Form_Load;
            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;
            form.Controls.Add(browser);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            var cachePath = Path.Combine(Application.StartupPath, "cache\\");

            if (!Directory.Exists(cachePath))
                Directory.CreateDirectory(cachePath);

            JobScheduler.Start();
        }

        private void JavascriptObjectRepository_ObjectBoundInJavascript(object sender, CefSharp.Event.JavascriptBindingEventArgs e)
        {
            var x = e.ObjectName;
        }

        private void JavascriptObjectRepository_ResolveObject(object sender, CefSharp.Event.JavascriptBindingEventArgs e)
        {
            var repo = e.ObjectRepository;
            if (e.ObjectName == "CS")
            {
                BindingOptions bindingOptions = BindingOptions.DefaultBinder;
                bindingOptions.CamelCaseJavascriptNames = false;
                repo.Register("CS", new AppBinding(), isAsync: false, options: bindingOptions);
            }
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
