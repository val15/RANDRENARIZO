using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjM2RBF
{
    class Ligne
    {


        //private List<double> l =new List<double>();
        private double[] l;
        private int taille;

        public int Taille { get => taille; set => taille = value; }

        public Ligne(int t)
        {
            l = new double[t];
            this.Taille = t;
        }
        public double getE(int indice)
        {
            return l[indice];
        }
        public void setE(int indice,double v)
        {
            l[indice] = v;
        }
        public void afficher()
        {
            for (int c = 0; c < Taille; c++)
                Console.Write(l[c] + "\t");
            Console.WriteLine();
        }

        public void setApartirDUnTableau(double[] tab)
        {
            this.l = tab;
        }

        public double[] getTableau()
        {
            return this.l;
        }




    }
}
