using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjM2RBF
{
    class Matrice
    {
        private Ligne[] mM;
        private int mI;
        private int mJ;

        public int MI { get => mI; set => mI = value; }
        public int MJ { get => mJ; set => mJ = value; }

        public Matrice(int i, int j)
        {
            MI = i;
            MJ = j;
            mM = new Ligne[i];

            for(int c=0;c<i;c++)
            {
                Ligne l0 = new Ligne(j);
                mM[c] = l0;
            }
        }

        public void setE(int i, int j, double v)
        {
            mM[i].setE(j,v);

        }

        public double getE(int i, int j)
        {
            return mM[i].getE(j);
        }

        public void setL(int i,Ligne l)
        {
             mM[i]=l;
        }
        public Ligne getL(int i)
        {
            return mM[i];
        }

        public void afficher()
        {
            for (int i = 0; i < MI; i++)
                mM[i].afficher();
        }

        public double[][] getTableau()
        {

       /*     double[][] result = new double[rows][];
            for (int r = 0; r < rows; ++r)
                result[r] = new double[cols];*/
            double[][] tabRetour = new double[MI][];
            for (int r = 0; r < this.MI; ++r)
                tabRetour[r] = new double[MJ];
            for (int i=0;i<this.MI;i++)
            {
                for (int j = 0; j < this.MJ; j++)
                    tabRetour[i][j] = this.getE(i, j);
            }
            return tabRetour;
        }



    }
}
