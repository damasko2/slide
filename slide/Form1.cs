using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace slide
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();

            if (f2.ShowDialog()==DialogResult.OK)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();

            if (f3.ShowDialog()==DialogResult.OK)
            {

            }
        }
    }
}
