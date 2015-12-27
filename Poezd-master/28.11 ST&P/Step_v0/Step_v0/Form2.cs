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
using System.Xml.Linq;
using System.Xml;

namespace Step_v0
{
    public partial class Form2 : Form
    {
        public Form2(List<Train> trains)
        {
            this.InitializeComponent();

        }
        int f = 0,count_stops=0;
        List<string> stops = new List<string>();
        List<Stops> ostanovki = new List<Stops>();
        List<DateTime> Time = new List<DateTime>();  
        const string Path_file1 = "Poezd.xml",Path_file2="Passanger.xml",Path_file3= "Distanation.xml";
        private void button1_Click(object sender, EventArgs e)
        {
            if (f == 1)
            {          
                Add_poezd();
                CreateXMLPoezd();
                comboBox5.Items.Add(Form1.trains[Form1.trains.Count - 1].Nomer);
                Form1.c_b.Items.Add(Form1.trains[Form1.trains.Count - 1].Nomer);
                MessageBox.Show("Поезд "+ Form1.trains[Form1.trains.Count - 1].Nomer + " добавлен");
            }
            if (f == 2)
            {
                Add_distanation();
                CreateXMLDIstanation();
                ostanovki = null;
                comboBox4.Items.Add(Form1.marshruti[Form1.marshruti.Count-1].ID);
                MessageBox.Show("Путь "  + Form1.marshruti[Form1.marshruti.Count-1].ID +" " + "Добавлен");
            }
            if (f == 3) {               
                Add_passanger();
                CreateXMLPasssender();
                MessageBox.Show("Пассажир " + textBox5.Text + " " + textBox6.Text + " добавлен");
            }

        }

        void Add_passanger()
        {
            Bilet bil;Passanger pas;
            string surname = textBox5.Text, name = textBox6.Text, nomer = comboBox5.Text, vagon = comboBox6.Text, n_mesta = comboBox7.Text, b_mesta = comboBox8.Text;
            bil = new Bilet(nomer, vagon, n_mesta, b_mesta);
            Form1.bilets.Add(bil);
            pas = new Passanger(name, surname, nomer, Form1.bilets);
            for (int i = 0; i < Form1.trains.Count; i++)
            {
                if(nomer==Form1.trains[i].Nomer)
                Form1.trains[i].passanger.Add(pas);
            }
        }

        void CreateXMLPasssender() {
            XDocument doc = XDocument.Load(Path_file2);
            doc.Root.Add(new XElement("passagir",
                new XElement("info", Form1.passangers[Form1.passangers.Count-1].Surname + " " + Form1.passangers[Form1.passangers.Count - 1].Name + " " + Form1.passangers[Form1.passangers.Count - 1].Nomer_poezda +
                " " + Form1.passangers[Form1.passangers.Count - 1].Bilets[Form1.bilets.Count-1].Nomer_Vagona + " " + Form1.passangers[Form1.passangers.Count - 1].Bilets[Form1.bilets.Count - 1].Mesto_Nomer +
                " " + Form1.passangers[Form1.passangers.Count - 1].Bilets[Form1.bilets.Count - 1].Mesto_Bukva)));
            doc.Save(Path_file2);
        }

        void Add_distanation()
        {
            string name = textBox4.Text;
            Distanation dist;
            dist = new Distanation(name, ostanovki[0].Name, ostanovki[ostanovki.Count - 1].Name, ostanovki);
            Form1.marshruti.Add(dist);
        }

        void CreateXMLDIstanation() {
            XDocument doc = XDocument.Load(Path_file3);
            string stops="";
            for (int i = 0; i < ostanovki.Count; i++) {
                stops += ostanovki[i].Name;
                if (i < ostanovki.Count - 1) {
                    stops += " ";
                }
            }
            doc.Root.Add(new XElement("Distanation", new XAttribute("id", Form1.marshruti[Form1.marshruti.Count-1].ID),
               new XElement("Start", Form1.marshruti[Form1.marshruti.Count-1].Start),
               new XElement("End", Form1.marshruti[Form1.marshruti.Count-1].End),
               new XElement("stops", stops)));
            doc.Save(Path_file3);
        }


        void CreateXMLPoezd()
        {
            string time = "";
            for (int i = 0; i < count_stops;i++) {
                time += Form1.trains[Form1.trains.Count - 1].Time[i].ToString("HH:mm");
                if (i < count_stops-1) {
                   time += " ";
                }
            }
            XDocument doc = XDocument.Load(Path_file1);
            doc.Root.Add(new XElement("train",new XAttribute("name", Form1.trains[Form1.trains.Count - 1].Nomer),
                new XElement("type", Form1.trains[Form1.trains.Count - 1].Type),
                new XElement("distanation", Form1.trains[Form1.trains.Count - 1].ID),
                new XElement("time", time)));
            doc.Save(Path_file1);
        }

        void Add_poezd() {
            Random realRnd = new Random();
            List<Passanger> passangers = new List<Passanger>();
            string nomer = textBox1.Text, type = comboBox1.Text,id=comboBox4.Text;
            int i = 0;
            for (i = 0; i < Form1.marshruti.Count; i++) {
                if (id == Form1.marshruti[i].ID) {
                    count_stops = Form1.marshruti[i].Ostanovki.Count;
                }
            }
            string[] time1 = textBox2.Text.Split(':');
            string[] time2 = textBox3.Text.Split(':');
            int avarage = (int.Parse(time2[0]) * 60 + int.Parse(time2[1])) - (int.Parse(time1[0]) * 60 + int.Parse(time1[1]));
            int current_time = (int.Parse(time1[0]) * 60 + int.Parse(time1[1]));
            for (int j = 0; j < count_stops; j++) {
                if (j != 0)
                {
                    if (j == count_stops - 1)
                    {
                        current_time = (int.Parse(time2[0]) * 60 + int.Parse(time2[1]));
                    } else
                    current_time += (realRnd.Next(avarage/8-20, avarage / 8));
                }else current_time += 0;
                DateTime dt = new DateTime();
                int Hour = ((current_time) / 60);
                int Min = ((current_time) % 60);
                dt = dt.AddHours(Hour);
                dt = dt.AddMinutes(Min);
                Time.Add(dt);      
            }
            Train t = new Train(nomer, type, id, Time,passangers);
            Form1.trains.Add(t);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            f = 2;
            textBox4.Enabled = true;comboBox2.Enabled = true;comboBox3.Enabled = true;
            textBox1.Enabled = false; textBox2.Enabled = false; textBox3.Enabled = false; comboBox1.Enabled = false; comboBox4.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            f = 1;
            textBox1.Enabled = true;textBox2.Enabled = true;textBox3.Enabled = true;comboBox1.Enabled = true;comboBox4.Enabled = true;
            textBox4.Enabled = false; comboBox2.Enabled = false; comboBox3.Enabled = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            f = 3;
            comboBox5.Enabled = true; comboBox6.Enabled = true;
        }

        private void combobox2_fil(string cur_it) {
            for (int i = 0; i < Form1.marshruti.Count; i++)
            {
                if (cur_it == Form1.marshruti[i].ID)
                {
                    for (int j = 0; j < Form1.marshruti[i].Ostanovki.Count; j++)
                    {
                        listBox1.Items.Add(Form1.marshruti[i].Ostanovki[j].Name);
                    }
                }
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            listBox1.Items.Clear(); listBox1.Visible = true;
            string cutItem = comboBox2.SelectedItem.ToString();
            combobox2_fil(cutItem);
        }

        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            listBox1.Items.Clear();listBox1.Visible = true;
            string cutItem = comboBox4.SelectedItem.ToString();
            combobox2_fil(cutItem);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string curitem = listBox1.SelectedItem.ToString();
            fil_ostanivki(curitem);
            MessageBox.Show("Остановка" + " "  + curitem + " " + "Добавлена");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Form1.marshruti.Count; i++)
                comboBox4.Items.Add(Form1.marshruti[i].ID);
            for (int i = 0; i < Form1.trains.Count - 1; i++)
                comboBox5.Items.Add(Form1.trains[i].Nomer);
            for (int i = 0; i < 3; i++)
                comboBox2.Items.Add(Form1.marshruti[i].ID);
        }

        public void fil_ostanivki(string str_it) {
            ostanovki.Add(Form1.Poiskostanovoki(str_it));
        }


    }
}
