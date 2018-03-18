using MangaCrawler.Crawler.Data;
using MangaCrawler.Crawler.Job;
using MangaCrawler.Crawler.Provider;
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
    public partial class MainForm : Form
    {
        private delegate void Change(List<IManga> lst);

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                var c = new MangaIndo();
                var lst = await c.GetList();

                Invoke(new Change(ChangeList), lst);
            });
        }

        private void ChangeList(List<IManga> lstManga)
        {
            listView1.Items.Clear();
            imageList1.Images.Clear();
            int counter = 0;

            foreach (var manga in lstManga)
            {
                var img = manga.GetThumbnail();
                var item = new ListViewItem();

                imageList1.Images.Add(img);

                item.ImageIndex = counter;
                item.Text = manga.Title;
                item.SubItems.Add(manga.MangaLink);

                listView1.Items.Add(item);
                counter++;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            JobScheduler.Start();
        }
    }
}
