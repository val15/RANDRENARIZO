using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace simpleFischer
{
    /// <summary>
    /// Pièce Fou</summary>
    /// <remarks>
    /// Le Fou se déplace en diagonale.
    /// </remarks>
    public class Fou : Piece
    {
        /// <summary>
        /// Constructeur de la classe. </summary>
        /// <param name="proprietaire">Joueur de même couleur qui peut manipuler la pièce</param>
        public Fou ( Joueur proprietaire)
            : base(proprietaire)
        {
        }

        /// <summary>
        /// Code de la pièce : Première lettre du nom  </summary>
        override public char lettre
        {
            get
            {
                return 'F';
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
            // les 4 mouvements en diagonale du fou
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 1, 1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, -1, -1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, -1, 1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 1, -1);

        }

        /// <summary>
        /// Clône du fou pour une position suivante</summary>
        /// <param name="deplacement"> Le fou vient-il de se déplacer ?</param>
        /// <returns>
        /// Une case de position </returns>
        override public Case positionSuivanteCase(Boolean deplacement = false)
        {
            // clone pour position suivante
            Case posSuivanteCase = new Fou(this.proprietaire);
            return posSuivanteCase;
        }


    }
}
