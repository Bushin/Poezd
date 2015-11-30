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
        int mesto_n;
        Train t;Stops ost; Passanger pas;
        public List<Train> trains = new List<Train>();
        List<Stops> ostanovki = new List<Stops>();
        List<Passanger> passangers = new List<Passanger>();
        public List<Train> current_trains = new List<Train>();
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
            if ((Char.IsDigit(e.KeyChar)) || (Char.IsControl(e.KeyChar)) || (Char.IsSeparator(e.KeyChar)))
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
            Doc.Load("C:\\Users\\Жанна\\Documents\\Visual Studio 2015\\Projects\\Poezd-master\\28.11 ST&P\\Step_v0\\Step_v0\\Poezd.xml");
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
            Doc.Load("C:\\Users\\Жанна\\Documents\\Visual Studio 2015\\Projects\\Poezd-master\\28.11 ST&P\\Step_v0\\Step_v0\\Ostanovki.xml");
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

        public void PoiskVsexPasagirov()
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load("C:\\Users\\Жанна\\Documents\\Visual Studio 2015\\Projects\\Poezd-master\\28.11 ST&P\\Step_v0\\Step_v0\\Passanger.xml");
            XmlElement Root = Doc.DocumentElement;
            string n = "", s = "", nomer_poezda = "", nomer_vagona = "", mesto_nomer = "", mesto_bukva = "";
            List<string> time = new List<string>();
            foreach (XmlNode node in Root)
            {
                foreach (XmlNode childnode in node.ChildNodes)
                {
                    if (childnode.Name == "info")
                    {
                        string[] info = childnode.InnerText.Split(' ');
                        n = info[1]; s = info[0]; nomer_poezda = info[2]; nomer_vagona = info[3]; mesto_nomer = info[4]; mesto_bukva = info[5];
                    }
                }
                pas = new Passanger(n, s, nomer_poezda, nomer_vagona, mesto_nomer, mesto_bukva);
                passangers.Add(pas);
            }
        }

        private void Receive_data1_Click(object sender, EventArgs e)
        {
            if (f == 3)
            {
                string str = textNomer.Text; bool f = false;
                string[] p = str.Split(' ');
                if (str == "")
                {
                    f = true;
                    for (int i = 0; i < trains.Count; i++)
                    {
                        t = trains[i];
                        t.last_ostanovka();
                        current_trains.Add(trains[i]);
                        t.Vivod(ref Vivod_data, ref textBox4, ref textBox3, ref textNomer);
                    }
                }
                if (f == false)
                {     
                    for (int j = 0; j < p.Length; j++)
                    {
                        for (int i = 0; i < trains.Count; i++)
                        {
                            if (p[j] == trains[i].Nomer)
                            {
                                trains[i].last_ostanovka();
                                current_trains.Add(trains[i]);
                                trains[i].Vivod(ref Vivod_data, ref textBox4, ref textBox3, ref textNomer);
                            }
                        }
                    }
                }
                
            }
            if (f == 1)
            {
                string str1 = textBox1.Text;string str2 = textBox6.Text; bool f = false;
                string str_ob = str1 + str2;
                if (str_ob == "")
                {
                    f = true;
                    for (int i = 0; i < passangers.Count; i++)
                    {
                        pas = passangers[i];
                        pas.Vivod(ref Vivod_data);
                    }
                }
                if (f == false) {
                    for (int i = 0; i < trains.Count; i++)
                    {
                       if ((passangers[i].Surname+passangers[i].Name).Contains(str_ob))
                       {
                           passangers[i].Vivod(ref Vivod_data);
                       }
                   }
                }
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
            current_trains.Clear();
        }

        private void MMT_Click(object sender, EventArgs e)
        {
            MMT MMT_Form = new MMT(trains,ostanovki);
            MMT_Form.ShowDialog();
        }

        private void Editor_Click(object sender, EventArgs e)
        {
            Form2 secondForm = new Form2();
            secondForm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PoiskVsexPoezdov();
            PoiskVsexostanovok();
            PoiskVsexPasagirov();
        }


    private void Vivod_data_SelectedIndexChanged(object sender, EventArgs e)
        {
            string curItem = Vivod_data.SelectedItem.ToString();
            string[] info = curItem.Split(' ');
            Vivod_ostanovok.Visible = true;
            Vivod_ostanovok.Items.Clear();
            if (f == 1)
            {  
                mesto_n = int.Parse(info[4]);mesto_b = info[5];
                pictureBox.Visible = true;
                pictureBox.BackColor = Color.Transparent;
                label10.Visible = true;label11.Visible = true;label12.Visible = true;label13.Visible = true;  
                pictureBoxs.Size = new Size(22, 22);
                pictureBoxs.Load("seat.jpg");
                pictureBoxs.BackColor = Color.Transparent;
                int X = Vagon.PoiskX(mesto_n, mesto_b);
                int Y = Vagon.PoiskY(mesto_n, mesto_b);
                pictureBoxs.Location = new Point(X, Y);
                pictureBox.Controls.Add(pictureBoxs);
            }
            if ((f == 3) || (f == 2))
            {
                for (int i = 0; i <= current_trains.Count; i++)
                {
                    if (info[1] == trains[i].Nomer)
                    {
                        for (int j = 0; j < trains[i].Distance.Count; j++)
                            Vivod_ostanovok.Items.Add(trains[i].Distance[j]);
                    }
                }
            }
        }

       

  }
}
