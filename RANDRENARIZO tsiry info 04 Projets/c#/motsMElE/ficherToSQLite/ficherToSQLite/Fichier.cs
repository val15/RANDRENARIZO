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
    public List<string> ReadAllLinges()//commence par 0
    {
      var tab = System.IO.File.ReadAllLines(NomFichier, Encoding.UTF8);
      return tab.ToList();


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
