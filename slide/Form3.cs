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

namespace slide
{
    public partial class Form3 : Form
    {
        bool language = true;
        bool Change = false;
        string path=null;
        private System.Drawing.Printing.PrintDocument docToPrint =
        new System.Drawing.Printing.PrintDocument();

        public Form3()
        {
            InitializeComponent();

            saveFileDialog1.Filter = "txt files(*.txt)|*.txt";
            openFileDialog1.Filter = "txt files(*.txt)|*.txt";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (language)
            {
                menuStrip1.Visible = false;
                menuStrip2.Visible = true;
                language = false;

                textBox1.ContextMenuStrip = contextMenuStrip2;
                toolStripButton1.Text = "Russian";
            }
            else
            {
                menuStrip1.Visible = true;
                menuStrip2.Visible = false;
                language = true;

                textBox1.ContextMenuStrip = contextMenuStrip1;
                toolStripButton1.Text = "Английский";
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="")
            {
                if (MessageBox.Show("Хотите сохранить?", "Предупреждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning)==DialogResult.Yes&&Change)
                {
                    Save();
                    Change = false;
                }

                textBox1.Text = "";
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader re = new StreamReader(openFileDialog1.FileName);
                    path = openFileDialog1.FileName;

                    this.Text = openFileDialog1.FileName;

                    textBox1.Text = re.ReadToEnd();
                    re.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (path != null)
            {
                Save();
                Change = false;
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                path = saveFileDialog1.FileName;
                Save();
                Change = false;
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Change)
            {
                if (MessageBox.Show("Текст был изменен!!!\nХотите сохранить?", "Предупреждение!!!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning)==DialogResult.Yes)
                {
                    Save();
                }
            }
            this.Close();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!=""&&Change)
            {
                if (MessageBox.Show("Сохранить файл?", "Предупреждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    try
                    {
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            path = saveFileDialog1.FileName;
                            Save();
                            Change = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            if (saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                this.Text = saveFileDialog1.FileName;
                path = saveFileDialog1.FileName;

            }
        }

        private void Save()
        {
            try
            {
                StreamWriter stream = new StreamWriter(path);

                stream.WriteLine(textBox1.Text);

                stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.Document = docToPrint;

            if (printDialog1.ShowDialog()==DialogResult.OK)
            {
                docToPrint.Print();
            }
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength>0)
            {
                textBox1.Cut();
            }
        }

        private void копироватьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
            {
                textBox1.Copy();
            }
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = textBox1.Font;

            if (fontDialog1.ShowDialog()==DialogResult.OK)
            {
                textBox1.Font = fontDialog1.Font;
            }
        }

        private void цветШрифтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog()==DialogResult.OK)
            {
                textBox1.ForeColor = colorDialog1.Color;
            }
        }

        private void цветФонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog()==DialogResult.OK)
            {
                textBox1.BackColor = colorDialog1.Color;
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
            {
                textBox1.SelectedText="";
            }
        }

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Change = true;
        }
    }
}
