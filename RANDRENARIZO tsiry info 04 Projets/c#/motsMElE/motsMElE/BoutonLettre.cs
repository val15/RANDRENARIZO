using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace motsMElE
{
    public partial class BoutonLettre : UserControl // un cotrole utilisateur spécialement créer pour pour geréer le jeux
    {
        public int IndiceY { get; set; }
        public int IndiceX { get; set; }
        public string Lettre { get; set; }
        public bool ActivE { get; set; }
        private Form1 m_frmMere;
        public BoutonLettre(string lettre, int indiceY, int indiceX, Form1 frmMere)
        {
            m_frmMere = frmMere;
            InitializeComponent();
            ActivE = true;
            Lettre = lettre;
            IndiceY = indiceY;
            IndiceX = indiceX;
            m_bt.Text = lettre;
        }

        private void m_bt_Click(object sender, EventArgs e)
        {
            if (ActivE)
            {
                Button button = (Button)sender;
                button.Enabled = false;

               desactivationDeToutLesAutresBoutons();//on desactive tout les click sauf pour le bouton actuel
               activerLesBoutonsAuTour();//on active le click sur les bouton au tour du bouton

               m_frmMere.StrTrouvE += Lettre;    //on ajoute la leltre cliquée dans le string mot a chercer
               m_frmMere.IndiceActuele++;//on incremente l'indice


               if (verifierSiStrActuelPeutEtreEncoreDansLaListe())//si la  lettre est tjr dans la liste, on ne fait rien
               {

               }
               else
                   resetTout();//si non on rese tout

               if (trouvEUn())//on verifie si le srt actuele est dans la lsite des mot à chercher
               {
                   m_frmMere.LstStringAChercE.Remove(m_frmMere.StrTrouvE);////si oui on l'enleve de là,
                   m_frmMere.actualiserTbMotsACherchE();//on actualise la liste des mot à chercehr, et on reset tout, 
                   m_frmMere.ajouterGrilleMotTrover(m_frmMere.StrTrouvE);//on l' ajoute dans la grille des nots trouvés



                   if (m_frmMere.LstStringAChercE.Count == 0)//si la liste des mots à chercer est vide
                   {
                       MessageBox.Show("vous avez trouver tout les mots ","felécitation");
                   }

                   resetTout();
               }
            }
        }

   

    private bool verifierSiStrActuelPeutEtreEncoreDansLaListe()
        {
            bool encoreLa=false;
            int c = 0;
            while (!encoreLa && c < m_frmMere.LstStringAChercE.Count)
            {
               
                if(m_frmMere.LstStringAChercE[c].IndexOf(m_frmMere.StrTrouvE) >=0)
                {
                    encoreLa = true;
                }
                else
                    c++;
            } 
            return encoreLa;
        }

        private bool trouvEUn()
        {
            bool trouvE = false;
            int c=0;
            if (m_frmMere.IndiceActuele > 1)
            {
                while (!trouvE && c < m_frmMere.LstStringAChercE.Count)
                {

                    if (String.Compare(m_frmMere.LstStringAChercE[c], m_frmMere.StrTrouvE) == 0)
                    {
                        trouvE = true;
                    }
                    else
                        c++;
                }
            }
            return trouvE;
        }

        private void resetTout()//initialisation de tout les boutons
        {
            m_frmMere.StrTrouvE = "";
            m_frmMere.IndiceActuele = 0;
            for (int y = 0; y < m_frmMere.ListBtl.Count; y++)
            {
                for (int x = 0; x < m_frmMere.ListBtl[y].Count; x++)
                {
                    m_frmMere.ListBtl[y][x].ActivE = true;
                    m_frmMere.ListBtl[y][x].setEnabledBt(true);

                }
            }
        }

        private void setEnabledBt(bool en)
        {
            m_bt.Enabled = en;
        }

        public void desactivationDeToutLesAutresBoutons()
        {
            for (int y = 0; y < m_frmMere.ListBtl.Count; y++)
            {
                for (int x = 0; x < m_frmMere.ListBtl[y].Count; x++)
                {
                    m_frmMere.ListBtl[y][x].ActivE = false;
                }
            }
        }

        public void activerLesBoutonsAuTour()
        {
            if (IndiceY == 0 && IndiceX == 0)
            {
                
                m_frmMere.ListBtl[IndiceY][IndiceX + 1].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY][IndiceX + 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY + 1][IndiceX].ActivE = true;
              //  m_frmMere.ListBtl[IndiceY + 1][IndiceX].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY + 1][IndiceX + 1].ActivE = true;
               // m_frmMere.ListBtl[IndiceY + 1][IndiceX + 1].modifierLetttre("-");
            }
            else if(IndiceY == m_frmMere.ListBtl.Count-1 && IndiceX ==0 )
            {

                m_frmMere.ListBtl[IndiceY - 1][IndiceX].ActivE = true;
              //  m_frmMere.ListBtl[IndiceY - 1][IndiceX].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY - 1][IndiceX + 1].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY - 1][IndiceX + 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY][IndiceX + 1].ActivE = true;
            //    m_frmMere.ListBtl[IndiceY][IndiceX + 1].modifierLetttre("-");
               
            }
            else if (IndiceY == 0 && IndiceX == m_frmMere.ListBtl[IndiceX].Count - 1)
            {
                
                m_frmMere.ListBtl[IndiceY][IndiceX - 1].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY][IndiceX - 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY + 1][IndiceX - 1].ActivE = true;
               // m_frmMere.ListBtl[IndiceY + 1][IndiceX - 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY + 1][IndiceX].ActivE = true;
               // m_frmMere.ListBtl[IndiceY + 1][IndiceX].modifierLetttre("-");
            }
            else if (IndiceY == m_frmMere.ListBtl.Count-1 && IndiceX == m_frmMere.ListBtl[IndiceX].Count - 1)
            {
                m_frmMere.ListBtl[IndiceY - 1][IndiceX - 1].ActivE = true;
            //    m_frmMere.ListBtl[IndiceY - 1][IndiceX - 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY - 1][IndiceX].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY - 1][IndiceX].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY][IndiceX - 1].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY][IndiceX - 1].modifierLetttre("-");
            }

            else if (IndiceY == 0)
            {

                m_frmMere.ListBtl[IndiceY][IndiceX - 1].ActivE = true;
              //  m_frmMere.ListBtl[IndiceY][IndiceX - 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY][IndiceX + 1].ActivE = true;
              //  m_frmMere.ListBtl[IndiceY][IndiceX + 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY + 1][IndiceX - 1].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY + 1][IndiceX - 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY + 1][IndiceX].ActivE = true;
              //  m_frmMere.ListBtl[IndiceY + 1][IndiceX].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY + 1][IndiceX + 1].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY + 1][IndiceX + 1].modifierLetttre("-");
            }
            else if (IndiceX == 0)
            {
                m_frmMere.ListBtl[IndiceY - 1][IndiceX].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY - 1][IndiceX].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY - 1][IndiceX + 1].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY - 1][IndiceX + 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY][IndiceX + 1].ActivE = true;
              //  m_frmMere.ListBtl[IndiceY][IndiceX + 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY + 1][IndiceX].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY + 1][IndiceX].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY + 1][IndiceX + 1].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY + 1][IndiceX + 1].modifierLetttre("-");
            }
            else if (IndiceY == m_frmMere.ListBtl.Count - 1)
            {
                m_frmMere.ListBtl[IndiceY - 1][IndiceX - 1].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY - 1][IndiceX - 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY - 1][IndiceX].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY - 1][IndiceX].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY - 1][IndiceX + 1].ActivE = true;
           //     m_frmMere.ListBtl[IndiceY - 1][IndiceX + 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY][IndiceX - 1].ActivE = true;
            //    m_frmMere.ListBtl[IndiceY][IndiceX - 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY][IndiceX + 1].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY][IndiceX + 1].modifierLetttre("-");
            }
            else if (IndiceX == m_frmMere.ListBtl[IndiceY].Count - 1)
            {
                m_frmMere.ListBtl[IndiceY - 1][IndiceX - 1].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY - 1][IndiceX - 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY - 1][IndiceX].ActivE = true;
            //    m_frmMere.ListBtl[IndiceY - 1][IndiceX].modifierLetttre("-");

                m_frmMere.ListBtl[IndiceY][IndiceX - 1].ActivE = true;
              //  m_frmMere.ListBtl[IndiceY][IndiceX - 1].modifierLetttre("-");

                m_frmMere.ListBtl[IndiceY + 1][IndiceX - 1].ActivE = true;
            //    m_frmMere.ListBtl[IndiceY + 1][IndiceX - 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY + 1][IndiceX].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY + 1][IndiceX].modifierLetttre("-");
            }



            //else if(IndiceX == 0 && IndiceY == 0)


            else
            {
                m_frmMere.ListBtl[IndiceY - 1][IndiceX - 1].ActivE = true;
            //    m_frmMere.ListBtl[IndiceY - 1][IndiceX - 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY - 1][IndiceX].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY - 1][IndiceX].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY - 1][IndiceX + 1].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY - 1][IndiceX + 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY][IndiceX - 1].ActivE = true;
           //     m_frmMere.ListBtl[IndiceY][IndiceX - 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY][IndiceX + 1].ActivE = true;
            //    m_frmMere.ListBtl[IndiceY][IndiceX + 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY + 1][IndiceX - 1].ActivE = true;
            //    m_frmMere.ListBtl[IndiceY + 1][IndiceX - 1].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY + 1][IndiceX].ActivE = true;
            //    m_frmMere.ListBtl[IndiceY + 1][IndiceX].modifierLetttre("-");
                m_frmMere.ListBtl[IndiceY + 1][IndiceX + 1].ActivE = true;
             //   m_frmMere.ListBtl[IndiceY + 1][IndiceX + 1].modifierLetttre("-");
            }



           // m_frmMere.afficherGrillerDesBoutons();
        }

        private void modifierLetttre(String lettre)
        {
            Lettre = lettre;
            m_bt.Text = Lettre;
        }
    }
}
