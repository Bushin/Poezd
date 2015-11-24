using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Step_v0
{
    class Train
    {
        public string Nomer, Type;
        public int Speed, Distance;

        public static List<string> PoiskPoezda(string Str)
        {
            List<string> collection = new List<string>();
            StreamReader fs1 = new StreamReader("baseTrain.txt");
            int k = 0;
            while (true)
            {
                string s = fs1.ReadLine();
                if (s != null)
                {
                    if (s.IndexOf(Str) > -1)
                    {
                        collection.Add(s);
                        k++;
                    }
                }
                else
                    break;
            }
            if (k == 0)
            {
                collection.Add("null");
            }
            fs1.Close();
            return collection;
        }

        public static List<string> PoiskVsexPoezdov()
        {
            List<string> collection = new List<string>();
            StreamReader fs1 = new StreamReader("baseTrain.txt");
            while (true)
            {
                string s = fs1.ReadLine();
                if (s != null)
                {
                    collection.Add(s);
                }
                else
                    break;
            }
            fs1.Close();
            return collection;
        }
    }
}
