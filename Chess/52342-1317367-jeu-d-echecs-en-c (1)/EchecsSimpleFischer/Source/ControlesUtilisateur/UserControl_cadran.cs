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
    /// UserControl Cadran</summary>
    /// <remarks>
    /// Cadran à affichage numérique, avec compte à rebours.
    /// Il est destiné aux pendules (UserControl_pendule)
    /// </remarks>
    public partial class UserControl_cadran : UserControl
    {
        /// <summary>
        /// Gestionnaire d'évennement de la chute de drapeau</summary>
        public event EventHandler chuteDrapeau;

        /// <summary>
        /// Gestionnaire de délégué de mise à jour des images/summary>
        private delegate void PictureBoxesDelegateHandler();

        /// <summary>
        /// Délégué de mise à jour des images</summary>
        private PictureBoxesDelegateHandler PictureBoxesDelegate;

        /// <summary>
        /// Temps restant en secondes </summary>
        private Double temps_restant=0;

        /// <summary>
        /// Compte à rebours activé ou non</summary>
        private Boolean decompte_actif = false;

        /// <summary>
        /// Constructeur de la classe. </summary>
        public UserControl_cadran()
        {
            InitializeComponent();

            this.pictureBox_dizainesminutes.Image = this.imageList_digital.Images[0];
            this.pictureBox_minutes.Image = this.imageList_digital.Images[0];
            this.pictureBox_dizainessecondes.Image = this.imageList_digital.Images[0];
            this.pictureBox_secondes.Image = this.imageList_digital.Images[0];
            this.pictureBox_point.Image = this.imageList_digital.Images[14];
            this.pictureBox_play.Image = this.imageList_digital.Images[12];

            PictureBoxesDelegate = new PictureBoxesDelegateHandler(pictureBoxesUpdate);
        }

        /// <summary>
        /// Activation/Désactivation du compte à rebours </summary>
        public Boolean actif
        {
            get
            {
                return decompte_actif;
            }
            set
            {
                decompte_actif = value;
                afficher();

            }
        }

        /// <summary>
        /// Lecture et mise à jour du temps restant </summary>
        public int temps
        {
            get
            {
                return (int)Math.Ceiling(temps_restant);
            }
            set
            {
                temps_restant = value;
                this.afficher();
            }
        }

        /// <summary>
        /// Déclancher l'évenement de chute de drapeau</summary>
        protected virtual void déclancherChuteDrapeau()
        {
            EventHandler handler = chuteDrapeau;

            if (handler != null)
            {
                handler(this, null);
            }
        }

        /// <summary>
        /// Mise à jour des images.</summary>
        private void pictureBoxesUpdate()
        {
            int minutes;
            int secondes;
            int chiffre1_minutes;
            int chiffre1_secondes;
            int chiffre2_minutes;
            int chiffre2_secondes;

            minutes = (int)Math.Floor(temps_restant / 60);
            secondes = (int)Math.Floor(temps_restant - (minutes * 60));
            chiffre1_minutes = minutes / 10;
            chiffre2_minutes = minutes - (chiffre1_minutes * 10);
            chiffre1_secondes = secondes / 10;
            chiffre2_secondes = secondes - (chiffre1_secondes * 10);

            if (chiffre1_minutes > 0)
            {
                // on affiche tous les chiffres
                this.pictureBox_dizainesminutes.Image = this.imageList_digital.Images[chiffre1_minutes + 1];
                this.pictureBox_minutes.Image = this.imageList_digital.Images[chiffre2_minutes + 1];
                this.pictureBox_point.Image = this.imageList_digital.Images[13];
                this.pictureBox_dizainessecondes.Image = this.imageList_digital.Images[chiffre1_secondes + 1];
                this.pictureBox_secondes.Image = this.imageList_digital.Images[chiffre2_secondes + 1];
            }
            else if (chiffre2_minutes > 0)
            {
                // on affiche tous les chiffres sauf les dizaines de minutes
                this.pictureBox_dizainesminutes.Image = this.imageList_digital.Images[0];
                this.pictureBox_minutes.Image = this.imageList_digital.Images[chiffre2_minutes + 1];
                this.pictureBox_point.Image = this.imageList_digital.Images[13];
                this.pictureBox_dizainessecondes.Image = this.imageList_digital.Images[chiffre1_secondes + 1];
                this.pictureBox_secondes.Image = this.imageList_digital.Images[chiffre2_secondes + 1];
            }
            else if (chiffre1_secondes > 0)
            {
                // on affiche seulement les secondes
                this.pictureBox_dizainesminutes.Image = this.imageList_digital.Images[0];
                this.pictureBox_minutes.Image = this.imageList_digital.Images[0];
                this.pictureBox_point.Image = this.imageList_digital.Images[14];
                this.pictureBox_dizainessecondes.Image = this.imageList_digital.Images[chiffre1_secondes + 1];
                this.pictureBox_secondes.Image = this.imageList_digital.Images[chiffre2_secondes + 1];
            }
            else
            {
                // on affiche seulement le second chiffre des secondes
                this.pictureBox_dizainesminutes.Image = this.imageList_digital.Images[0];
                this.pictureBox_minutes.Image = this.imageList_digital.Images[0];
                this.pictureBox_point.Image = this.imageList_digital.Images[14];
                this.pictureBox_dizainessecondes.Image = this.imageList_digital.Images[0];
                this.pictureBox_secondes.Image = this.imageList_digital.Images[chiffre2_secondes + 1];
            }

            if (decompte_actif == true)
            {
                this.pictureBox_play.Image = this.imageList_digital.Images[11];
            }
            else
            {
                this.pictureBox_play.Image = this.imageList_digital.Images[12];
            }
        }

        /// <summary>
        /// Affichage du cadran.</summary>
        private void afficher()
        {
            this.Invoke(this.PictureBoxesDelegate, null);
        }

        /// <summary>
        /// Evennement : 100 milisecondes se sont écoulées</summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (decompte_actif == true)
            {
                if (temps_restant > 0)
                {
                    temps_restant = temps_restant - 0.1;

                    if (temps_restant <= 0)
                    {
                        temps_restant = 0;
                        decompte_actif = false;
                        déclancherChuteDrapeau();
                    }
                    else
                    {
                        afficher();
                    }
                }
            }     
        }

    }
}
