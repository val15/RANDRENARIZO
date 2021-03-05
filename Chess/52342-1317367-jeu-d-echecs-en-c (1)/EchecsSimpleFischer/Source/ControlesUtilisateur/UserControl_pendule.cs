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
    /// UserControl Pendule de jeu.</summary>
    /// <remarks>
    /// La pendule est constitué de 2 cadrans (1 par joueur) basculés à chaque tour.
    /// Elle gère l'évenement de chute des drapeaux (temps écoulé d'un joueur)
    /// </remarks>
    public partial class UserControl_pendule : UserControl
    {
        /// <summary>
        ///  Bonus de temps à chaque coup </summary>
        private int bonus_temps = 0;

        /// <summary>
        /// Pointeur sur le cadran des blancs </summary>
        private UserControl_cadran userControl_cadranBlanc;

        /// <summary>
        /// Pointeur sur le cadran des noirs</summary>
        private UserControl_cadran userControl_cadranNoir;

        /// <summary>
        /// Gestionnaire d'évennement de chute du drapeau </summary>
        public event EventHandler chuteDrapeauHandler;

        /// <summary>
        /// Constructeur de la classe. </summary>
        public UserControl_pendule()
        {
            InitializeComponent();

            
            this.userControl_cadranBlanc = new simpleFischer.UserControl_cadran();
            this.userControl_cadranNoir = new simpleFischer.UserControl_cadran();
            this.SuspendLayout();
            // 
            // userControl_cadran1
            // 
            this.userControl_cadranBlanc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(153)))), ((int)(((byte)(133)))));
            this.userControl_cadranBlanc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userControl_cadranBlanc.Location = new System.Drawing.Point(13, 5);
            this.userControl_cadranBlanc.Name = "userControl_cadran1";
            this.userControl_cadranBlanc.Size = new System.Drawing.Size(167, 54);
            this.userControl_cadranBlanc.TabIndex = 0;
            // 
            // userControl_cadran2
            // 
            this.userControl_cadranNoir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(153)))), ((int)(((byte)(133)))));
            this.userControl_cadranNoir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userControl_cadranNoir.Location = new System.Drawing.Point(199, 5);
            this.userControl_cadranNoir.Name = "userControl_cadran2";
            this.userControl_cadranNoir.Size = new System.Drawing.Size(167, 54);
            this.userControl_cadranNoir.TabIndex = 1;


            this.Controls.Add(this.userControl_cadranBlanc);
            this.Controls.Add(this.userControl_cadranNoir);

            this.userControl_cadranBlanc.chuteDrapeau += new System.EventHandler(this.chuteDrapeauBlanc);
            this.userControl_cadranNoir.chuteDrapeau += new System.EventHandler(this.chuteDrapeauNoir);
        }

        /// <summary>
        /// Déclancher l'évenement de chute de drapeau </summary>
        /// <param name="couleur"> Référence au joueur dont le cadran est arrivé à zéro</param>
        private void déclancherChuteDrapeau(Joueur couleur)
        {
            EventArgs args = new ChuteDrapeauArgs(couleur);

            EventHandler handler = chuteDrapeauHandler;
            if (handler != null)
            {
                handler(this, args);
            }

        }

        /// <summary>
        /// Evennement: chute du drapeau des blancs </summary>
        void chuteDrapeauBlanc(object sender, EventArgs e)
        {
            this.déclancherChuteDrapeau(Partie.blancs);
        }

        /// <summary>
        /// Evennement: chute du drapeau des noirs</summary>
        void chuteDrapeauNoir(object sender, EventArgs e)
        {
            this.déclancherChuteDrapeau(Partie.noirs);
        }


        /// <summary>
        /// Initialisation de la pendule</summary>
        /// <param name="temps_initial">Temps au début de la partie</param>
        /// <param name="bonus">Incrément Fischer</param>
        public void initialiser(int temps_initial=900, int bonus=2)
        {
            bonus_temps = bonus;
            this.userControl_cadranBlanc.temps = temps_initial;
            this.userControl_cadranNoir.temps = temps_initial;
            this.userControl_cadranNoir.actif = false;
            this.userControl_cadranBlanc.actif = true;
        }

        /// <summary>
        /// Arrêt de la pendule</summary>
        public void arreter()
        {
            this.userControl_cadranNoir.actif = false;
            this.userControl_cadranBlanc.actif = false;
        }

        /// <summary>
        /// Basculement de la pendule après un coup</summary>
        public void basculer()
        {
            if (this.userControl_cadranBlanc.actif == true)
            {
                this.userControl_cadranBlanc.temps += bonus_temps;
                this.userControl_cadranBlanc.actif = false;
                this.userControl_cadranNoir.actif = true;
            }
            else if (this.userControl_cadranNoir.actif == true)
            {
                this.userControl_cadranNoir.temps += bonus_temps;
                this.userControl_cadranNoir.actif = false;
                this.userControl_cadranBlanc.actif = true;
            }
            else
            {
                 // erreur : on ne peut pas basculer la pendule si elle est à l'arret
            }
        }
        

    }

    /// <summary>
    /// Classe d'arguments de l'évenement chute de drapeau </summary>
    public class ChuteDrapeauArgs : EventArgs
    {
        /// <summary>
        /// Constructeur </summary>
        public ChuteDrapeauArgs(Joueur couleur)
        {
            this.couleur = couleur;
        }
        public Joueur couleur;
    }  
}
