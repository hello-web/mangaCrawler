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
        private List<IManga> lstManga = new List<IManga>();
        private delegate void Change();

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                var provider = new MangaIndo();
                var tmpManga = await provider.GetList();
                lstManga = tmpManga.ToList();

                Invoke(new Change(ChangeList));
            });
        }

        private void ChangeList()
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
                item.SubItems.Add(manga.Url);

                listView1.Items.Add(item);
                counter++;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            JobScheduler.Start();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                //double click to see detail;
                var idx = listView1.SelectedItems[0].Index;
                var manga = lstManga[idx];
                var detailDialog = new DetailManga(manga);

                detailDialog.Show();
            }
        }
    }
}
