using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace motsMElE
{
    public class Grille//une grille est un tableau de cases
    {
        private int m_cotE;
        string[,] m_tabContenu = new string[1, 1];

     
        public int NbLigne { get; set; }
        public int NbColonne { get; set; }

        public Grille(int cotE)
        {
            m_cotE = cotE;
            NbLigne = m_cotE;
            NbColonne = m_cotE;
          
            string[,] tabContenu = new string[m_cotE, m_cotE];
            m_tabContenu = tabContenu;
            
        }
        public int getNbCaseVide()
        {
            int nbCaseVide=0;
             for (int y = 0; y < NbLigne; y++)
            {
                for (int x = 0; x < NbColonne; x++)
                {
                    if (String.Compare(m_tabContenu[y, x], "_") == 0)
                        nbCaseVide++;

                    
                    
                }
            }
             return nbCaseVide;
        }

        public void intitialiser()
        {
            for (int y = 0; y < NbLigne; y++)
            {
                for (int x = 0; x < NbColonne; x++)//on replit  toutes les cases de "_"
                    m_tabContenu[y, x] = "_";
            }
        }
        public void placerDansLaCase(int x, int y, string c)
        {
            m_tabContenu[y, x] = c;
        }

        public string getContenuDuCase(int y,int x)
        {
             return m_tabContenu[y, x];  
        }

        public void mettreMot(Mot mot)
        {
            if (!mot.EstInverser && mot.Orientation == 0)//horizontal normal
            {
                int positionx = mot.CaseDeDeparX;
                for (int c = 0; c < mot.Longeur; c++)
                {
                    
                    m_tabContenu[mot.CaseDeDeparY, positionx] = mot.Contenu.ToCharArray()[c].ToString();
                    positionx++;
                }
                    
            }
            if (mot.EstInverser && mot.Orientation == 0)//horizontal inverse
            {
                int positionx = mot.CaseDeDeparX;
                for (int c = 0; c < mot.Longeur; c++)
                {
                    m_tabContenu[mot.CaseDeDeparY, positionx] = mot.Contenu.ToCharArray()[c].ToString();
                    positionx--;
                }    
            }

            if (!mot.EstInverser && mot.Orientation == 1)//vertical normal
            {
                int positiony = mot.CaseDeDeparY;
                for (int c = 0; c < mot.Longeur; c++)
                {

                    m_tabContenu[positiony, mot.CaseDeDeparX] = mot.Contenu.ToCharArray()[c].ToString();
                    positiony++;
                }

            }
            if (mot.EstInverser && mot.Orientation == 1)//vertical inverse
            {
                int positiony = mot.CaseDeDeparY;
                for (int c = 0; c < mot.Longeur; c++)
                {
                    m_tabContenu[positiony, mot.CaseDeDeparX] = mot.Contenu.ToCharArray()[c].ToString();
                    positiony--;
                }
            }


            if (!mot.EstInverser && mot.Orientation == 2)//diagomale droite, bas y+;x+
            {
                int positiony = mot.CaseDeDeparY;
                int positionx = mot.CaseDeDeparX;
                for (int c = 0; c < mot.Longeur; c++)
                {
                    m_tabContenu[positiony, positionx] = mot.Contenu.ToCharArray()[c].ToString();
                    positiony++;
                    positionx++;
                }
            }

            if (mot.EstInverser && mot.Orientation == 2)//diagomale gauche, haut y-;x-
            {
                int positiony = mot.CaseDeDeparY;
                int positionx = mot.CaseDeDeparX;
                for (int c = 0; c < mot.Longeur; c++)
                {

                    m_tabContenu[positiony, positionx] = mot.Contenu.ToCharArray()[c].ToString();
                    positiony--;
                    positionx--;
                }
            }

            if (!mot.EstInverser && mot.Orientation == 3)//diagomale droite, haut y-;x+
            {
                int positiony = mot.CaseDeDeparY;
                int positionx = mot.CaseDeDeparX;
                for (int c = 0; c < mot.Longeur; c++)
                {

                    m_tabContenu[positiony, positionx] = mot.Contenu.ToCharArray()[c].ToString();
                    positiony--;
                    positionx++;
                }
            }

            if (mot.EstInverser && mot.Orientation == 3)//diagomale gauche, bas y+;x-
            {
                int positiony = mot.CaseDeDeparY;
                int positionx = mot.CaseDeDeparX;
                for (int c = 0; c < mot.Longeur; c++)
                {

                    m_tabContenu[positiony, positionx] = mot.Contenu.ToCharArray()[c].ToString();
                    positiony++;
                    positionx--;
                }
            }
        }

        public void GenererPattern(Mot mot)
        {
            if (!mot.EstInverser && mot.Orientation == 0)//horizental normal
            {
                int positionx = mot.CaseDeDeparX;
                for (int c = 0; c < mot.Longeur; c++)
                {

                    mot.Patterne += m_tabContenu[mot.CaseDeDeparY, positionx];
                    positionx++;
                }

            }
            if (mot.EstInverser && mot.Orientation == 0)//horizental inverse
            {
                int positionx = mot.CaseDeDeparX;
                for (int c = 0; c < mot.Longeur; c++)
                {
                    mot.Patterne += m_tabContenu[mot.CaseDeDeparY, positionx];
                    positionx--;
                }
            }

            if (!mot.EstInverser && mot.Orientation == 1)//vertical normal
            {
                int positiony = mot.CaseDeDeparY;
                for (int c = 0; c < mot.Longeur; c++)
                {
                    mot.Patterne += m_tabContenu[positiony, mot.CaseDeDeparX];
                    positiony++;
                }

            }
            if (mot.EstInverser && mot.Orientation == 1)//vertical inverse
            {
                int positiony = mot.CaseDeDeparY;
                for (int c = 0; c < mot.Longeur; c++)
                {
                    mot.Patterne += m_tabContenu[positiony, mot.CaseDeDeparX];
                    positiony--;
                }
            }


            if (!mot.EstInverser && mot.Orientation == 2)//diagomale droite, bas y+;x+
            {
                int positiony = mot.CaseDeDeparY;
                int positionx = mot.CaseDeDeparX;
                for (int c = 0; c < mot.Longeur; c++)
                {
                    mot.Patterne+=m_tabContenu[positiony, positionx];
                    positiony++;
                    positionx++;
                }
            }

            if (mot.EstInverser && mot.Orientation == 2)//diagomale gauche, haut y-;x-
            {
                int positiony = mot.CaseDeDeparY;
                int positionx = mot.CaseDeDeparX;
                for (int c = 0; c < mot.Longeur; c++)
                {
                    mot.Patterne += m_tabContenu[positiony, positionx];
                    positiony--;
                    positionx--;
                }
            }

            if (!mot.EstInverser && mot.Orientation == 3)//diagomale droite, haut y-;x+
            {
                int positiony = mot.CaseDeDeparY;
                int positionx = mot.CaseDeDeparX;
                for (int c = 0; c < mot.Longeur; c++)
                {
                    mot.Patterne += m_tabContenu[positiony, positionx];
                    positiony--;
                    positionx++;
                }
            }

            if (mot.EstInverser && mot.Orientation == 3)//diagomale gauche, bas y+;x-
            {
                int positiony = mot.CaseDeDeparY;
                int positionx = mot.CaseDeDeparX;
                for (int c = 0; c < mot.Longeur; c++)
                {
                    mot.Patterne += m_tabContenu[positiony, positionx];
                    positiony++;
                    positionx--;
                }
            }
        }

        public string chercherDansLaBaseAPartirDUnPatterneDuMot(Mot mot)
        {
            string motTrouver=" ";
            SQLiteConnection maConnexion;
            maConnexion = new SQLiteConnection("Data Source=words.db;Version=3;");
            maConnexion.Open();
            string pattern = "'"+mot.Patterne+"'";
            string sql = "SELECT word FROM words WHERE word LIKE" + pattern + " AND LENGTH(word)="+mot.Longeur + " ORDER BY RANDOM() LIMIT 1";
            SQLiteCommand commande = new SQLiteCommand(sql, maConnexion);
            SQLiteDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                motTrouver = reader["word"].ToString();
            }

            maConnexion.Close();
            return motTrouver;
        }

        public bool estRemplie()
        {
           bool estRemplie=false;
            int f=1;
            for (int x = 0; x < m_cotE; x++)
           {
               for (int y = 0; y < m_cotE; y++)
               {
                   if(String.Compare(m_tabContenu[y, x], "_")!=0)
                   {
                       f=f*1;
                   }
                   else
                   {
                       f=f*0;
                   }
               }
                   
           }

            if(f==0)
                estRemplie = false;
            else
                estRemplie = true;
            return estRemplie;
       }

        public string genererMotCacher(int longeur)
        {
            //le mot caché ne doit pas se trouver dans la liste des mots trouvés et doit respecter la longueur
            string motTrouver = " ";
            SQLiteConnection maConnexion;
            maConnexion = new SQLiteConnection("Data Source=words.db;Version=3;");
            maConnexion.Open();

            string sql = "SELECT word FROM words WHERE LENGTH(word)=" + longeur + " ORDER BY RANDOM() LIMIT 1";
            SQLiteCommand commande = new SQLiteCommand(sql, maConnexion);
            SQLiteDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                motTrouver = reader["word"].ToString();
            }

            maConnexion.Close();
            return motTrouver;

        }

        public void metreMotCaherDansLesCaserRestante(String mot)
        {
            int c=0;
            while (c < mot.Length)
            {
                for (int y = 0; y < NbLigne; y++)
                {
                    for (int x = 0; x < NbColonne; x++)
                    {
                        if (String.Compare(m_tabContenu[y, x], "_") == 0)
                        {
                            m_tabContenu[y, x] = mot.ToCharArray()[c].ToString();
                            c++;
                        }

                    }
                }   
            }
        }
    }
}
