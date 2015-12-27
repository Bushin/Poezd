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
    public partial class Editor_passanger : Form
    {
        public Editor_passanger()
        {
            InitializeComponent();
        }

        const string Path_file2 = "Passanger.xml";
        private void button1_Click(object sender, EventArgs e)
        {
            Add_passanger();
            CreateXMLPasssender();
            MessageBox.Show("Пассажир " + textBox5.Text + " " + textBox6.Text + " добавлен");
        }

        void Add_passanger()
        {
            Bilet bil; Passanger pas;
            string surname = textBox5.Text, name = textBox6.Text, nomer = comboBox5.Text, vagon = comboBox6.Text, n_mesta = comboBox7.Text, b_mesta = comboBox8.Text;
            bil = new Bilet(nomer, vagon, n_mesta, b_mesta);
            Glades.bilets.Add(bil);
            pas = new Passanger(name, surname, nomer, Glades.bilets);
            for (int i = 0; i < Glades.trains.Count; i++)
            {
                if (nomer == Glades.trains[i].Nomer)
                    Glades.trains[i].passanger.Add(pas);
            }
        }

        void CreateXMLPasssender()
        {
            XDocument doc = XDocument.Load(Path_file2);
            string surname = textBox5.Text, name = textBox6.Text, nomer = comboBox5.Text, vagon = comboBox6.Text, n_mesta = comboBox7.Text, b_mesta = comboBox8.Text;
            doc.Root.Add(new XElement("passagir",
                new XElement("info", surname + " " + name + " " + nomer +
                " " + vagon + " " + n_mesta +
                " " + b_mesta)));
            doc.Save(Path_file2);
        }

        private void Editor_passanger_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Glades.trains.Count - 1; i++)
                comboBox5.Items.Add(Glades.trains[i].Nomer);
        }
    }
}
