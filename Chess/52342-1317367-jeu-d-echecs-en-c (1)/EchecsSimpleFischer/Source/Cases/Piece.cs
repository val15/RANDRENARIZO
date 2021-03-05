using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace simpleFischer
{
    /// <summary>
    /// Case Pièce</summary>
    /// <remarks>
    /// Cette classe peut être héritée en : Roi, Dame, Cavalier, Fou, Tour, Pion
    /// </remarks>
    public class Piece : Case
    {

        /// <summary>
        /// Constructeur de la classe. </summary>
        /// <param name="proprietaire">Joueur de même couleur qui peut manipuler la pièce</param>
        public Piece(Joueur proprietaire_p)
            : base()
        {
            proprietaire = proprietaire_p;

        }

        /// <summary>
        /// Case occupée par une pièce (lecture seule) </summary>
        override public Boolean occupé
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Fonction facilitante pour les lignes droites de déplacements  </summary>
        /// <param name="incrementX">détermine la direction du déplacement dans l'axe X</param>
        /// <param name="incrementY">détermine la direction du déplacement dans l'axe Y</param>
        /// <remarks>
        /// Les pièces concernées sont : le fou, la tour, la dame
        /// </remarks>        
        protected void ajouterLigneCoups(Position position, ArrayList coupsPossibles, int colonne, int ligne, int incrementX, int incrementY)
        {
            int colonnes;
            int lignes;
            Case c;
            Boolean obstacle;

            colonnes = 0;
            lignes = 0;
            obstacle = false;
            while (obstacle == false)
            {
                colonnes += incrementX;
                lignes += incrementY;

                if ((colonne + colonnes) >= 0 && (colonne + colonnes) <= 7 && (ligne + lignes) >= 0 && (ligne + lignes) <= 7)
                {
                    c = position.Case[colonne + colonnes, ligne + lignes];
                    if (c.occupé == false)
                    {
                        // possibilité de se déplacer 
                        coupsPossibles.Add(new Deplacement(position, colonne, ligne, colonne+colonnes, ligne+lignes));
                    }
                    else
                    {
                        // on a rencontré une pièce donc impossible d'aller plus loin
                        obstacle = true;
                        if (c.joueur_proprietaire != this.joueur_proprietaire)
                        {
                            // possibilité de prendre la pièce adverse
                            coupsPossibles.Add(new Prise(position, colonne, ligne, colonne + colonnes, ligne + lignes));
                        }
                    }
                }
                else
                {
                    // sortie de l'échiquier : impossible d'aller plus loin
                    obstacle = true;
                }
            }

        }
        


    }
}
