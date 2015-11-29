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
          /* // Train train = new Train();string str_ob="";
            List<string> collection = new List<string>();
            Random realRnd = new Random();int k = 0;
            string[] new_poezd = new string[6];
            string new_ostanovki = "",time_ost=textBox2.Text; 
            new_poezd[0] = textBox1.Text;
            new_poezd[1] = comboBox1.Text;
            new_poezd[2] = comboBox2.Text; new_poezd[3] = comboBox3.Text;string marsh = new_poezd[2] + ' ' + new_poezd[3];
            new_poezd[4] = textBox2.Text;
            new_poezd[5] = textBox3.Text;
            string[] time1 = new_poezd[4].Split(':');
            string[] time2 = new_poezd[5].Split(':');
            int avarage = (int.Parse(time2[0])*60+int.Parse(time2[1]))-(int.Parse(time1[0]) * 60 + int.Parse(time1[1]));
            int current_time = (int.Parse(time1[0]) * 60 + int.Parse(time1[1]));
        //    collection = Train.PoiskPoezda(marsh);

            if (collection[0].Contains("null"))
            {
                MessageBox.Show("Ехать с пересадкой");
            }
            else
            {
                info = collection[0].Split(' ');
                for (int i = 7; i < info.Count()-3; i=i+3) {
                     current_time += (realRnd.Next(40, avarage/8));
                if (i == 7){
                        time_ost = new_poezd[4]; k++;
                        new_ostanovki += info[i] + ' ' + '(' + time_ost + ')' + ' ' + '-' + ' ';
                    }
                if (i == info.Count() - 4) {
                        time_ost = new_poezd[5]; k++;
                        new_ostanovki += info[i] + ' ' + '(' + time_ost + ')' + ' ';

                    }
                    if (k == 0) {
                        time_ost += ((current_time) / 60).ToString() + ':' + ((current_time) % 60).ToString();
                        new_ostanovki += info[i] + ' ' + '(' + time_ost + ')' + ' '+'-'+' ';
                    }
                    k = 0;
                    
                    time_ost = "";
                }
            }
            for (int i = 0; i < new_poezd.Length; i++) {
                str_ob += new_poezd[i] + ' ';         
            }
            str_ob += '{' + ' ' + new_ostanovki + '}';
            StreamWriter sw = new StreamWriter("new.txt",true);
            sw.WriteLine(str_ob);
            sw.Close();*/
        }
    }
}
