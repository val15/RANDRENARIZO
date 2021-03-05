using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace simpleFischer
{
    /// <summary>
    /// Pièce Cavalier</summary>
    /// <remarks>
    /// Le cavalier se déplace en sautant deux case dans une direction et une case dans l'autre
    /// </remarks>
    public class Cavalier : Piece
    {
        /// <summary>
        /// Constructeur de la classe. </summary>
        /// <param name="proprietaire">Joueur de même couleur qui peut manipuler la pièce</param>
        public Cavalier (Joueur proprietaire)
            : base(proprietaire)
        {
        }

        /// <summary>
        /// Code de la pièce : Première lettre du nom </summary>
        override public char lettre
        {
            get
            {
                return 'C';
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

            // test des 8 déplacements (ou prises) possibles pour un cavalier 

            if (posColonne>0 && posLigne > 1)
            {
                if (position.Case[posColonne-1, posLigne - 2].occupé == false)
                {
                    // deplacement 
                    coupsPossibles.Add( new Deplacement(position, posColonne, posLigne, posColonne - 1, posLigne - 2));
                }
                else
                {
                    if (position.Case[posColonne-1, posLigne - 2].joueur_proprietaire != this.joueur_proprietaire)
                    {
                        // prise
                        coupsPossibles.Add(new Prise(position, posColonne, posLigne, posColonne -1, posLigne -2));
                    }
                }
            }
            if (posColonne > 1 && posLigne > 0)
            {
                if (position.Case[posColonne - 2, posLigne - 1].occupé == false)
                {
                    // deplacement 
                    coupsPossibles.Add(new Deplacement(position, posColonne, posLigne,posColonne -2, posLigne -1));
                }
                else
                {
                    if (position.Case[posColonne - 2, posLigne - 1].joueur_proprietaire != this.joueur_proprietaire)
                    {
                        // prise
                        coupsPossibles.Add(new Prise(position, posColonne, posLigne, posColonne -2, posLigne -1));
                    }
                }
            }
            if (posColonne < 6 && posLigne > 0)
            {
                if (position.Case[posColonne + 2, posLigne - 1].occupé == false)
                {
                    // deplacement 
                    coupsPossibles.Add(new Deplacement(position,posColonne, posLigne, posColonne +2, posLigne -1));
                }
                else
                {
                    if (position.Case[posColonne + 2, posLigne - 1].joueur_proprietaire != this.joueur_proprietaire)
                    {
                        // prise
                        coupsPossibles.Add(new Prise(position,posColonne, posLigne, posColonne +2, posLigne -1));
                    }
                }
            }
            if (posColonne < 6 && posLigne < 7 )
            {
                if (position.Case[posColonne + 2, posLigne + 1].occupé == false)
                {
                    // deplacement 
                    coupsPossibles.Add(new Deplacement(position,posColonne, posLigne, posColonne +2, posLigne +1));
                }
                else
                {
                    if (position.Case[posColonne + 2, posLigne + 1].joueur_proprietaire != this.joueur_proprietaire)
                    {
                        // prise
                        coupsPossibles.Add(new Prise(position,posColonne, posLigne,posColonne +2,posLigne +1));
                    }
                }
            }
            if (posColonne > 1 && posLigne < 7)
            {
                if (position.Case[posColonne - 2, posLigne + 1].occupé == false)
                {
                    // deplacement 
                    coupsPossibles.Add(new Deplacement(position,posColonne, posLigne, posColonne -2, posLigne +1));
                }
                else
                {
                    if (position.Case[posColonne - 2, posLigne + 1].joueur_proprietaire != this.joueur_proprietaire)
                    {
                        // prise
                        coupsPossibles.Add(new Prise(position,posColonne, posLigne, posColonne -2, posLigne +1));
                    }
                }
            }
            if (posColonne <7 && posLigne < 6)
            {
                if (position.Case[posColonne+1, posLigne + 2].occupé == false)
                {
                    // deplacement 
                    coupsPossibles.Add(new Deplacement(position,posColonne, posLigne, posColonne +1, posLigne +2));
                }
                else
                {
                    if (position.Case[posColonne+1, posLigne + 2].joueur_proprietaire != this.joueur_proprietaire)
                    {
                        // prise
                        coupsPossibles.Add(new Prise(position,posColonne, posLigne,posColonne +1, posLigne +2));
                    }
                }
            }
            if (posColonne < 7 && posLigne>1)
            {
                if (position.Case[posColonne + 1, posLigne-2].occupé == false)
                {
                    // deplacement 
                    coupsPossibles.Add(new Deplacement(position, posColonne, posLigne, posColonne +1, posLigne -2));
                }
                else
                {
                    if (position.Case[posColonne + 1, posLigne-2].joueur_proprietaire != this.joueur_proprietaire)
                    {
                        // prise
                        coupsPossibles.Add(new Prise(position,posColonne, posLigne, posColonne +1, posLigne -2));
                    }
                }
            }
            if (posColonne > 0 && posLigne<6)
            {
                if (position.Case[posColonne - 1, posLigne+2].occupé == false)
                {
                    // deplacement 
                    coupsPossibles.Add(new Deplacement(position,posColonne, posLigne,posColonne -1, posLigne+ 2));
                }
                else
                {
                    if (position.Case[posColonne - 1, posLigne+2].joueur_proprietaire != this.joueur_proprietaire)
                    {
                        // prise
                        coupsPossibles.Add(new Prise(position,posColonne, posLigne, posColonne -1, posLigne+ 2));
                    }
                }
            }

        }

        /// <summary>
        /// Clône du cavalier pour une position suivante</summary>
        /// <param name="deplacement"> Le cavalier vient-il de se déplacer ?</param>
        /// <returns>
        /// Une case de position </returns>
        override public Case positionSuivanteCase(Boolean deplacement = false)
        {
            // clone pour position suivante
            Case posSuivanteCase = new Cavalier(this.proprietaire);
            return posSuivanteCase;
        }



    }
}
