using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Step_v0
{
    public partial class MMT : Form
    {
        Train testTR;
        List<Train> list_poezdov;
        List<Stops> list_ostanovok;
        List<TrainControl> list_ctrl_train= new List<TrainControl>();

        public MMT(List<Train> trains,List<Stops> ostanovki)
        {
            list_poezdov = trains;
            list_ostanovok = ostanovki;
            InitializeComponent();
            Opacity = 0;
            Timer timer = new Timer();
            timer.Tick += new EventHandler((sender, e) =>
            {
                if ((Opacity += 0.02d) == 1) timer.Stop();
            });
            timer.Interval = 3;
            timer.Start();
           
        }

        private void MMT_Load(object sender, EventArgs e)
        {
            create_ctrl();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }
        public void create_ctrl()
        {
            bool flag1 = true;
            bool flag2 = true;
            int count = 0;
            int i = 0;
            while (flag1)
            {

                //count = 0;
                //  for (int count = 0; count < list_ostanovok.Count; count++)
                while (flag2)
                {
                    if (list_poezdov[i].last_stops == list_ostanovok[count].Name)
                    {
                        if (list_ctrl_train.Count == 0)
                        {
                            TrainControl tr = new TrainControl(list_ostanovok[count].X-10, list_ostanovok[count].Y-22);
                            pictureBox1.Controls.Add(tr);
                            tr.coll_train.Add(list_poezdov[i]);
                            i++;
                            flag2 = false;
                            list_ctrl_train.Add(tr);
                           
                            tr.Click += new System.EventHandler(tr_Click);
                            tr.MouseEnter += new System.EventHandler(tr_MouseEnter);
                            tr.MouseLeave += new System.EventHandler(tr_MouseLeave);
                        }
                        else
                        {
                            for(int k=0;k<list_ctrl_train.Count;k++)
                            {
                                TrainControl test_tr;
                                test_tr = list_ctrl_train[k];
                                if ((test_tr.ctrl_x == (list_ostanovok[count].X-10)) & (test_tr.ctrl_y == (list_ostanovok[count].Y-22)))
                                {
                                    test_tr.coll_train.Add(list_poezdov[i]);
                                    i++;
                                    flag2 = false;
                                  
                                }
                                    }

                                if(flag2==true)
                                {
                                    TrainControl tr = new TrainControl(list_ostanovok[count].X-10, list_ostanovok[count].Y-22);
                                    tr.Click += new System.EventHandler(tr_Click);
                                    tr.MouseEnter += new System.EventHandler(tr_MouseEnter);
                                    tr.MouseLeave += new System.EventHandler(tr_MouseLeave);
                                    pictureBox1.Controls.Add(tr);
                                    tr.coll_train.Add(list_poezdov[i]);
                                    i++;
                                    flag2 = false;
                                    list_ctrl_train.Add(tr);
                                    
                                }

                            
                        }


                    }
                    else
                    {
                        count++;
                    }


                }
                //count++;
                if (i == list_poezdov.Count)
                {
                    flag1 = false;
                    flag2 = false;
                }
                else
                {
                    flag2 = true;
                    count = 0;
                }

            }

        }

        private void tr_Click(object sender, EventArgs e)
        {
            TrainControl poezd = (TrainControl)sender;
           
 
            listBox1.Items.Clear();
            for (int i = 0; i < poezd.coll_train.Count; i++)
            {
                if (poezd.coll_train[i].motion)
                    listBox1.Items.Add("Поезд № " + poezd.coll_train[i].Nomer + ' ' + "В пути");
                else
                    listBox1.Items.Add("Поезд № " + poezd.coll_train[i].Nomer + ' ' + "Стоит");
            }
          
            listBox1.Size = new Size(115, 75);
            listBox1.Visible = true;
            listBox1.Location = new Point(poezd.ctrl_x, 40 + poezd.ctrl_y);

        }


        private void tr_MouseEnter(object sender, EventArgs e)
        {
            TrainControl poezd = (TrainControl)sender;
            poezd.Size = new Size(30, 30);
        }
        private void tr_MouseLeave(object sender, EventArgs e)
        {
            TrainControl poezd = (TrainControl)sender;
            poezd.Size = new Size(21, 23);
        }



        private void button1_MouseEnter(object sender, EventArgs e)
        {
           // button1.Size = new Size(20, 20);
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.Size = new Size(20, 20);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Size = new Size(75, 23);
        }

    }
}
