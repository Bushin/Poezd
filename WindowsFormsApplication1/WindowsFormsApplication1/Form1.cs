using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string s1, s2, s3;

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (Char.IsDigit(e.KeyChar) ) e.Handled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            s1 = textBox1.Text;
            s2 = textBox2.Text;
            textBox3.Text = s1;
        }
    }
}
