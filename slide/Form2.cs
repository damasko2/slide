using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace slide
{
    public partial class Form2 : Form
    {
        Image[] LoadAllImages;
        string folderPath;
        int count=0;
        bool flag=false;
        Thread th;

        public Form2()
        {
            InitializeComponent();
        }

        private void Run()
        {
            while (true)
            {
                if (flag)
                {
                    break;
                }

                count++;

                if (count >= LoadAllImages.Length)
                {
                    count = 0;
                }

                label1.Text = $"{count + 1}/" + Convert.ToString(LoadAllImages.Length);
                pictureBox1.Image = LoadAllImages[count];

                Thread.Sleep((int)numericUpDown1.Value * 1000);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            fbd.Description = "выберите папку";
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                folderPath = fbd.SelectedPath;
                string searchPattern = "*.*";
                LoadAllImages = Directory.GetFiles(folderPath, searchPattern).Where(f => f.EndsWith(".jpg") ||
                f.EndsWith(".jpeg") || f.EndsWith(".bmp") || f.EndsWith(".PNG")||f.EndsWith(".png"))
                    .Select(Image.FromFile).ToArray();

                if (LoadAllImages.Length==0)
                {
                    label2.Text = "Изображения не обнаружены";
                    label1.Text = " / ";
                    pictureBox1.Image = null;
                    count = 0;
                }
                else
                {
                    count = 0;
                    label1.Text = $"{count+1}/" + Convert.ToString(LoadAllImages.Length);
                    pictureBox1.Image = LoadAllImages[count];
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                    label2.Text = "";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (LoadAllImages!=null&&LoadAllImages.Length!=0)
            {
                count++;
                if (count>=LoadAllImages.Length)
                {
                    count = 0;
                }

                pictureBox1.Image = LoadAllImages[count];
                label1.Text = $"{count+1}/" + Convert.ToString(LoadAllImages.Length);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (LoadAllImages != null && LoadAllImages.Length != 0)
            {
                count--;
                if (count < 0)
                {
                    count = LoadAllImages.Length - 1;
                }
                pictureBox1.Image = LoadAllImages[count];
                label1.Text = $"{count + 1}/" + Convert.ToString(LoadAllImages.Length);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button5.Visible = false;
            button1.Visible = false;
            numericUpDown1.Visible = false;

            if (LoadAllImages!=null)
            {
                th = new Thread(() => Run());
                th.Start();

                flag = false;
            }

            button4.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flag = true;
            th.Abort();

            button1.Visible = true;
            button3.Visible = true;
            button5.Visible = true;
            numericUpDown1.Visible = true;
            button4.Enabled = true;
        }
    }
}
