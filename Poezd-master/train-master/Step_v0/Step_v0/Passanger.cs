using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Step_v0
{
    class Passanger
    {
        public string Surname, Name, Secondname;
        public static List<string> PoiskVsexPasagirov()
        {
            StreamReader fs2 = new StreamReader("basePassengers.txt");
            List<string> collection = new List<string>();
            while (true)
            {
                string s = fs2.ReadLine();
                if (s != null)
                {
                    collection.Add(s);
                }
                else
                    break;
            }
            fs2.Close();
            return collection;
        }

        public static List<string> PoiskPasagirov(string Str)
        {
            StreamReader fs2 = new StreamReader("basePassengers.txt");
            List<string> collection = new List<string>();
            int k = 0;
            while (true)
            {
                string s = fs2.ReadLine();
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
            fs2.Close();
            return collection;
        }
    }
  }