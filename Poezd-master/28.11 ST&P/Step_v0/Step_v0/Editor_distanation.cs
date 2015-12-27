using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Step_v0
{
    public partial class Editor_distanation : Form
    {
        public Editor_distanation()
        {
            InitializeComponent();
        }

        List<Stops> ostanovki = new List<Stops>();
        const string Path_file3 = "Distanation.xml";
        private void button1_Click(object sender, EventArgs e)
        {
            Add_distanation();
            CreateXMLDIstanation();
            ostanovki = null;
            MessageBox.Show("Путь " + Glades.marshruti[Glades.marshruti.Count - 1].ID + " " + "Добавлен");
        }

        void Add_distanation()
        {
            string name = textBox4.Text;
            Distanation dist;
            dist = new Distanation(name, ostanovki[0].Name, ostanovki[ostanovki.Count - 1].Name, ostanovki);
            Glades.marshruti.Add(dist);
        }

        void CreateXMLDIstanation()
        {
            XDocument doc = XDocument.Load(Path_file3);
            string stops = "";
            for (int i = 0; i < ostanovki.Count; i++)
            {
                stops += ostanovki[i].Name;
                if (i < ostanovki.Count - 1)
                {
                    stops += " ";
                }
            }
            doc.Root.Add(new XElement("Distanation", new XAttribute("id", Glades.marshruti[Glades.marshruti.Count - 1].ID),
               new XElement("Start", Glades.marshruti[Glades.marshruti.Count - 1].Start),
               new XElement("End", Glades.marshruti[Glades.marshruti.Count - 1].End),
               new XElement("stops", stops)));
            doc.Save(Path_file3);
        }

        private void combobox2_fil(string cur_it)
        {
            for (int i = 0; i < Glades.marshruti.Count; i++)
            {
                if (cur_it == Glades.marshruti[i].ID)
                {
                    for (int j = 0; j < Glades.marshruti[i].Ostanovki.Count; j++)
                    {
                        listBox1.Items.Add(Glades.marshruti[i].Ostanovki[j].Name);
                    }
                }
            }
        }



        private void Editor_distanation_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                comboBox2.Items.Add(Glades.marshruti[i].ID);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string curitem = listBox1.SelectedItem.ToString();
            fil_ostanivki(curitem);
            MessageBox.Show("Остановка" + " " + curitem + " " + "Добавлена");
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            listBox1.Items.Clear(); listBox1.Visible = true;
            string cutItem = comboBox2.SelectedItem.ToString();
            combobox2_fil(cutItem);
        }

        public void fil_ostanivki(string str_it)
        {
            ostanovki.Add(Glades.Poiskostanovoki(str_it));
        }
    }
}
