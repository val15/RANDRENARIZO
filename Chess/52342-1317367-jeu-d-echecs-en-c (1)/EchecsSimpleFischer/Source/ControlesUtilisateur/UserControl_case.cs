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
    /// UserControl Case </summary>
    /// <remarks>
    /// Affichage graphique d'une case et des pièces eventuelles qui sont dessus
    /// </remarks>
    public partial class UserControl_case : UserControl
    {
        /// <summary>
        /// Pointeur vers l'échiquier graphique </summary>
        private UserControl_echiquier echiquier;

        /// <summary>
        /// Numéro de colonne </summary>
        private int positionColonne;

        /// <summary>
        /// Numéro de ligne </summary>
        private int positionLigne;

        /// <summary>
        /// Couleur de la case </summary>
        private char couleur_case='c';

        /// <summary>
        /// Pointeur vers le joueur propriétaire d'une pièce</summary>
        private Joueur proprietaire_piece;

        /// <summary>
        /// Code de la pièce posée sur la case</summary>
        private char lettre_piece=' ';

        /// <summary>
        /// Code couleur de la bordure</summary>
        private char couleur_bordure = ' ';

        /// <summary>
        /// Surbrillance de la bordure au passage de la souris </summary>
        private Boolean surbrillance_bordure = false;

        /// <summary>
        /// Activation du clic souris pour jouer manuellement des coups</summary>
        private Boolean clicEstActif=false;

        /// <summary>
        /// Constructeur de la classe. </summary>
        /// <param name="s"> Emplacement de la description du paramètre de s</param>
        /// <param name="s"> Emplacement de la description du paramètre de s</param>
        /// <param name="s"> Emplacement de la description du paramètre de s</param>
        public UserControl_case(UserControl_echiquier conteneur, int colonne, int ligne)
        {
            InitializeComponent();
            this.positionColonne = colonne;
            this.positionLigne = ligne;
            echiquier = conteneur;
        }

        /// <summary>
        /// Activation/désactivation du clic souris </summary>
        public Boolean clicActif
        {
            get
            {
                return clicEstActif;
            }
            set
            {
                clicEstActif = value;
            }
        }

        /// <summary>
        /// Numéro de la colonne (lecture seule) </summary>
        public int posColonne
        {
            get
            {
                return positionColonne;
            }
        }

        /// <summary>
        /// Numéro de la ligne (lecture seule) </summary>
        public int posLigne
        {
            get
            {
                return positionLigne;
            }
        }

        /// <summary>
        /// Lecture/Mise à jour de la bordure </summary>
        public char bordure
        {
            get
            {
                return couleur_bordure;
            }
            set
            {
                couleur_bordure = value;
                dessiner_bordure();
            }
        }

        /// <summary>
        /// Lecture/Mise à jour de la couleur de la case</summary>
        public char Couleur_case
        {
            get
            {
                return couleur_case;
            }
            set
            {

                couleur_case = value;
                dessiner();
            }
        }

        /// <summary>
        /// Mise à jour de l'image de bordure de case</summary>
        private void dessiner_bordure()
        {
            int index_image = 25;

            switch (couleur_bordure)
            {                
                case ' ':
                    index_image = 25;
                break;
                case 'd':
                    index_image = 27;
                break;
                case 'a':
                index_image = 29;
                break;

            }

            if (surbrillance_bordure == true)
            {
                index_image = index_image + 1;
            }

            if (index_image == 25)
            {
                this.pictureBox_case.Image = null;
            }
            else
            {
                this.pictureBox_case.Image = imageList_case.Images[index_image];
            }
        }

        /// <summary>
        /// Dessiner la case et les pièces qui sont dessus</summary>
        private void dessiner()
        {
            int index_image = 0;

            switch (lettre_piece)
            {
                case 'P': // pion
                    index_image = 14;
                break;
                case 'R': // roi
                    index_image = 18;
                break;
                case 'D': // dame
                    index_image = 6;
                break;
                case 'T': // tour
                    index_image = 22;
                break;
                case 'F': // fou
                    index_image = 10;
                break;
                case 'C': // cavalier
                    index_image = 2;
                break;
            }

            if (index_image>0 && proprietaire_piece == Partie.noirs)
            {
                index_image = index_image + 2;
            }
            if (couleur_case == 'f')
            {
                index_image = index_image + 1;
            }

            this.pictureBox_case.BackgroundImage = this.imageList_case.Images[index_image];

        }

        /// <summary>
        /// modification du matériel présent sur la case</summary>
        /// <param name="lettre"> Le code lettre de la pièce</param>
        /// <param name="proprietaire">Le propriétaire blanc ou noir de la pièce</param>
        public void change_piece(char lettre, Joueur proprietaire)
        {
            if (lettre_piece != lettre || proprietaire_piece != proprietaire)
            {
                lettre_piece = lettre;
                proprietaire_piece = proprietaire;
                dessiner();
            }
        }

        /// <summary>
        /// Evennement : le joueur a cliqué la case</summary>
        private void pictureBox_case_Click(object sender, EventArgs e)
        {
            if (clicActif == true)
            {
                echiquier.selectionnerCase(posColonne, posLigne);
                dessiner_bordure();
            }
        }

        /// <summary>
        /// Evennement : le joueur passe la souris devant la case</summary>
        private void pictureBox_case_MouseEnter(object sender, EventArgs e)
        {

                this.pictureBox_case.Image = imageList_case.Images[29];
                surbrillance_bordure = true;
                dessiner_bordure();

        }

        /// <summary>
        /// Evennement : le joueur déplace la souris en dehors de la case</summary>
        private void pictureBox_case_MouseLeave(object sender, EventArgs e)
        {

                surbrillance_bordure = false;
                dessiner_bordure();
        }



    }
}
