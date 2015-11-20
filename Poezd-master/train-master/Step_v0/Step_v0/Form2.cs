using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Step_v0
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string[] info;
        private void button1_Click(object sender, EventArgs e)
        {
            Train train = new Train();
            List<string> collection = new List<string>();
            string nomer = textBox1.Text;
            string type = comboBox1.Text;
            string otpr = comboBox2.Text;string prib = comboBox3.Text;string marsh = otpr + ' ' + prib;
            string time_otpr = textBox2.Text;
            string time_prib = textBox3.Text;
            collection = Train.PoiskPoezda(marsh);
            if (collection[0].Contains("null"))
            {
                MessageBox.Show("Ехать с пересадкой");
            }
            else
            {
                info = collection[0].Split('{');
            }
            string str_ob = nomer + ' ' + type+' '+ marsh + ' ' + time_otpr + time_prib + ' '+'{'+ info[1];
            StreamWriter sw = new StreamWriter("new.txt",true);
            sw.WriteLine(str_ob);
            sw.Close();
        }
    }
}
