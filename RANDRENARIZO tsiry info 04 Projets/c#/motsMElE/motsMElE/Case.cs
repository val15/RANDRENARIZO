using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace motsMElE
{
    class Case
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Contenu { get; set; }

        public Case(int x,int y,string c)
        {
            X = x;
            Y = x;
            Contenu = c;
            System.Console.WriteLine(c);
        }
    }
}
