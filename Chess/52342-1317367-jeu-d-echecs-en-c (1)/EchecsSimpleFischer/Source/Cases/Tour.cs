using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace simpleFischer
{
    /// <summary>
    /// Pièce Tour </summary>
    /// <remarks>
    /// Cette pièce se déplace en ligne droite.
    /// Le roi peut roquer avec elle, à condition qu'elle ne se soit pas déplacée.
    /// </remarks>
    public class Tour : Piece
    {
        /// <summary>
        /// Mémorise si la tour a déjà été déplacée </summary>
        private Boolean déplacé = false;

        /// <summary>
        /// Constructeur de la classe. </summary>
        /// <param name="proprietaire">Joueur de même couleur qui peut manipuler la pièce</param>
        public Tour(Joueur proprietaire_piece, Boolean déjaDéplacé = false)
            : base(proprietaire_piece)
        {
            déplacé = déjaDéplacé;
        }

        /// <summary>
        /// La tour a-t-elle déjà été déplacée ? (lecture seule) </summary>
        public Boolean aDéjaEtéDéplacé
        {
            get
            {
                return déplacé;
            }
        }

        /// <summary>
        /// Code de la pièce : Première lettre du nom </summary>
        override public char lettre
        {
            get
            {
                return 'T';
            }
        }

        /// <summary>
        /// Calculer les coups de la pièce </summary>
        /// <param name="position"> Position initiale </param>
        /// <param name="position"> Référence au tableau de coups à compléter</param>
        /// <param name="posColonne"> Coordonnée X de la pièce</param>
        /// <param name="posColonne"> Coordonnée Y de la pièce </param>
        override public void coups(Position position, ArrayList coupsPossibles, int posColonne, int posLigne)
        {
            // la tour se déplace en lignes horizontales et verticales
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 0, 1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 0, -1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, -1, 0);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 1, 0);
        }

        /// <summary>
        /// Clône de la tour pour une position suivante </summary>
        /// <param name="deplacement"> La tour est elle en déplacement ?</param>
        /// <returns> Une case de position </returns>
        override public Case positionSuivanteCase(Boolean deplacement=false)
        {
            if (deplacement == true || this.déplacé == true)
            {
                return new Tour(this.proprietaire, true);
            }
            else
            {
                return new Tour(this.proprietaire, false);
            }
        }


    }
}
