using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace simpleFischer
{
    /// <summary>
    /// Partie d'échecs. </summary>
    /// <remarks>
    /// La classe principale, qui gère à la fois les données de la partie et l'écran d'affichage.
    /// </remarks>
    public partial class Partie : Form
    {
        /// <summary>
        /// Référence statique vers le joueur blanc </summary>
        static private Joueur joueurBlancs;

        /// <summary>
        /// Référence statique vers le joueur noir</summary>
        static private Joueur joueurNoirs;

        /// <summary>
        /// Gestionnaire de délégué gérant la fermeture du jeu</summary>
        private delegate void PartieCloseDelegateHandler();
        /// <summary>
        /// Délégué gérant la fermeture du jeu</summary>
        private PartieCloseDelegateHandler partieCloseDelegate;

        /// <summary>
        /// Référence de la position de jeu en cours</summary>
        private Position positionEnCours;

        /// <summary>
        /// Référence vers le dernier coup joué</summary>
        private Coup dernierCoupJoué = null;

        /// <summary>
        /// Constructeur de la classe. </summary>
        public Partie()
        {
            InitializeComponent();

            partieCloseDelegate = new PartieCloseDelegateHandler(partieClose);

            this.userControl_pendule1.chuteDrapeauHandler += new System.EventHandler(this.chuteDrapeau);
        }

        /// <summary>
        /// Activation/Désactivation du mode de jeu manuel (écriture seule) </summary>
        public Boolean activerCoupsEchiquier
        {
            set
            {
                this.userControl_echiquier1.clicsActivés = value;
            }
        }

        /// <summary>
        /// Dernier coup joué (lecture seule)</summary>
        public Coup dernierCoup
        {
            get
            {
                return dernierCoupJoué;
            }
        }

        /// <summary>
        /// Joueur blanc (lecture seule) </summary>
        static public Joueur blancs
        {
           get
           {
               return joueurBlancs;
           }
        }

        /// <summary>
        /// Joueur noir (lecture seule) </summary>
        static public Joueur noirs
        {
            get
            {
                return joueurNoirs;
            }
        }

        /// <summary>
        /// Position en cours (lecture seule) </summary>
        public Position position
        {
            get
            {
                return positionEnCours;
            }
        }

        /// <summary>
        /// Evennement : Initialisation graphique de la partie</summary>
        private void Partie_Load(object sender, EventArgs e)
        {
            initialiser();
        }

        /// <summary>
        /// Evennement : Chute d'un drapeau </summary>
        /// <param name="e"> contient une référence sur le joueur dont le drapeau a chuté</param>
        void chuteDrapeau(object sender, EventArgs e)
        {
            ChuteDrapeauArgs args = (ChuteDrapeauArgs)e;
            terminerPartie(args.couleur.messageChuteDrapeau);

        }

        /// <summary>
        /// Initialisation d'une nouvelle partie</summary>
        public void initialiser()
        {
            ///////////////////////////////////////////
            // choix de la couleur du joueur
            Form_choix form_choix = new Form_choix();
            DialogResult choix_couleur = form_choix.ShowDialog();

            if (choix_couleur == DialogResult.Yes)
            {
                joueurBlancs = new Humain(this);
                joueurNoirs = new Ordinateur(this);
                // on oriente l'échiquier face au joueur
                this.userControl_echiquier1.initialiser(blancs, this);
            }
            else
            {
                joueurBlancs = new Ordinateur(this);
                joueurNoirs = new Humain(this);
                // on oriente l'échiquier face au joueur
                this.userControl_echiquier1.initialiser(noirs, this);
            }

            // initialisation de la position
            this.positionEnCours = new Position();
            this.dernierCoupJoué = null;
            this.userControl_echiquier1.afficher();

            // initialisation de la feuille de jeu
            this.userControl_feuilleDeJeu1.initialiser();

            // initialisation de la pendule
            this.userControl_pendule1.initialiser();

            // donner le trait aux blancs : la partie commence
            Partie.blancs.trait();
        }

        /// <summary>
        /// Un joueur joue un coup.</summary
        /// <param name="coup"> le Coup joué</param>
        public void jouerCoup(Coup coup)
        {
            // stockage du coup et de la nouvelle position
            this.dernierCoupJoué = coup;
            this.positionEnCours = this.dernierCoupJoué.positionResultante;
            
            // mise à jour graphique de l'échiquier et de la feuille de jeu
            this.userControl_echiquier1.afficher();
            this.userControl_echiquier1.afficherDernierCoup(this.dernierCoupJoué.colonne2, this.dernierCoupJoué.ligne2);
            this.userControl_feuilleDeJeu1.noterCoup(this.dernierCoupJoué);

            if (this.position.MAT == true)
            {                
                terminerPartie(this.position.trait.messageMat);
            }
            else if (this.position.PAT == true)
            {
                terminerPartie(this.position.trait.messagePat);
            }
            else
            {
                // il n'y a ni MAT, ni PAT, donc la partie continue
                this.userControl_pendule1.basculer();
                this.position.trait.trait();
            }
        }

        /// <summary>
        /// Partie terminée: proposer de rejouer.</summary>
        /// <param name="messageResultat"> Le message annonçant le verdict de la partie.</param>
        private void terminerPartie(String messageResultat)
        {
            this.userControl_pendule1.arreter();
            Form_resultat form_resultat = new Form_resultat(messageResultat);
            DialogResult nouvelle_partie = form_resultat.ShowDialog();

            if (nouvelle_partie == DialogResult.Yes)
            {
                this.initialiser();
            }
            else
            {
                this.Invoke(this.partieCloseDelegate, null);   
            }
        }

        /// <summary>
        /// Fermeture forcée de la fenettre. </summary>
        private void partieClose()
        {
            this.Close();
        }

    }
}
