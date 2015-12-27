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
 
        string mesto_b;string curitem_inteface;
        int mesto_n;int count = 0;public static int K = 0;
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

        Glades glades = new Glades();
        void Function()
        {
            glades.PoiskVsexPoezdov();
            glades.PoiskVsexmarshrutov();
        }

        private void Receive_data1_Click(object sender, EventArgs e)
        {
            if (curitem_inteface == "0")
            {
                string str = c_b.Text; int proverka_na_povtor = 0;
                if (K != 0)
                {
                    proverka_na_povtor = Glades.Proverka_poezdov(str, glades.current_trains);
                }

                if ((str == "") && (K == 0))
                {
                    for (int i = 0; i < Glades.trains.Count; i++)
                    {
                        K++;
                        Vivod_poezdov_marshrutov(i);
                    }
                }
                else
                {
                    if (proverka_na_povtor == 0)
                    {
                        for (int i = 0; i < Glades.trains.Count; i++)
                        {
                            if (str == Glades.trains[i].Nomer)
                            {
                                K++;
                                Vivod_poezdov_marshrutov(i);
                            }
                        }
                    }
                }
            }

            if (curitem_inteface == "1")
            {
                string str1 = textBox4.Text, str2 = textBox3.Text; int proverka_na_povtor = 0;
                string str_ob = str1 + str2;
                if (K != 0)
                {
                    proverka_na_povtor = Glades.Proverka_poezdov(str_ob, glades.current_trains);
                }

                if ((str_ob == "") && (K == 0))
                {
                    for (int i = 0; i < Glades.trains.Count; i++)
                    {
                        K++;
                        Vivod_poezdov_marshrutov(i);
                    }
                }
                else
                {
                    if (proverka_na_povtor == 0)
                    {
                        for (int i = 0; i < Glades.trains.Count; i++)
                        {
                            if ((Glades.marshruti[i].Start + Glades.marshruti[i].End).Contains(str_ob))
                            {
                                K++;
                                Vivod_poezdov_marshrutov(i);
                            }
                        }
                    }
                }
            }

            if (curitem_inteface == "2")
            {
                string str1 = textBox1.Text; string str2 = textBox6.Text; vivod_pas.Rows.Add(); vivod_bil.Rows.Add();
                string str_ob = str1 + str2;
                if (str_ob == "")
                {
                    for (int i = 0; i < Glades.trains.Count; i++)
                    {
                        for (int j = 0; j < Glades.trains[i].passanger.Count; j++)
                        {
                            Glades.trains[i].passanger[j].Vivod(ref vivod_pas, ref count);
                            count++;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < Glades.trains.Count; i++)
                    {
                        for (int j = 0; j < Glades.trains[i].passanger.Count; j++)
                        {
                            if ((Glades.trains[i].passanger[j].Surname + Glades.trains[i].passanger[j].Name).Contains(str_ob))
                            {
                                Glades.trains[i].passanger[j].Vivod(ref vivod_pas, ref count);
                                count++;
                            }
                        }
                    }
                }
            }
            
        }

        void Vivod_poezdov_marshrutov(int i)
        {
            for (int j = 0; j < Glades.marshruti.Count; j++)
            {
                if (Glades.trains[i].ID == Glades.marshruti[j].ID)
                {
                    Glades.trains[i].last_ostanovka(Glades.marshruti[j]);
                    glades.current_trains.Add(Glades.trains[i]);
                    Glades.trains[i].Vivod(ref textBox4, ref textBox3, ref Vivod, ref count, ref Glades.marshruti[j].Start, ref Glades.marshruti[j].End);
                    count++;
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
            glades.current_trains.Clear();count = 0;
            Vivod.Rows.Clear();vivod_pas.Rows.Clear();vivod_bil.Visible = false;K = 0;
        }

        private void MMT_Click(object sender, EventArgs e)
        {
            MMT MMT_Form = new MMT(glades.current_trains,Glades.marshruti);
            MMT_Form.ShowDialog();
        }

        private void Editor_Click(object sender, EventArgs e)
        {
            Form2 secondForm = new Form2(Glades.trains);
            secondForm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Function();
            c_b.Location= new Point(801,73);c_b.Visible = false;
            this.Controls.Add(c_b);
            for (int i=0;i<Glades.trains.Count;i++)
            c_b.Items.Add(Glades.trains[i].Nomer);
        }


        private void vivod_pas_SelectionChanged(object sender, EventArgs e)
        {
            if (K > 0) { 
            string curSurname = vivod_pas[0, vivod_pas.CurrentRow.Index].Value.ToString();
            string curName = vivod_pas[1, vivod_pas.CurrentRow.Index].Value.ToString();
            vivod_bil.Rows.Clear();
            string Fio = ""; Fio+= curSurname + curName;
            for (int i = 0; i < Glades.trains.Count; i++) {
                    for (int j = 0; j < Glades.trains[i].passanger.Count; j++)
                    {
                        if (Fio == (Glades.trains[i].passanger[j].Surname + Glades.trains[i].passanger[j].Name))
                        {
                            Glades.trains[i].passanger[j].Bilets[j].Vivod(ref vivod_bil, ref i);
                            mesto_n = int.Parse(Glades.trains[i].passanger[j].Bilets[j].Mesto_Nomer); mesto_b = Glades.trains[i].passanger[j].Bilets[j].Mesto_Bukva;
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
                        Form2 secondForm = new Form2(Glades.trains);
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
            curitem_inteface = toolStripComboBox2.SelectedIndex.ToString();
            switch (curitem_inteface) {
                case "0":
                    label9.Visible = true; label2.Visible = false; label3.Visible = false; label7.Visible = false; label8.Visible = false;
                    c_b.Visible = true; textBox3.Visible = false; textBox4.Visible = false; textBox1.Visible = false; textBox6.Visible = false;
                    Receive_data1.Visible = true;Clean.Visible = true;MMT.Visible = true; label10.Visible = false; label11.Visible = false; label12.Visible = false; label13.Visible = false;
                    Vivod_ostanovok.Items.Clear();
                    label6.ForeColor = Color.Gold;
                    label1.ForeColor = Color.White;
                    label5.ForeColor = Color.White;
                    vivod_pas.Visible = false; pictureBox.Visible = false; count = 0; K = 0; vivod_bil.Visible = false; vivod_pas.Visible = false;
                    break;
                case "1":
                    textBox3.Visible = true; textBox4.Visible = true; textBox1.Visible = false; textBox6.Visible = false; c_b.Visible = false;
                    label2.Visible = false; label3.Visible = false; label9.Visible = false; label7.Visible = true; label8.Visible =true;
                    Receive_data1.Visible = true; Clean.Visible = true; MMT.Visible = true; label10.Visible = false; label11.Visible = false; label12.Visible = false; label13.Visible = false;
                    Vivod_ostanovok.Items.Clear();
                    label5.ForeColor = Color.Gold;
                    label1.ForeColor = Color.White;
                    label6.ForeColor = Color.White; K = 0;
                    Vivod.Visible = false; pictureBox.Visible = false;vivod_bil.Visible = false;vivod_pas.Visible = false;
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
