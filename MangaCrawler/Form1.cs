﻿using MangaCrawler.Crawler.Provider;
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
                var c = new MangaIndo();
                var lst = await c.GetList();
            });
        }

        
    }
}
