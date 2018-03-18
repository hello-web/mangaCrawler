using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MangaCrawler
{
    public partial class Form1 : Form
    {
        private delegate void Change();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                string a = await Download(textBox1.Text);
                Change b = delegate ()
                {
                    textBox2.Text = a;
                };

                Invoke(b);
            });
        }

        private async Task<string> Download(string address)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri requestUri = new Uri(address);
                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUri);
                HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);
                
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent content = httpResponse.Content;
                    return await content.ReadAsStringAsync();
                }

                return null;
            }
        }
    }
}
