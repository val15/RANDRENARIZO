using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace motsMElE
{
    public  partial class FormImprimer : Form
    {
        private Form1  m_frmMere;
        private List<string> m_lstStringAChercE;
        public FormImprimer(Form1  frmMere, List<string> lstStringAChercE)
        {
            m_frmMere=frmMere;
            m_lstStringAChercE=lstStringAChercE;
            InitializeComponent();
        }

        private void FormImprimer_Load(object sender, EventArgs e)
        {
            m_tbImprimer.AppendText("grille : ");
            m_tbImprimer.AppendText("\r\n");
            for (int y = 0; y < m_frmMere.Gr0.NbLigne; y++)
            {

                for (int x = 0; x < m_frmMere.Gr0.NbColonne; x++)
                {
                    m_tbImprimer.AppendText(m_frmMere.Gr0.getContenuDuCase(y, x) + "\t");
                }
                m_tbImprimer.AppendText("\r\n");
            }
            m_tbImprimer.AppendText("\r\n");
            m_tbImprimer.AppendText("mot caché: "+ m_frmMere.MotCachE);
            m_tbImprimer.AppendText("\r\n");
            m_tbImprimer.AppendText("\r\n");
            m_tbImprimer.AppendText("mots à chercher: ");
            m_tbImprimer.AppendText("\r\n");
            for (int c = 0; c < m_lstStringAChercE.Count; c++)
            {
                m_tbImprimer.AppendText(m_lstStringAChercE[c]);
                m_tbImprimer.AppendText("\r\n");
            }

        }

        private void m_btImprimer_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "fichier text|*.txt";
            saveFileDialog1.Title = "enregistrer fichier sous";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                m_tbNomFichier.Text = saveFileDialog1.FileName;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void m_btImprimer_Click_1(object sender, EventArgs e)
        {
            if (m_tbNomFichier.Text != "")
            {
                Fichier f0 = new Fichier(m_tbNomFichier.Text);
                f0.EcrireAuDebut(m_tbImprimer.Text);
                this.Close();
            }
            else
                MessageBox.Show("le nom dufichier vide!!", "erreur");
        }
    }
}
