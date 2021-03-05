using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace simpleFischer
{
    /// <summary>
    /// Pièce Dame </summary>
    /// <remarks>
    /// La Dame se déplace comme un fou et comme une tour
    /// </remarks>
    public class Dame : Piece
    {
        /// <summary>
        /// Constructeur de la classe. </summary>
        /// <param name="proprietaire">Joueur de même couleur qui peut manipuler la pièce</param>
        public Dame (Joueur proprietaire_piece)
            : base(proprietaire_piece)
        {
        }

        /// <summary>
        /// Code de la pièce : Première lettre du nom  </summary>
        override public char lettre
        {
            get
            {
                return 'D';
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
            // les 4 mouvements de Dame en diagonale (comme un fou)
            this.ajouterLigneCoups(position, coupsPossibles,posColonne, posLigne, 1, 1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, -1, -1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, -1, 1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 1, -1);
            // les 4 mouvements de Dame en ligne droite (comme une tour)
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 0, 1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 0, -1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, -1, 0);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 1, 0);

        }

        /// <summary>
        /// Clône de la dame pour une position suivante</summary>
        /// <param name="deplacement"> La dame vient-elle de se déplacer ?</param>
        /// <returns>
        /// Une case de position </returns>
        override public Case positionSuivanteCase(Boolean deplacement)
        {
            // clone pour position suivante
            Case posSuivanteCase = new Dame(this.proprietaire);
            return posSuivanteCase;
        }



    }
}
