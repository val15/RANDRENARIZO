using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Windows.Forms;

namespace motsMElE
{
    class Fichier
    {
        public string NomFichier { get; set; }
        public int NbLinge { get; private set; }
        public Fichier(string nomFichier)
        {
            NomFichier = nomFichier;
            
            if (File.Exists(NomFichier))
            {
                
                string[] lines = System.IO.File.ReadAllLines(NomFichier);
                NbLinge = lines.Count();
            }
        }
        public string LireUneLinge(int numlinge)//commence par 0
        {
            if (numlinge < NbLinge)
            {
                string[] lines = System.IO.File.ReadAllLines(NomFichier);
                return lines[numlinge];
            }
            else
                return "NUMERO DE LIGNE INCORRECT!!";

        }
        public string LireTout()
        {
            return System.IO.File.ReadAllText(NomFichier);

        }
        public void EcrireAlaFin(string texte)//ne supprime pas le fichier mais ajoute à la fin
        {
            if (File.Exists(NomFichier))
            {
                string contenu = LireTout();
                contenu = contenu + "\r\n" + texte;
                System.IO.File.WriteAllText(NomFichier, contenu);
            }
            else
                EcrireAuDebut(texte);

        }
        public void EcrireAuDebut(string texte)//supprime le contunu du fichier et le remplace
        {
            System.IO.File.WriteAllText(NomFichier, texte);
        }
    }
}
