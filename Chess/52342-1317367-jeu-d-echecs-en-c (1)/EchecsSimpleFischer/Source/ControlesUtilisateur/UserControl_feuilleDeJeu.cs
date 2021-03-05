using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace simpleFischer
{
    /// <summary>
    /// UserControl Feuille De Jeu</summary>
    /// <remarks>
    /// La feuille de jeu liste les notations algébriques des Coups joués
    /// </remarks>
    public partial class UserControl_feuilleDeJeu : UserControl
    {

        /// <summary>
        /// Gestionnaire de délégués d'affichage des coups</summary>
        private delegate void DataGridCoupsDelegateHandler();

        /// <summary>
        /// Délégué d'affichage des coups</summary>
        private DataGridCoupsDelegateHandler DataGridCoupsDelegate;

        /// <summary>
        /// Tableau des coups </summary>
        private DataTable tableCoups = new DataTable();

        /// <summary>
        /// Décompte des coups</summary>
        private int numeroCoup = 0;

        /// <summary>
        /// Constructeur de la classe. </summary>
        public UserControl_feuilleDeJeu()
        {
            InitializeComponent();

            DataColumn myDataColumn = new DataColumn();

            tableCoups.Columns.Add();
            tableCoups.Columns.Add();
            tableCoups.Columns.Add();

            this.dataGridView_coups.DataSource = tableCoups;
            this.dataGridView_coups.Columns[0].Width = 20;
            this.dataGridView_coups.Columns[1].Width = 50;
            this.dataGridView_coups.Columns[2].Width = 50;

            DataGridCoupsDelegate = new DataGridCoupsDelegateHandler(updateDataGridCoups);

        }

        /// <summary>
        /// Initialisation de la feuille de jeu.</summary>
        public void initialiser()
        {
            numeroCoup = 0;

            while (tableCoups.Rows.Count > 0)
            {
                tableCoups.Rows.Remove(tableCoups.Rows[0]);
            }

            this.Invoke(this.DataGridCoupsDelegate, null);
        }

        /// <summary>
        /// Mise à jour de l'affichage du tableau de coups.</summary>
        private void updateDataGridCoups()
        {
            
            int i;
            for (i = 0; i < dataGridView_coups.RowCount - 1; i++)
            {
                dataGridView_coups.Rows[i].Selected = false;
            }
            if (dataGridView_coups.RowCount > 0)
            {
                dataGridView_coups.Rows[dataGridView_coups.RowCount - 1].Selected = true;
                dataGridView_coups.FirstDisplayedScrollingRowIndex = dataGridView_coups.Rows[dataGridView_coups.RowCount - 1].Index;
            }
            dataGridView_coups.Refresh();
        }


        /// <summary>
        /// Noter un coup qui vient d'être effecuté par un joueur</summary>
        /// <param name="coup"> Le Coup à noter</param>
        public void noterCoup(Coup coup)
        {
            String notationCoup = coup.notationAlgebrique;

            if (coup.positionResultante.PAT == true)
            {
                notationCoup += "=";
            }
            else if (coup.positionResultante.MAT == true)
            {
                notationCoup += "#";

            }
            else if (coup.positionResultante.échec == true)
            {
                notationCoup += "+";
            }


            if (coup.positionResultante.trait == Partie.noirs)
            {
                numeroCoup++;

                DataRow myDataRow = tableCoups.NewRow();

                myDataRow[0] = numeroCoup;
                myDataRow[1] = notationCoup;
                myDataRow[2] = null;
                tableCoups.Rows.Add(myDataRow);

            }
            else
            {
                DataRow myDataRow = tableCoups.Rows[tableCoups.Rows.Count-1];
                myDataRow[2] = notationCoup;
            }

            this.Invoke(this.DataGridCoupsDelegate, null);
        }



        /// <summary>
        /// Evennement: redimmensionnage de la feuille de jeu</summary>
        private void UserControl_feuilleDeJeu_Resize(object sender, EventArgs e)
        {
            this.dataGridView_coups.Size = this.Size;
        }
    }
}
