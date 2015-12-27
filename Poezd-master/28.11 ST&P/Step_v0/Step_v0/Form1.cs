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
        int mesto_n;int count = 0;public static int K = 0;
        Train t; Passanger pas;Bilet bil;Distanation dist;
        public static List<Train> trains = new List<Train>();
        public static List<Distanation> marshruti = new List<Distanation>();
        public static List<Passanger> passangers = new List<Passanger>();
        public static List<Bilet> bilets = new List<Bilet>();
        public List<Train> current_trains = new List<Train>();
        PictureBox pictureBoxs = new PictureBox();
        public static ComboBox c_b = new ComboBox();

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
            Doc.Load("Poezd.xml");
            XmlElement Root = Doc.DocumentElement;
            string nomer="",type="",id="";
            int Hour, Min;
            foreach (XmlNode node in Root)
            {
                List<string> distance = new List<string>();
                List<DateTime> time = new List<DateTime>();
                List<Passanger> passangers = new List<Passanger>();
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
                        id = childnode.InnerText;
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
                passangers = PoiskVsexPasagirov(nomer);
                t= new Train(nomer,type,id,time,passangers);           
                trains.Add(t);
                distance = null;
                time = null;
                passangers = null;
            }
        }

        public static Stops Poiskostanovoki(string name_ostanovki)
        {
            Stops ost;
            XmlDocument Doc = new XmlDocument();
            Doc.Load("Ostanovki.xml");
            XmlElement Root = Doc.DocumentElement;
            string name = "";int cor_x=0, cor_y=0;
            foreach (XmlNode node in Root)
            {                
                if (node.Attributes.Count > 0)
                {
                    XmlNode attr = node.Attributes.GetNamedItem("name");
                    if (attr != null)
                        name = attr.Value;
                }
                if (name_ostanovki == name)
                {
                    foreach (XmlNode childnode in node.ChildNodes)
                    {
                        if (childnode.Name == "info")
                        {
                            string[] info = childnode.InnerText.Split(' ');
                            cor_x = int.Parse(info[0]);
                            cor_y = int.Parse(info[1]);
                        }
                    }
                    return ost = new Stops(name, cor_x, cor_y);
                }
            }
            return ost = new Stops("null", 0, 0);
        }

        public void PoiskVsexmarshrutov()
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load("Distanation.xml");
            XmlElement Root = Doc.DocumentElement;
            string start = "", end = "",id="";
            foreach (XmlNode node in Root)
            {
                List<Stops> ostanovki = new List<Stops>(); 
                if (node.Attributes.Count > 0)
                {
                    XmlNode attr = node.Attributes.GetNamedItem("id");
                    if (attr != null)
                        id = attr.Value;
                }
                foreach (XmlNode childnode in node.ChildNodes)
                {
                    if (childnode.Name == "Start")
                    {
                        start = childnode.InnerText;
                    }
                    if (childnode.Name == "End")
                    {
                        end = childnode.InnerText;        
                    }
                    if (childnode.Name == "stops")
                    {
                        string[] info = childnode.InnerText.Split(' ');
                        for (int i = 0; i < info.Length; i++)
                        {                          
                            ostanovki.Add(Poiskostanovoki(info[i]));
                        }
                    }
                }
                dist = new Distanation(id,start,end,ostanovki);
                marshruti.Add(dist);
                ostanovki  = null;
            }
        }

        public static List<Passanger> PoiskVsexPasagirov(string nomer)
        {
            Passanger pas; Bilet bil;
            XmlDocument Doc = new XmlDocument();
            List<Passanger> passangers_for_current_train = new List<Passanger>();
            Doc.Load("Passanger.xml");
            XmlElement Root = Doc.DocumentElement;
            string n = "", s = "", nomer_poezda = "", nomer_vagona = "", mesto_nomer = "", mesto_bukva = "";
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
                if (nomer == nomer_poezda)
                {
                    bil = new Bilet(nomer_poezda, nomer_vagona, mesto_nomer, mesto_bukva);
                    bilets.Add(bil);
                    pas = new Passanger(n, s, nomer_poezda, bilets);
                    passangers_for_current_train.Add(pas);
                    
                }

            }
            return passangers_for_current_train;
        }

        void Vivod_poezdov_marshrutov(int i) {
            for (int j = 0; j < marshruti.Count; j++)
            {
                if (trains[i].ID == marshruti[j].ID)
                {
                    trains[i].last_ostanovka(marshruti[j]);
                    current_trains.Add(trains[i]);
                    trains[i].Vivod(ref textBox4, ref textBox3, ref Vivod, ref count, ref marshruti[j].Start, ref marshruti[j].End);
                    count++;
                }
            }
        }



        private void Receive_data1_Click(object sender, EventArgs e)
        {
            if (f == 3)
            {
                string str = c_b.Text;int proverka_na_povtor=0;               
                if (K != 0)
                { 
                  proverka_na_povtor=Proverka_poezdov(str,current_trains); 
                }
                
                if ((str == "") && (K == 0))
                {                 
                    for (int i = 0; i < trains.Count; i++)
                    {
                        K++;
                        Vivod_poezdov_marshrutov(i);
                    }
                }
                else
                {
                    if (proverka_na_povtor == 0)
                    {
                        for (int i = 0; i < trains.Count; i++)
                        {
                            if (str == trains[i].Nomer)
                            {
                                K++;
                                Vivod_poezdov_marshrutov(i); 
                            }
                        }
                    }
                }
            }

            if (f == 2)
            {
                string str1 = textBox4.Text, str2 = textBox3.Text; int proverka_na_povtor = 0;
                string str_ob = str1 + str2;
                if (K != 0)
                {
                    proverka_na_povtor = Proverka_poezdov(str_ob, current_trains);
                }

                if ((str_ob == "") && (K == 0))
                {
                    for (int i = 0; i < trains.Count; i++)
                    {
                        K++;
                        Vivod_poezdov_marshrutov(i);
                    }
                }
                else
                {
                    if (proverka_na_povtor == 0)
                    {
                        for (int i = 0; i < trains.Count; i++)
                        {
                            if ((marshruti[i].Start + marshruti[i].End).Contains(str_ob))
                            {
                                K++;
                                Vivod_poezdov_marshrutov(i);
                            }
                        }
                    }
                }
            }

            if (f == 1)
            {
                string str1 = textBox1.Text;string str2 = textBox6.Text;vivod_pas.Rows.Add();vivod_bil.Rows.Add();
                string str_ob = str1 + str2;
                if (str_ob == "")
                {
                    for (int i = 0; i < trains.Count; i++)
                    {
                        for (int j = 0; j < trains[i].passanger.Count; j++)
                        {
                            trains[i].passanger[j].Vivod(ref vivod_pas, ref count);
                            count++;
                        }
                    }
                }
                else {
                    for (int i = 0; i < trains.Count; i++)
                    {
                        for (int j = 0; j < trains[i].passanger.Count; j++)
                        {
                            if ((trains[i].passanger[j].Surname + trains[i].passanger[j].Name).Contains(str_ob))
                            {
                                trains[i].passanger[j].Vivod(ref vivod_pas, ref count);
                                count++;
                            }
                        }
                    }
                }
            }
        }

        private void Clean_Click(object sender, EventArgs e)
        {
            Vivod_ostanovok.Items.Clear();
            Vivod_ostanovok.Visible = false;
            textBox1.Clear(); c_b.Text=""; textBox4.Clear(); textBox6.Clear(); textBox3.Clear();
            label10.Visible = false; label11.Visible = false; label12.Visible = false; label13.Visible = false;
            pictureBoxs.Image = null;
            pictureBox.Visible = false;
            pictureBox.Refresh();
            current_trains.Clear();count = 0;
            Vivod.Rows.Clear();vivod_pas.Rows.Clear();vivod_bil.Visible = false;K = 0;
        }

        private void MMT_Click(object sender, EventArgs e)
        {
            MMT MMT_Form = new MMT(current_trains,marshruti);
            MMT_Form.ShowDialog();
        }

        private void Editor_Click(object sender, EventArgs e)
        {
            Form2 secondForm = new Form2(trains);
            secondForm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PoiskVsexmarshrutov();
            PoiskVsexPoezdov();          
            c_b.Location= new Point(801,73);c_b.Visible = false;
            this.Controls.Add(c_b);
            for (int i=0;i<trains.Count;i++)
            c_b.Items.Add(trains[i].Nomer);
        }


        private void Vivod_SelectionChanged(object sender, EventArgs e)
        {         
           /* string curItem = Vivod[1, Vivod.CurrentRow.Index].Value.ToString();
            Vivod_ostanovok.Items.Clear();
            if ((f == 3) || (f == 2))
            {
                for (int i = 0; i < trains.Count; i++)
                {
                    if (curItem == trains[i].Nomer)
                    {
                        for (int j = 0; j < trains[i]..Count; j++)
                            Vivod_ostanovok.Items.Add(trains[i].Distance[j]);
                    }
                }
            Vivod_ostanovok.Visible = true;
           }*/
        }

        public static int Proverka_poezdov(string s,List<Train> c_t){
            int p = 0;
            for (int i = 0; i < c_t.Count; i++)
            {
                if (s == c_t[i].Nomer)
                {
                     p = 1;
                    return p;
                }
                else {  p = 0; }
            }
            return p;
        }


        private void vivod_pas_SelectionChanged(object sender, EventArgs e)
        {
            if (K > 0) { 
            string curSurname = vivod_pas[0, vivod_pas.CurrentRow.Index].Value.ToString();
            string curName = vivod_pas[1, vivod_pas.CurrentRow.Index].Value.ToString();
            vivod_bil.Rows.Clear();
            string Fio = ""; Fio+= curSurname + curName;
            for (int i = 0; i < trains.Count; i++) {
                    for (int j = 0; j < trains[i].passanger.Count; j++)
                    {
                        if (Fio == (trains[i].passanger[j].Surname + trains[i].passanger[j].Name))
                        {
                            trains[i].passanger[j].Bilets[j].Vivod(ref vivod_bil, ref i);
                            mesto_n = int.Parse(trains[i].passanger[j].Bilets[j].Mesto_Nomer); mesto_b = trains[i].passanger[j].Bilets[j].Mesto_Bukva;
                            pictureBox.Visible = true;
                            pictureBox.BackColor = Color.Transparent;
                            label10.Visible = true; label11.Visible = true; label12.Visible = true; label13.Visible = true;
                            pictureBoxs.Size = new Size(22, 22);
                            pictureBoxs.Load("seat.jpg");
                            pictureBoxs.BackColor = Color.Transparent;
                            int X = Vagon.PoiskX(mesto_n, mesto_b);
                            int Y = Vagon.PoiskY(mesto_n, mesto_b);
                            pictureBoxs.Location = new Point(X, Y);
                            pictureBox.Controls.Add(pictureBoxs);
                        }
                    }
                }
            }           
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string curent_index = toolStripComboBox1.SelectedIndex.ToString();
            switch (curent_index) {
                case "0":
                        Editor_distanation firstForm = new Editor_distanation();
                        firstForm.ShowDialog();
                        break;
                case "1":
                    Form2 secondForm = new Form2(trains);
                    secondForm.ShowDialog();
                    break;
                case "2":
                    Editor_passanger thirdForm = new Editor_passanger();
                    thirdForm.ShowDialog();
                    break;
            }
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string curitem = toolStripComboBox2.SelectedIndex.ToString();
            switch (curitem) {
                case "0":
                    label9.Visible = true; label2.Visible = false; label3.Visible = false; label7.Visible = false; label8.Visible = false;
                    c_b.Visible = true; textBox3.Visible = false; textBox4.Visible = false; textBox1.Visible = false; textBox6.Visible = false;
                    Receive_data1.Visible = true;Clean.Visible = true;MMT.Visible = true;
                    Vivod_ostanovok.Items.Clear();
                    label6.ForeColor = Color.Gold;
                    label1.ForeColor = Color.White;
                    label5.ForeColor = Color.White;
                    vivod_pas.Visible = false; count = 0; K = 0;
                    break;
                case "1":
                    textBox3.Visible = true; textBox4.Visible = true; textBox1.Visible = false; textBox6.Visible = false; c_b.Visible = false;
                    label2.Visible = false; label3.Visible = false; label9.Visible = false; label7.Visible = true; label8.Visible =true;
                    Receive_data1.Visible = true; Clean.Visible = true; MMT.Visible = true;
                    Vivod_ostanovok.Items.Clear();
                    label5.ForeColor = Color.Gold;
                    label1.ForeColor = Color.White;
                    label6.ForeColor = Color.White; K = 0;
                    break;
                case "2":
                    textBox1.Visible = true; textBox6.Visible = true; textBox3.Visible = false; textBox4.Visible = false; c_b.Visible = false; ;
                    label2.Visible = true; label3.Visible = true;label9.Visible = false; label7.Visible = false; label8.Visible = false;
                    Receive_data1.Visible = true; Clean.Visible = true; MMT.Visible = true;
                    Vivod_ostanovok.Items.Clear();
                    label1.ForeColor = Color.Gold;
                    label5.ForeColor = Color.White;
                    label6.ForeColor = Color.White;
                    Vivod.Visible = false; count = 0; K = 0;
                    break;

            }
        }
    }
}
