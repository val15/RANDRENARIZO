using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;


namespace motsMElE
{
    public delegate void Clk(object sender, EventArgs e);
    public partial class Form1 : Form
    {
       
        public Grille Gr0 { get; private set; }
        public string MotCachE { get; set; }
        Grille m_grCorrection;
        Grille m_grMotTrouvE;
        private int m_nbCotE=0;
        private int m_motCorrigEActuel = 0;
        public List<string> LstStringAChercE { get; set; }
        List<Mot> m_lstMot = new List<Mot>();//liste qui contient touts les Mot (des objet Mot))

        public string StrTrouvE { get; set; }
        public int IndiceActuele { get; set; }
        public List<List<BoutonLettre>> ListBtl { get; set; }
        public Form1()
        {
            StrTrouvE = "";
            IndiceActuele = 0;
            InitializeComponent();
            m_nUDCotee.Maximum = 20;
            m_nUDCotee.Minimum = 5;
            ListBtl = new List<List<BoutonLettre>>();
            LstStringAChercE = new List<string>();
     

    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
      e.Graphics.DrawLine(pen, 20, 10, 300, 100);
    }

    private void Form1_Load(object sender, EventArgs e)
        {
            m_btRemplir.Enabled = false;
            m_cbCorrection.Enabled = false;
      
    }

        

        
        


         private void m_generer_Click_1(object sender, EventArgs e)
        {
            // Initialisation
            if (Int32.Parse(m_nUDCotee.Value.ToString()) >= 5)
            {
                m_nbCotE = Int32.Parse(m_nUDCotee.Value.ToString());
                Gr0 = new Grille(m_nbCotE);
                Gr0.intitialiser();

                m_grCorrection = new Grille(m_nbCotE);
                m_grMotTrouvE = new Grille(m_nbCotE);

                m_btRemplir.Enabled = true;

                m_nUDCotee.Enabled = false;

                //initialisation du tableau des boutons
                for (int y = 0; y < m_nbCotE; y++)
                {
                    List<BoutonLettre> lstbtl = new List<BoutonLettre>();
                    for (int x = 0; x < m_nbCotE; x++)
                    {
                        BoutonLettre bt = new BoutonLettre("_",0,0,this);
                        lstbtl.Add(bt);
                    }
                    ListBtl.Add(lstbtl);
                }  
            }
            else
                MessageBox.Show("nb de côté invalide", "erreur");
            m_btGenerer.Enabled = false;
        }

        private void afficherGrille()
        {

            int Y = 0;


            for (int y = 0; y < Gr0.NbLigne; y++)
            {
                int X = 0;
                for (int x = 0; x < Gr0.NbColonne; x++)
                {
                    System.Drawing.Graphics formGraphics = this.CreateGraphics();

                    System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 10);
                    System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);


                    System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                    
                    if (String.Compare(Gr0.getContenuDuCase(y, x), "_") != 0)
                         formGraphics.DrawString(Gr0.getContenuDuCase(y, x), drawFont, drawBrush, X, Y, drawFormat);
                    drawFont.Dispose();
                    drawBrush.Dispose();
                    formGraphics.Dispose();

                    X += 25;
                }
                Y += 25;
            }

           
        }

        public void afficherGrilleDesBoutons()
        {
            int Y = 0;


            for (int y = 0; y < Gr0.NbLigne; y++)
            {
                int X = 0;
                List<BoutonLettre> lstbtl = new List<BoutonLettre>();
                for (int x = 0; x < Gr0.NbColonne; x++)
                {
                        BoutonLettre btl = new BoutonLettre(Gr0.getContenuDuCase(y, x), y, x, this);
                          btl.Location = new Point(X, Y);
                         lstbtl.Add(btl);
                    X += 25;
                }
                Y += 25;
                ListBtl[y] = lstbtl;
            }

            for (int y = 0; y < Gr0.NbLigne; y++)
            {
                for (int x = 0; x < Gr0.NbColonne; x++)
                {
                    this.Controls.Add(ListBtl[y][x]);
                }
            }

        }



        private void m_btRemplir_Click(object sender, EventArgs e)
        {
            m_btRemplir.Enabled = false;
            while (!Gr0.estRemplie() && Gr0.getNbCaseVide() >=m_nbCotE)
            {
                
                Random randNum = new Random();
                int longeur = randNum.Next(3, m_nbCotE);

                
                int orientation = randNum.Next(0, 4);//de 1-3
                int caseDeDeparY = randNum.Next(0, m_nbCotE);
                int caseDeDeparX = randNum.Next(0, m_nbCotE);
                int inv = randNum.Next(0, 11);
                bool estInversE;
                estInversE = true;
                if (inv%2 ==0 )
                    estInversE = true;
                else
                    estInversE = false;

                Mot m0 = new Mot(longeur, estInversE, orientation, caseDeDeparY, caseDeDeparX,m_nbCotE);//Mot(int longueur, bool estInverser, int orientation,int caseDeDeparY,int caseDeDeparX, string contenu)
                Gr0.GenererPattern(m0);
                string mottrouvE = Gr0.chercherDansLaBaseAPartirDUnPatterneDuMot(m0);
                if (String.Compare(mottrouvE," ") != 0 )//si on trouve un mot et que ce mot n'est pas dans la liste
                {
                    if (!estDejaDansLaListeDesMotsATrouver(mottrouvE))
                    {
                        m0.Contenu = mottrouvE;
                        Gr0.mettreMot(m0);
                        afficherGrille();
                        LstStringAChercE.Add(m0.Contenu);
                        m_lbNbMotAChercher.Text = LstStringAChercE.Count.ToString();
                        m_lstMot.Add(m0);
                        
                        
                        
                    }
                }   
            }

            //actualisation du tableau des mots à chercher
            actualiserTbMotsACherchE();
            
            genererMotCacher();
            

            List <String> lstTmp=new List <String>();
            lstTmp.Add(" ");
            for(int c=0;c<LstStringAChercE.Count;c++)
            {
                lstTmp.Add(LstStringAChercE[c]);
            }

            m_cbCorrection.DataSource = lstTmp;
            afficherGrilleDesBoutons();

            m_grMotTrouvE.intitialiser();
            afficherGrilleMotsTrouvE();

            m_cbCorrection.Enabled = true;
            m_btImprimer.Enabled = true;

      this.Refresh();
     
    }

        public void actualiserTbMotsACherchE()
        {
            m_lbNbMotAChercher.Text = LstStringAChercE.Count.ToString();
            m_tblisteMotAChercer.Clear();
            for (int c = 0; c < LstStringAChercE.Count; c++)
            {
                m_tblisteMotAChercer.AppendText(LstStringAChercE[c]);
                m_tblisteMotAChercer.AppendText("\r\n");
            }
        }


        

        private bool estDejaDansLaListeDesMotsATrouver(string motTrouvE)
        {
            bool trouvE = false;
            int c = 0;
            if (LstStringAChercE.Count > 0)
            {
                while (c < LstStringAChercE.Count && !trouvE)
                {
                    if (String.Compare(LstStringAChercE[c], motTrouvE) == 0)
                        trouvE = true;
                    c++;
                }
            }
            return trouvE;
        }

        private void afficherGrilleCorrection()
        {

            m_tbCorrection.Clear();
            for (int y = 0; y < m_grCorrection.NbLigne; y++)
            {
                
                for (int x = 0; x < m_grCorrection.NbColonne; x++)
                {
                    m_tbCorrection.AppendText(" "+ m_grCorrection.getContenuDuCase(y, x)+" ");
                }
                m_tbCorrection.AppendText("\r\n");
            }
        }

        private void afficherGrilleMotsTrouvE()
        {
            m_tbGrilleMotTrouvE.Clear();
            for (int y = 0; y < m_grMotTrouvE.NbLigne; y++)
            {

                for (int x = 0; x < m_grMotTrouvE.NbColonne; x++)
                {
                    m_tbGrilleMotTrouvE.AppendText(" " + m_grMotTrouvE.getContenuDuCase(y, x) + " ");
                }
                m_tbGrilleMotTrouvE.AppendText("\r\n");
            }
        }

        

        void genererMotCacher()
        {
            string mottrouvE = " ";
            bool trouvE = false;
            
            while(String.Compare(mottrouvE, " ") == 0 || !trouvE)//si on trouve un mot et que ce mot n'est pas dans la liste
            {
                if (!estDejaDansLaListeDesMotsATrouver(mottrouvE))
                {
                    mottrouvE = m_grCorrection.genererMotCacher(Gr0.getNbCaseVide());
                    trouvE = true;
                }
            }

            
            Gr0.metreMotCaherDansLesCaserRestante(mottrouvE);
            m_tbMotCaché.Text = mottrouvE;
            MotCachE = mottrouvE;
            afficherGrille();

        }


        public void ajouterGrilleMotTrover(String str)
        {
            
            for (int c = 0; c < m_lstMot.Count; c++)
                if (String.Compare(str, m_lstMot[c].Contenu) == 0)
                {
                    m_grMotTrouvE.mettreMot(m_lstMot[c]);
                    afficherGrilleCorrection();
                }
            afficherGrilleMotsTrouvE();
        }
        
          

           private void m_cbNotATrouver_SelectedIndexChanged(object sender, EventArgs e)
           {
               m_grCorrection.intitialiser();
               afficherGrilleCorrection();
               for(int c=0;c<m_lstMot.Count;c++)
                   if(String.Compare(m_cbCorrection.SelectedItem.ToString(),m_lstMot[c].Contenu)==0)
                   {
                       m_grCorrection.mettreMot(m_lstMot[c]);
                       afficherGrilleCorrection();
                   }        
           }

          
           private void button1_Click(object sender, EventArgs e)
           {
               FormImprimer frmIprimer = new FormImprimer(this, LstStringAChercE);
               frmIprimer.Show();
           }

           private void label4_Click(object sender, EventArgs e)
           {

           }        
    }
}
