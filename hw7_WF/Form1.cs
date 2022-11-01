using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace hw7_WF
{
    public partial class Form1 : Form
    {

        List<System.Drawing.Image> images = new List<System.Drawing.Image>();
        int iImage;

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void bt_open_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if(folder.ShowDialog() == DialogResult.OK)
            {
                timer1.Stop();
                if (images.Count != 0)
                {
                    foreach (var item in images)
                    {
                        item.Dispose();
                    }
                    images.Clear();
                   pictureBox1.Image = images[iImage];
                }
            }
            DirectoryInfo directory = new DirectoryInfo(folder.SelectedPath);
            IEnumerable<FileInfo> files = directory.EnumerateFiles();
            foreach (var item in files)
            {
                string fileName = Path.GetExtension(item.FullName);
                if (fileName == ".jpg" || fileName == ".jpeg" || fileName == ".bmp" || fileName == ".tif" || fileName == ".png" || fileName == ".gif")
                {

                    Bitmap bitmap = new Bitmap(item.FullName);
                    images.Add(new Bitmap(bitmap, pictureBox1.Size));
                }
                pictureBox1.Image = images[iImage];
                label1.Text = Convert.ToString((iImage + 1) + "/" + images.Count);
            }
            if(images.Count != 0)
            {
                bt_start.Enabled = true;
                bt_stop.Enabled = true;
                bt_prev.Enabled = true;
                bt_next.Enabled = true;
            }

        }
        private void bt_start_Click(object sender, EventArgs e)
        {
            if (images.Count != 0)
            {
                timer1.Start();
            }
            else
                MessageBox.Show("Вы не выбрали папку для слайд-шоу");
        }
        private void bt_stop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
        private void bt_exit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void bt_prev_Click(object sender, EventArgs e)
        {
            if(images.Count == 0)
            {
                return;
            }
            iImage--;
            if (iImage < 0)
            {
                iImage = images.Count-1;
            }
            pictureBox1.Image = images[iImage];
            label1.Text = Convert.ToString((iImage + 1) + "/" + images.Count);
        }
        private void bt_next_Click(object sender, EventArgs e)
        {
            if(images.Count == 0)
            {
                return;
            }
            iImage++;
            if (iImage >= images.Count)
            {
                iImage = 0;
            }
            pictureBox1.Image = images[iImage];
            label1.Text = Convert.ToString((iImage + 1) + "/" + images.Count);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (images.Count == 0)
            {
                return;
            }
            iImage++;
            if (iImage >= images.Count)
            {
                iImage = 0;
            }
            pictureBox1.Image = images[iImage];
            label1.Text = Convert.ToString((iImage + 1) + "/" + images.Count);
        }

    }
}
