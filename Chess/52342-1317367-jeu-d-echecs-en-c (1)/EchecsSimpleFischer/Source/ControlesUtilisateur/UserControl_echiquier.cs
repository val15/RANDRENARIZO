using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace simpleFischer
{
    /// <summary>
    /// UserControl échiquier </summary>
    /// <remarks>
    /// Affichage graphique de la Position en cours sur l'échiquier
    /// Constitué de 64 instance de UserControl_case
    /// </remarks>
    public partial class UserControl_echiquier : UserControl
    {
        /// <summary>
        /// Gestionnaire de délégué d'affichage des cases</summary>
        private delegate void PositionCaseDelegateHandler();

        /// <summary>
        /// Délégué d'affichage des cases </summary>
        private PositionCaseDelegateHandler positionCaseDelegate;

        /// <summary>
        /// Tableau des 8x8=64 cases </summary>
        private UserControl_case[,] userControl_case=null;

        /// <summary>
        /// Case sélectionnée pour départ d'un coup</summary>
        private UserControl_case caseSelectionneeDepart=null;

        /// <summary>
        /// Case sélectionnée pour arrivée d'un coup</summary>
        private UserControl_case caseSelectionneeArrivee = null;

        /// <summary>
        /// Pointeur vers la partie affichée </summary>
        private Partie partie;

        /// <summary>
        /// Orientation de l'échiquier sur l'écran</summary>
        private Boolean orientation;

        /// <summary>
        /// Constructeur de la classe. </summary>
        public UserControl_echiquier()
        {
             InitializeComponent();
             int x;
             int y;
             char couleur_case = 'f';

             this.userControl_case = new simpleFischer.UserControl_case[8, 8];

             for (x = 0; x < 8; x++)
             {
                 for (y = 0; y < 8; y++)
                 {

                     this.userControl_case[x, y] = new simpleFischer.UserControl_case(this, x, y);

                     this.userControl_case[x, y].Location = new System.Drawing.Point(350 - x * 50, 350 - y * 50);

                     this.userControl_case[x, y].Name = "userControl_case";
                     this.userControl_case[x, y].Size = new System.Drawing.Size(50, 50);
                     this.userControl_case[x, y].TabIndex = 0;
                     this.userControl_case[x, y].Couleur_case = couleur_case;
                     this.Controls.Add(this.userControl_case[x, y]);

                     if (y < 7)
                     {
                         if (couleur_case == 'f')
                         {
                             couleur_case = 'c'; // case claire
                         }
                         else
                         {
                             couleur_case = 'f'; // case foncée 
                         }
                     }
                 }
             }

             positionCaseDelegate = new PositionCaseDelegateHandler(positionCaseUpdate);

        }

        /// <summary>
        /// Activer/Désactiver les clics (écriture seule) </summary>
        public Boolean clicsActivés
        {
            set
            {
                int x;
                int y;

                for (x = 0; x < 8; x++)
                {
                    for (y = 0; y < 8; y++)
                    {
                        this.userControl_case[x, y].clicActif = value;
                    }
                }
            }
        }

        /// <summary>
        /// Mise à jour graphique de l'emplacement des cases de jeu</summary>
        private void positionCaseUpdate()
        {
            int x;
            int y;

            for (x = 0; x < 8; x++)
            {
                for (y = 0; y < 8; y++)
                {
                    this.userControl_case[x, y].bordure = ' ';
                    if (orientation == true)
                    {
                        this.userControl_case[x, y].Location = new System.Drawing.Point(x * 50, 350 - y * 50);
                    }
                    else
                    {
                        this.userControl_case[x, y].Location = new System.Drawing.Point(350 - x * 50, y * 50);
                    }
                }
            }


        }

        /// <summary>
        /// Initialisation graphique de l'échiquier</summary>
        /// <param name="joueurPrincipal"> Le joueur vers lequel on oriente l'échiquier</param>
        /// <param name="nouvellePartie"> Référence à la partie à afficher</param>
        public void initialiser(Joueur joueurPrincipal, Partie nouvellePartie)
        {
            partie = nouvellePartie;

            if (joueurPrincipal == Partie.blancs)
            {
                orientation = true;
            }
            else
            {
                orientation = false;
            }

            this.Invoke(this.positionCaseDelegate, null);

        }

        /// <summary>
        /// Afficher le dernier coup sur l'échiquier par surlignage</summary>
        /// <param name="colonne"> Colonne de la case d'arrivée du coup</param>
        /// <param name="ligne"> Ligne de la case d'arrivée du coup</param>
        public void afficherDernierCoup(int colonne, int ligne)
        {
              if (caseSelectionneeArrivee != null)
              {
                   caseSelectionneeArrivee.bordure = ' ';
                   caseSelectionneeArrivee = null;
              }
              caseSelectionneeArrivee =  this.userControl_case[colonne, ligne];
              caseSelectionneeArrivee.bordure = 'a';

        }



        /// <summary>
        /// Selectionner une case pour joueur un coup</summary>
        /// <param name="colonne"> Colonne de la case </param>
        /// <param name="ligne"> Ligne de la case</param>
        public void selectionnerCase(int colonne, int ligne)
        {
            UserControl_case nouvelleCaseSelectionnee;
            nouvelleCaseSelectionnee=this.userControl_case[colonne, ligne];

            if (caseSelectionneeDepart == null)
            {
                foreach (Coup c in partie.position.coupsPermisLégaux)
                {

                   if (c.colonne1 == colonne && c.ligne1 == ligne)
                   {
                            caseSelectionneeDepart = nouvelleCaseSelectionnee;
                            caseSelectionneeDepart.bordure = 'd';
                   }

                }
            }
            else if (caseSelectionneeDepart == nouvelleCaseSelectionnee)
            {
                caseSelectionneeDepart.bordure = ' ';
                caseSelectionneeDepart = null;
            }
            else
            {
                foreach (Coup c in partie.position.coupsPermisLégaux)
                {
                        if (c.colonne1 == caseSelectionneeDepart.posColonne && c.ligne1 == caseSelectionneeDepart.posLigne)
                        {
                            if (c.colonne2 == colonne && c.ligne2 == ligne)
                            {
                                caseSelectionneeDepart.bordure = ' ';
                                caseSelectionneeDepart = null;

                                partie.jouerCoup(c);
                                break;
                            }
                        }
                }
            }
        }

        /// <summary>
        /// Afficher la position en cours.</summary>
        public void afficher()
        {
            Position position = partie.position;
            int x;
            int y;
            for (x = 0; x < 8; x++)
            {
                for (y = 0; y < 8; y++)
                {
                    this.userControl_case[x, y].change_piece(position.Case[x, y].lettre, position.Case[x, y].joueur_proprietaire);
                }
            }
        }


    }
}
