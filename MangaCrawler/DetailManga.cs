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
        ICollection<IChapter> lstChapter = new List<IChapter>();
        IDictionary<string, object> lstMeta = new Dictionary<string, object>();

        private delegate void Change();

        public DetailManga(IManga manga)
        {
            InitializeComponent();

            pictureBox1.Image = manga.GetThumbnail();

            Task.Run(async () =>
            {
                lstChapter = await manga.GetChapters();
                lstMeta = await manga.GetMetas();

                Invoke(new Change(ChangeList));
            });
        }

        private void ChangeList()
        {
            //
        }

        private void DetailManga_Load(object sender, EventArgs e)
        {
            //
        }
    }
}
