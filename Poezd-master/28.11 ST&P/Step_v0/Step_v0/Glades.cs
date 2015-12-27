using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Step_v0
{
    class Glades
    {
      /*  string mesto_b; string curitem_inteface;
        int mesto_n; int count = 0; public static int K = 0;
        Train t; Passanger pas; Bilet bil; Distanation dist;
        public static List<Train> trains = new List<Train>();
        public static List<Distanation> marshruti = new List<Distanation>();
        public static List<Passanger> passangers = new List<Passanger>();
        public static List<Bilet> bilets = new List<Bilet>();
        public List<Train> current_trains = new List<Train>();
        //    PictureBox pictureBoxs = new PictureBox();
        //    public static ComboBox c_b = new ComboBox();

        public void PoiskVsexPoezdov()
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load("Poezd.xml");
            XmlElement Root = Doc.DocumentElement;
            string nomer = "", type = "", id = "";
            int Hour, Min;
            foreach (XmlNode node in Root)
            {
                List<string> distance = new List<string>();
                List<DateTime> time = new List<DateTime>();
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
                t = new Train(nomer, type, id, time);
                trains.Add(t);
                distance = null;
                time = null;
            }
        }

        public static Stops Poiskostanovoki(string name_ostanovki)
        {
            Stops ost;
            XmlDocument Doc = new XmlDocument();
            Doc.Load("Ostanovki.xml");
            XmlElement Root = Doc.DocumentElement;
            string name = ""; int cor_x = 0, cor_y = 0;
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
        }*/



    }
}
