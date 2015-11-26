using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace Step_v0
{
    public class Train
    {
        public string Nomer, Type;
        public List<string>  Time;
        public string[] Distance;

        public Train(string n, string t, string[] ostanovki, List<string> Vreme_ostanovkok) {
            Nomer = n;
            Type = t;
            Distance = ostanovki;
            Time = Vreme_ostanovkok;
        }


        public void Vivod(ref ListBox vivod, ref TextBox t4, ref TextBox t3, ref TextBox nomer) {
            string str = nomer.Text;
            List<string> collection = new List<string>();
            bool flag = true;
            if (str == "")
            {
                flag = false;
                vivod.Items.Add(Nomer + ' ' + Type + ' ' +Distance.Length);
                if (flag == true)
                {
                    // collection = Train.PoiskPoezda(str);
                    if (collection[0].Contains("null"))
                    {
                        vivod.Items.Add("Совпадений не найдено");
                    }
                    else
                    {
                        //info = collection[i].Split('{');
                        // vivod.Items.Add(info[i]);
                        // marsh = info[0].Split(' ');
                        //t4.Text = marsh[2];
                        //t3.Text = marsh[3];
                    }
                }
                vivod.Visible = true;
            }
        }
    }
}

