using MangaCrawler.Crawler.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MangaCrawler
{
    public partial class DetailManga : Form
    {
        IEnumerable<IChapter> lstChapter = new List<IChapter>();
        IDictionary<string, object> lstMeta = new Dictionary<string, object>();

        private delegate void Change(List<ListViewItem> list);

        public DetailManga(IManga manga)
        {
            InitializeComponent();

            pictureBox1.Image = manga.GetThumbnail();

            Task.Run(async () =>
            {
                lstChapter = await manga.GetChapters();
                lstMeta = await manga.GetMetas();
                var lstViewItem = new List<ListViewItem>();

                foreach (var chapter in lstChapter)
                {
                    var item = new ListViewItem(chapter.Num.ToString());
                    var pages = await chapter.GetPages();
                    item.SubItems.Add(pages.LongCount().ToString());
                    item.SubItems.Add(chapter.Title);

                    lstViewItem.Add(item);
                }

                Invoke(new Change(ChangeList), lstViewItem);
            });
        }

        private void ChangeList(List<ListViewItem> items)
        {
            listView2.Items.Clear();

            foreach (var item in items)
            {
                listView2.Items.Add(item);
            }
        }

        private void DetailManga_Load(object sender, EventArgs e)
        {
            //
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                //Download and read it.
            }
        }
    }
}
