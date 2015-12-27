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
        int count_stops=0;
        List<string> stops = new List<string>();
        List<DateTime> Time = new List<DateTime>();
        const string Path_file1 = "Poezd.xml";
        private void button1_Click(object sender, EventArgs e)
        {                  
                Add_poezd();
                CreateXMLPoezd();
                Form1.c_b.Items.Add(Form1.trains[Form1.trains.Count - 1].Nomer);
                MessageBox.Show("Поезд "+ Form1.trains[Form1.trains.Count - 1].Nomer + " добавлен");   
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

        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            listBox1.Items.Clear();listBox1.Visible = true;
            string cutItem = comboBox4.SelectedItem.ToString();
            combobox2_fil(cutItem);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Form1.marshruti.Count; i++)
                comboBox4.Items.Add(Form1.marshruti[i].ID);
        }

    }
}
