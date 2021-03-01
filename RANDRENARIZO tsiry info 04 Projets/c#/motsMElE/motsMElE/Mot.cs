using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace motsMElE
{
   public  class Mot
    {
        public int Longeur { get; set; }
        public bool EstInverser { get; set; }
        public string Contenu { get; set; }
        public int CaseDeDeparX { get; set; }
         public int CaseDeDeparY { get; set; }
         public int CaseDeDArivE { get; set; }
         private int TailleMax { get; set; }

        public int Orientation { get; set; }//horizentation => H=0; V=1; DB=2,GB=3

        //patterne
        public string Patterne { get; set; }

        public Mot(int longeur, bool estInverser, int orientation, int caseDeDeparY, int caseDeDeparX,int tailleMax)
        {
            Longeur = longeur;
            EstInverser = estInverser;
            Orientation=orientation;
            CaseDeDeparX=caseDeDeparX;
            CaseDeDeparY = caseDeDeparY;
            TailleMax = tailleMax;

            //construction de patterne avant toute chose
            //on verifie que le mot ne dépasse pas
            if (orientation == 0)
            {
                if (!estInverser)
                {
                    if (caseDeDeparX + Longeur > TailleMax)
                    {
                        CaseDeDeparX = TailleMax - Longeur;
                    }
                }
                else
                {
                    if (caseDeDeparX-Longeur < 0)
                    {
                        CaseDeDeparX = 0 + Longeur-1;

                    }
                }
            }
            if (orientation == 1)
            {
                if (!estInverser)
                {
                    if (caseDeDeparY + Longeur > TailleMax)
                    {
                        CaseDeDeparY = TailleMax - Longeur;

                    }
                }
                else
                {
                    if (caseDeDeparY - Longeur < 0)
                    {
                        CaseDeDeparY = 0 + Longeur - 1;

                    }
                }
            }

            if (orientation == 2)//diagonal droite et bas 
            {
                if (!estInverser)
                {
                    if (caseDeDeparX + Longeur > TailleMax)
                    {
                        CaseDeDeparX = TailleMax - Longeur;
                    }

                    if (caseDeDeparY + Longeur > TailleMax)
                    {
                        CaseDeDeparY = TailleMax - Longeur;

                    }

                }
                else//diagonal gauche et haut
                {
                    if (caseDeDeparX - Longeur < 0)
                    {
                        CaseDeDeparX = 0 + Longeur - 1;

                    }
                    if (caseDeDeparY - Longeur < 0)
                    {
                        CaseDeDeparY = 0 + Longeur - 1;

                    }
                }
            }


            if (orientation == 3)//diagonale droite et haut 
            {
                if (!estInverser)
                {
                    if (caseDeDeparX + Longeur > TailleMax)
                    {
                        CaseDeDeparX = TailleMax - Longeur;

                    }

                    if (caseDeDeparY - Longeur < 0)
                    {
                        CaseDeDeparY = 0 + Longeur - 1;

                    }

                }
                else//diagonale gauche et bas
                {
                    if (caseDeDeparX - Longeur < 0)
                    {
                        CaseDeDeparX = 0 + Longeur - 1;

                    }
                    if (caseDeDeparY + Longeur > TailleMax)
                    {
                        CaseDeDeparY = TailleMax - Longeur;

                    }
                }
            }    
        }
    }
}
