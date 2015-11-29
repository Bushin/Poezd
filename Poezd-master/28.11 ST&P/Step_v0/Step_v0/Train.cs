using System.IO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace Step_v0
{
    public class Train
    {
        public string Nomer, Type;
        public List<DateTime> Time;
        public List<string>Distance;
        public string last_stops;
        public  bool motion;

        public Train(string n, string t, List<string> ostanovki, List<DateTime> Vreme_ostanovkok) {
            Nomer = n;
            Type = t;
            Distance = ostanovki;
            Time = Vreme_ostanovkok;
        }


        public void Vivod(ref ListBox vivod, ref TextBox t4, ref TextBox t3, ref TextBox nomer) {
            string str = nomer.Text;
                vivod.Items.Add(last_stops+' '+ Nomer + ' ' + Type + ' ' +Distance[0] +' '+Distance[Distance.Count-1] + ' '
                    + Time[0].ToString("HH:mm") + ' ' + Time[Time.Count - 1].ToString("HH:mm"));
                vivod.Visible = true;         
        }

        public void last_ostanovka()
        {
            int count=0,i = 0;
            int hour, min;
            Boolean flag = true;
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            DateTime realtime = new DateTime();
            hour = int.Parse(DateTime.Now.ToString("HH"));
            min = int.Parse(DateTime.Now.ToString("mm"));
           realtime = realtime.AddHours(hour);
           realtime= realtime.AddMinutes(min);

            if (realtime <= Time[0])//поезl не выехал
            {
                motion = false;
                last_stops = Distance[0];
                flag = false;
                count=0;
            }
            if (realtime >= Time[Time.Count - 1])//поезд приехал
            {
                last_stops = Distance[Distance.Count - 1];
                flag = false;
                count=Distance.Count-1;
                motion = false;
            }


            //for (int i = 0; i < Time.Count; i++)
            while(flag)
            {
                dt1 = Time[i];
                dt2 = Time[i + 1];
                if((realtime>dt1)&(realtime>=dt2))
                {
                    i++;
                    count++;
                }
                if((realtime>=dt1)&(realtime<dt2))
                {
                    if (realtime == dt1)
                        motion = false;
                    else
                        motion = true;

                    last_stops = Distance[count];
                    flag = false;
                }
               
            }
        }   

    }
}

