using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
namespace Step_v0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
 
        int f = 0;
        string mesto_b;
        int mesto_n, i;
        Train t;Stops ost;
        public List<Train> trains = new List<Train>();
        List<Stops> ostanovki = new List<Stops>();
        PictureBox pictureBoxs = new PictureBox();
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                f = 1;
            textBox1.Enabled = true; textBox6.Enabled = true; textBox5.Enabled = true; textBox3.Enabled = false; textBox4.Enabled = false; textNomer.Enabled = false;
            Vivod_data.Items.Clear();
            Vivod_ostanovok.Items.Clear();
            label1.ForeColor = Color.Gold;
            label5.ForeColor = Color.White;
            label6.ForeColor = Color.White;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                f = 2;
            textBox3.Enabled = true; textBox4.Enabled = true; textBox1.Enabled = false; textBox6.Enabled = false; textBox5.Enabled = false; textNomer.Enabled = false;
            Vivod_data.Items.Clear();
            Vivod_ostanovok.Items.Clear();
            label5.ForeColor = Color.Gold;
            label1.ForeColor = Color.White;
            label6.ForeColor = Color.White;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                f = 3;
            textNomer.Enabled = true; textBox3.Enabled = false; textBox4.Enabled = false; textBox1.Enabled = false; textBox6.Enabled = false; textBox5.Enabled = false;
            Vivod_data.Items.Clear();
            Vivod_ostanovok.Items.Clear();
            label6.ForeColor = Color.Gold;
            label1.ForeColor = Color.White;
            label5.ForeColor = Color.White;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if ((Char.IsDigit(e.KeyChar)) || (Char.IsControl(e.KeyChar)))
                e.Handled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length == 1)
                ((TextBox)sender).Text = ((TextBox)sender).Text.ToUpper();
            ((TextBox)sender).Select(((TextBox)sender).Text.Length, 0);
        }

        public void PoiskVsexPoezdov()
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load("C:\\Users\\Serg\\Desktop\\Проект ST&P\\test\\Poezd-master\\Poezd-master\\train-master\\Step_v0\\Step_v0\\Poezd.xml");
            XmlElement Root = Doc.DocumentElement;
            string nomer="",type="";
            int Hour, Min;
            foreach (XmlNode node in Root)
            {
                List<string> distance = new List<string>();
                List<DateTime> time = new List<DateTime>();
                if (node.Attributes.Count > 0)
                {                 
                    XmlNode attr = node.Attributes.GetNamedItem("name");
                    if (attr != null)
                       nomer = attr.Value;
                }
                foreach (XmlNode childnode in node.ChildNodes)
                {
                    if (childnode.Name == "type")
                    {
                        type = childnode.InnerText;
                    }
                    if (childnode.Name == "distanation")
                    {     
                        string[] info = childnode.InnerText.Split(' ');
                        for(int i=0;i<info.Length;i++)
                        distance.Add(info[i]);
                    }
                    if (childnode.Name == "time")
                    {
                        string[] info = childnode.InnerText.Split(' ');
                        for (int i = 0; i < info.Length; i++)
                        {
                            string[] info1 = (info[i].Split(':'));
                            Hour = int.Parse(info1[0]);
                            Min = int.Parse(info1[1]);
                            DateTime dt = new DateTime();

                            dt = dt.AddHours(Hour);
                            dt = dt.AddMinutes(Min);
                            time.Add(dt);
                        }
                    }
                }
                t= new Train(nomer,type,distance,time);           
                trains.Add(t);
                distance = null;
                time = null;
            }
        }

        public void PoiskVsexostanovok()
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load("C:\\Users\\Serg\\Desktop\\Проект ST&P\\test\\Poezd-master\\Poezd-master\\train-master\\Step_v0\\Step_v0\\Ostanovki.xml");
            XmlElement Root = Doc.DocumentElement;
            string name = "";int cor_x=0, cor_y=0;
            List<string> time = new List<string>();
            foreach (XmlNode node in Root)
            {                
                if (node.Attributes.Count > 0)
                {
                    XmlNode attr = node.Attributes.GetNamedItem("name");
                    if (attr != null)
                        name = attr.Value;
                }
                foreach (XmlNode childnode in node.ChildNodes)
                {
                    if (childnode.Name == "info")
                    {
                        string[] info = childnode.InnerText.Split(' ');
                        cor_x = int.Parse(info[0]);
                        cor_y = int.Parse(info[1]);
                    }
                }
                ost = new Stops(name, cor_x, cor_y);
                ostanovki.Add(ost);
            }
        }

        private void Receive_data1_Click(object sender, EventArgs e)
        {
            if (f == 3)
            {
                PoiskVsexPoezdov();
                PoiskVsexostanovok();
                for (int i = 0; i < trains.Count; i++)
                {
                    t = trains[i];
                    t.Vivod(ref Vivod_data, ref textBox4, ref textBox3, ref textNomer);
                }
                for (int i = 0; i < ostanovki.Count; i++)
                {
                    ost = ostanovki[i];
                    ost.Vivod(ref Vivod_ostanovok);
                }
            }
            if (f == 1)
            {
               
            }
            if (f == 2)
            {
               
            }
        }

        private void Clean_Click(object sender, EventArgs e)
        {
            Vivod_data.Items.Clear();
            Vivod_ostanovok.Items.Clear();
            Vivod_ostanovok.Visible = false;
            textBox1.Clear(); textNomer.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear(); textBox3.Clear();
            label10.Visible = false; label11.Visible = false; label12.Visible = false; label13.Visible = false;
            pictureBoxs.Image = null;
            pictureBox.Visible = false;
            pictureBox.Refresh();
        }

        private void MMT_Click(object sender, EventArgs e)
        {
            MMT MMT_Form = new MMT();
            MMT_Form.ShowDialog();
        }

        private void Editor_Click(object sender, EventArgs e)
        {
            Form2 secondForm = new Form2();
            secondForm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



    private void Vivod_data_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (f == 1)
            {
               
            }
            if ((f == 3) || (f == 2))
            {
 
            }
        }

  }
}
