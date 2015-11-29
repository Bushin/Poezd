using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Step_v0
{
    class Passanger
    {
        public string Surname, Name, Nomer_poezda, Nomer_Vagona, Mesto_Nomer, Mesto_Bukva;

        public Passanger(string n, string s, string nomer_poezda, string nomer_vagona, string mesto_nomer, string mesto_bukva)
        {
            Name = n;
            Surname = s;
            Nomer_poezda = nomer_poezda;
            Nomer_Vagona = nomer_vagona;
            Mesto_Nomer = mesto_nomer;
            Mesto_Bukva = mesto_bukva;
        }
        public void Vivod(ref ListBox vivod)
        {
            vivod.Items.Add(Name + ' ' + Surname + ' ' + Nomer_poezda + ' ' + Nomer_Vagona + ' '
                + Mesto_Nomer + ' ' + Mesto_Bukva);
            vivod.Visible = true;
        }
    }
  }