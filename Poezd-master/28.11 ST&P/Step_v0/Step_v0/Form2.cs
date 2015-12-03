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
        public Form2(List<Train> trains, List<Stops> ostanovki)
        {
            this.InitializeComponent();
            list_ostanovok = ostanovki;
           // list_trains = trains;
        }
        List<Stops> list_ostanovok;
       // List<Train> list_trains;
        List<string> stops = new List<string>();
        List<DateTime> Time = new List<DateTime>();  
        const string Path_file = "Poezd.xml";
        private void button1_Click(object sender, EventArgs e)
        {
            Add_poezd();
            CreateXML();
            Form1.c_b.Items.Add(Form1.trains[Form1.trains.Count - 1].Nomer);
        }

        void CreateXML()
        {
            string dist = "";
            string time = "";
            for (int i = 0; i < Form1.trains[Form1.trains.Count-1].Distance.Count;i++) {
                dist += Form1.trains[Form1.trains.Count - 1].Distance[i];
                time += Form1.trains[Form1.trains.Count - 1].Time[i].ToString("HH:mm");
                if (i < Form1.trains[Form1.trains.Count - 1].Distance.Count-1) {
                    dist += " ";time += " ";
                }
            }
            XDocument doc = XDocument.Load(Path_file);
            doc.Root.Add(new XElement("train",new XAttribute("name", Form1.trains[Form1.trains.Count - 1].Nomer),
                new XElement("type", Form1.trains[Form1.trains.Count - 1].Type),
                new XElement("distanation", dist),
                new XElement("time", time)));
            doc.Save(Path_file);
        }

        void Add_poezd() {
            Random realRnd = new Random();
            string nomer = textBox1.Text;
            string type = comboBox1.Text;
            int i=0;
            for (i = 0; i < list_ostanovok.Count; i++) {
                if (comboBox2.Text == list_ostanovok[i].Name) {
                    stops.Add(list_ostanovok[i].Name);
                    while (comboBox3.Text != list_ostanovok[i].Name)
                    {
                        i++;
                        stops.Add(list_ostanovok[i].Name);
                    }break;
                }
            }
            string[] time1 = textBox2.Text.Split(':');
            string[] time2 = textBox3.Text.Split(':');
            int avarage = (int.Parse(time2[0]) * 60 + int.Parse(time2[1])) - (int.Parse(time1[0]) * 60 + int.Parse(time1[1]));
            int current_time = (int.Parse(time1[0]) * 60 + int.Parse(time1[1]));
            for (int j = 0; j < stops.Count; j++) {
                if (j != 0)
                {
                    if (j == stops.Count - 1)
                    {
                        current_time = (int.Parse(time2[0]) * 60 + int.Parse(time2[1]));
                    } else
                    current_time += (realRnd.Next(40, avarage / 8));
                }else current_time += 0;
                DateTime dt = new DateTime();
                int Hour = ((current_time) / 60);
                int Min = ((current_time) % 60);
                dt = dt.AddHours(Hour);
                dt = dt.AddMinutes(Min);
                Time.Add(dt);      
            }
           Train t = new Train(nomer, type, stops, Time);
            Form1.trains.Add(t);
        }


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
