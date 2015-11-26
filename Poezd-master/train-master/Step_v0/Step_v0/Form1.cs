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

        string[] info;
        string[] arr;
        int f = 0;
        string mesto_b;
        int mesto_n, i;
        Train t;
        public List<Train> trains = new List<Train>();
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

        public void Create_Train_obj() {

        }

        public void PoiskVsexPoezdov()
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load("C:\\Users\\Жанна\\Documents\\Visual Studio 2015\\Projects\\Poezd-master\\train-master\\Step_v0\\Step_v0\\Poezd.xml");
            XmlElement Root = Doc.DocumentElement;
            string nomer="",type="";
            
            List<string> distance1 = new List<string>();
            List<string> time = new List<string>();
            foreach (XmlNode node in Root)
            {
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
                       // for(int i=0;i<info.Length;i++)
                       // distance1.Add(info[i]);
                    }
                    if (childnode.Name == "time")
                    {
                        time.Add(childnode.InnerText.Split(' ').ToString());
                    }
                }
                t= new Train(nomer,type,info,time); 
                trains.Add(t);
                distance1.Clear();
            }
        }


        private void Receive_data1_Click(object sender, EventArgs e)
        {
            if (f == 3)
            {
                PoiskVsexPoezdov();
                t = trains[0];
                t.Vivod(ref Vivod_data, ref textBox4, ref textBox3, ref textNomer);
            }
            if (f == 1)
            {
                Passanger passanger = new Passanger();
                string str1 = textBox1.Text; string str2 = textBox6.Text; string str3 = textBox5.Text;
                List<string> collection = new List<string>();
                bool flag = true;
                if ((str1 == "") && (str2 == ""))
                {
                    flag = false;
                    collection = Passanger.PoiskVsexPasagirov();
                    for (int i = 0; i < collection.Count; i++)
                    {
                        Vivod_data.Items.Add(collection[i]);
                    }
                }
                if (flag == true)
                {
                    string str_ob = str1 + " " + str2;
                    collection = Passanger.PoiskPasagirov(str_ob);
                    if (collection[0].Contains("null"))
                    {
                        MessageBox.Show("Таких пассажиров нет");
                        Vivod_data.Items.Add("Таких пассажиров нет");
                    }
                    else
                    {
                        for (int i = 0; i < collection.Count; i++)
                        {
                            Vivod_data.Items.Add(collection[i]);
                        }
                    }
                }
                MMT.Visible = true;
                Vivod_data.Visible = true;
            }
            if (f == 2)
            {
                // Train train = new Train();string str1;
                string str4 = textBox3.Text; string str3 = textBox4.Text;
                List<string> collection = new List<string>();
                bool flag = true;
                if ((str3 == "") && (str4 == ""))
                {
                    flag = false;
                    // str1 = Train.PoiskVsexPoezdov();
                    for (int i = 0; i < collection.Count; i++)
                    {
                        info = collection[i].Split('{');
                        Vivod_data.Items.Add(info[0]);
                    }
                }
                if (flag == true)
                {
                    string str_ob = str3 + " " + str4;
                   // collection = Train.PoiskPoezda(str_ob);
                    if (collection[0].Contains("null"))
                    {
                        MessageBox.Show("Не найденно совпадений");
                        Vivod_data.Items.Add("Совпадений не найдено");
                    }
                    else
                    {
                        for (int i = 0; i < collection.Count; i++)
                        {
                            info = collection[i].Split('{');
                            Vivod_data.Items.Add(info[0]);
                        }
                        MMT.Visible = true;
                    }
                }
                Vivod_data.Visible = true;
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
            int[] coordinates; string[] time_Stops; int timeBegginHour, timeBegginMin, timeEndHour, timeEndMin;
            string[] Arr_Train = new string[Vivod_data.Items.Count];
            for (int j = 0; j < Vivod_data.Items.Count; j++)
            {
                string stroka = Vivod_data.Items[j].ToString();
                arr = stroka.Split(' ');
                Arr_Train[j] = arr[0];   // Массив поездов(номера)
            }
            int i = 0; // Переключатель между поездами

            // Данные для передачи в MMT
            coordinates = Massive.Coordinates_fill(Arr_Train[i].Split('/'));
            time_Stops = Massive.Time_stops_fill(Arr_Train[i].Split('/'));
            timeBegginHour = int.Parse(time_Stops[0]); timeBegginMin = int.Parse(time_Stops[1]); timeEndHour = int.Parse(time_Stops[time_Stops.Length - 2]); timeEndMin = int.Parse(time_Stops[time_Stops.Length - 1]);


            Vivod_ostanovok.Visible = true;
            for (int k = 0; k < coordinates.Length; k++) {
                Vivod_ostanovok.Items.Add(coordinates[k]);
            }
            for (int k = 0; k < time_Stops.Length; k++)
            {
                Vivod_ostanovok.Items.Add(time_Stops[k]);
            }
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
                string curItem = Vivod_data.SelectedItem.ToString();
                string[] info = curItem.Split(' ');
                mesto_n = int.Parse(info[5]);
                mesto_b = info[7];
                pictureBox.Visible = true;
                pictureBox.BackColor = Color.Transparent;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
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
                Vivod_ostanovok.Visible = true;
                string[] ostanovka;
                ostanovka = info[1].Split('-');
                for (int i=0;i<ostanovka.Length; i++)
                {
                    Vivod_ostanovok.Items.Add(ostanovka[i]);
                }
            }
        }

    }


    public class Massive {
        
        public static int[] Coordinates_fill(string[] arr_train)
        {
            string[] arr;int[] coordinates=new int[0];
            StreamReader fs3 = new StreamReader("BaseStope.txt");
            StreamReader fs1 = new StreamReader("BaseTrain.txt");
            List<string> collection = new List<string>();
            while (true)
            {
                string s = fs1.ReadLine();
                if (s != null)
                {
                    if (s.IndexOf(arr_train[0]) > -1)
                    {
                        collection.Add(s);
                    }
                }
                else
                    break;     
            }
            arr = collection[0].Split(' ');
            arr[arr.Length - 1] = "null";
            int j = 0;
            while (true)
            {
                string s = fs3.ReadLine();
                if (s != null)
                {
                    for(int i=7;i<arr.Count();i+=3)
                    if (s.IndexOf(arr[i]) > -1)
                    {
                          Array.Resize(ref coordinates, coordinates.Length + 2);
                          string[] info = s.Split(' ');
                          coordinates[j] = int.Parse(info[2]);
                          coordinates[j+1] = int.Parse(info[4]);
                          j = j + 2; 
                        }
                }
                else
                    break;
            }
            return coordinates;
        }

        public static string[] Time_stops_fill(string[] arr_train)
        {
            StreamReader fs1 = new StreamReader("BaseTrain.txt");
            List<string> collection = new List<string>();
            string[] arr; string[] time_stops = new string[0];
            while (true)
            {
                string s = fs1.ReadLine();
                if (s != null)
                {
                    if (s.IndexOf(arr_train[0]) > -1)
                    {
                        collection.Add(s);
                    }
                }
                else
                    break;
            }
            arr = collection[0].Split(' ');
            arr[arr.Length - 1] = "null";
            int k = 0;
            for (int j = 8; j < arr.Length; j += 3)
            {
                string[] time = (arr[j].Split(':'));
                Array.Resize(ref time_stops, time_stops.Length + 2);
                time_stops[k] = time[0].Replace("(","");
                time_stops[k+1] = time[1].Replace(")",""); 
                k = k + 2;
            }
            return time_stops;
        }
    }


}
