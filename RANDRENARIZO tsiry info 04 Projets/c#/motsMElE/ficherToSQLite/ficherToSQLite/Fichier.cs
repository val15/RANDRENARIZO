using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ficherToSQLite
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
        public string lireUneLinge(int numlinge)//commence par 0
        {
            if (numlinge < NbLinge)
            {
                string[] lines = System.IO.File.ReadAllLines(NomFichier);
                return lines[numlinge];
            }
            else
                return "NUMERO DE LIGNE INCORRECT!!";

        }
        public string lireTout()
        {
            return System.IO.File.ReadAllText(NomFichier);

        }
        public void ecrireAlaFin(string texte)//ne supprime pas le fichier mais ajoute à la fin
        {
            if (File.Exists(NomFichier))
            {
                string contenu = lireTout();
                contenu = contenu + "\r\n" + texte;
                System.IO.File.WriteAllText(NomFichier, contenu);
            }
            else
                ecrireAuDebut(texte);

        }
        public void ecrireAuDebut(string texte)//supprime le contunu du fichier et le remplace
        {
            System.IO.File.WriteAllText(NomFichier, texte);
        }
    }
}
