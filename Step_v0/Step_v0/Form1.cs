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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] info;
        string[] route;
        Image[] images = null;
        string mesto_b;
        int mesto_n,i;
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                textBox1.Enabled = true;
                textBox6.Enabled = true;
                textBox5.Enabled = true;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textNomer.Enabled = false;
                Receive_data2.Visible = true;
                Receive_data1.Visible = false;
                Receive_data3.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox1.Enabled = false;
                textBox6.Enabled = false;
                textBox5.Enabled = false;
                textNomer.Enabled = false;
                Receive_data3.Visible = true;
                Receive_data2.Visible = false;
                Receive_data1.Visible = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                textNomer.Enabled = true;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox1.Enabled = false;
                textBox6.Enabled = false;
                textBox5.Enabled = false;
                Receive_data1.Visible = true;
                Receive_data2.Visible = false;
                Receive_data3.Visible = false;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if ((Char.IsDigit(e.KeyChar)) || (Char.IsControl(e.KeyChar)))
                e.Handled = false;
        }

        private void Receive_data1_Click(object sender, EventArgs e)
        {
            Train train = new Train();
            List<string> collection = new List<string>();
            StreamReader fs1 = new StreamReader("baseV2.txt");
            while (true) 
            {
                string s = fs1.ReadLine();

                if (s != null)
                {
                    collection.Add(s);
                }
                else
                    break;
            }
            string str = textNomer.Text;
            int k = 0;
            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].Contains(str))
                {
                    info = collection[i].Split(' ');
                    route = info[4].Split('-');
                    textBox4.Text = route[0];
                    textBox3.Text = route[1];
                    k++;
                    Vivod_data.Items.Add(collection[i]);
                    Count.Visible = true;
                }
            }

            if (k == 0)
            {
                MessageBox.Show("Не найденно совпадений");
                Vivod_data.Items.Add("Совпадений не найдено");
            }
                // string s = Properties.Resources.Base;
                Vivod_data.Visible = true;
        }

        private void Receive_data2_Click(object sender, EventArgs e)
        {
            Shema.Visible = true;
            Passanger passanger = new Passanger();
            List<string> collection = new List<string>();
            StreamReader fs2 = new StreamReader("baseV3.txt");
            while (true)
            {
                string s = fs2.ReadLine();

                if (s != null)
                {
                    collection.Add(s);
                }
                else
                    break;
            }
            string str1 = textBox1.Text;
            string str2 = textBox6.Text;
            string str3 = textBox5.Text;
            int k = 0;
            for (int i = 0; i < collection.Count; i++)
            {
                if ((collection[i].Contains(str1)) && (collection[i].Contains(str2)) && (collection[i].Contains(str3)))
                {
                    string[] info = collection[i].Split(' ');
                    passanger.Surname = info[0];
                    passanger.Name = info[1];
                    passanger.Secondname = info[2];
                    mesto_n = int.Parse(info[3]);
                    mesto_b = info[4];
                    k++;
                    Vivod_data.Items.Add(collection[i]);
                }
            }

            if (k == 0)
            {
                MessageBox.Show("Таких людей нет");
                Vivod_data.Items.Add("Таких людей нет");
            }
            // string s = Properties.Resources.Base;
            Vivod_data.Visible = true;
        }

        private void Receive_data3_Click(object sender, EventArgs e)
        {
            
        }

        private void Clean_Click(object sender, EventArgs e)
        {
            Vivod_data.Items.Clear();
            pictureBox1.Refresh();
        }

        private void Count_Click(object sender, EventArgs e)
        {
            List<string> collection = new List<string>();
            StreamReader fs3 = new StreamReader("baseV4.txt");
            Train train = new Train();
            while (true)
            {
                string s = fs3.ReadLine();

                if (s != null)
                {
                    collection.Add(s);
                }
                else
                    break;
            }
            for (int i = 0; i < collection.Count; i++)
            {
                if ((collection[i].Contains(route[0])) && (collection[i].Contains(route[1])))
                {
                   string[] route1 = collection[i].Split(' ');
                     train.Distance = int.Parse(route1[2]);
                }
            }  
            train.Nomer = info[0];
            train.Type = info[1];
            train.Speed = int.Parse(info[2]);
            Vivod_data.Items.Add("Поезд будет в пути :");
            Vivod_data.Items.Add(Train.AnalizInfo(train.Distance, train.Speed) + " часов");
        }

        private void Shema_Click(object sender, EventArgs e)
        {
            
            images = new Image[7];
            for (int k = 0; k < images.Length; k++)
            {
                images[k] = Image.FromFile(string.Format(@"{0}.jpg", k));
            }
            
            if (mesto_b == "A")
            {
                 i = mesto_n - 1;
            }
            pictureBox1.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            label12.Visible = true;
            label13.Visible = true;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.Image = images[i];
        }
    }

    public class Train {
        public string Nomer, Type;
        public int Speed, Distance;

        public static int AnalizInfo(int distance, int speed)
        {
            int time = distance / speed;
            return time;
        }
    }

    public class Vagon {

    }

    public class Passanger {
        public string Surname, Name, Secondname;

        public void AnalizInfo(string surname, string name, string secondname)
        {
            
        }
    }

}
