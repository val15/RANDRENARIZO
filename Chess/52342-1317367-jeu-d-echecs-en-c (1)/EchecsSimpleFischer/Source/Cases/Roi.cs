﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace simpleFischer
{
    /// <summary>
    /// Pièce roi</summary>
    /// <remarks>
    /// Le roi se déplace d'une case en diagonale et ligne droite. 
    /// Il peut également roquer, sous conditions.
    /// Il ne peut jamais être pris dans une position légale. 
    /// </remarks>
    public class Roi : Piece
    {
        /// <summary>
        /// Mémorise si le roi a déja été déplacé </summary>
        private Boolean déplacé = false;

        /// <summary>
        /// Constructeur de la classe. </summary>
        /// <param name="proprietaire">Joueur de même couleur qui peut manipuler la pièce</param>
        public Roi(Joueur proprietaire_piece, Boolean déjaDéplacé = false)
            : base(proprietaire_piece)
        {
            déplacé = déjaDéplacé;
        }

        /// <summary>
        /// Le roi a t-il déja été déplacé ? (lecture seule) </summary>
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
                return 'R';
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
            // le roi se déplace dans toutes les directions mais d'une seule case maximum
            // il peut aussi roquer si :
            // _ il ne s'est jamais déplacé
            // _ la tour du coté du roque ne s'est jamais déplacée
            // _ toutes les cases entre lui et la tour sont libres
            // en outre, la légalité du roque est vérifiée dans les classes PetitRoque et GrandRoque

                if (posLigne > 0)
                {
                    if (position.Case[posColonne, posLigne - 1].occupé == false)
                    {
                        // deplacement 
                        coupsPossibles.Add( new Deplacement(position, posColonne, posLigne, posColonne + 0, posLigne - 1));
                    }
                    else
                    {
                        if (position.Case[posColonne, posLigne - 1].joueur_proprietaire != this.joueur_proprietaire)
                        {
                            // prise
                            coupsPossibles.Add( new Prise(position, posColonne, posLigne, posColonne + 0, posLigne - 1));
                        }
                    }
                }
                if (posLigne > 0 && posColonne > 0)
                {
                    if (position.Case[posColonne - 1, posLigne - 1].occupé == false)
                    {
                        // deplacement 
                        coupsPossibles.Add( new Deplacement(position, posColonne, posLigne, posColonne - 1, posLigne - 1));
                    }
                    else
                    {
                        if (position.Case[posColonne - 1, posLigne - 1].joueur_proprietaire != this.joueur_proprietaire)
                        {
                            // prise
                            coupsPossibles.Add( new Prise(position, posColonne, posLigne, posColonne - 1, posLigne - 1));
                        }
                    }
                }
                if (posLigne > 0 && posColonne < 7)
                {
                    if (position.Case[posColonne + 1, posLigne - 1].occupé == false)
                    {
                        // deplacement 
                        coupsPossibles.Add( new Deplacement(position, posColonne, posLigne, posColonne + 1, posLigne - 1));
                    }
                    else
                    {
                        if (position.Case[posColonne + 1, posLigne - 1].joueur_proprietaire != this.joueur_proprietaire)
                        {
                            // prise
                            coupsPossibles.Add( new Prise(position, posColonne, posLigne, posColonne + 1, posLigne - 1));
                        }
                    }
                }
                if (posLigne < 7 && posColonne < 7)
                {
                    if (position.Case[posColonne + 1, posLigne + 1].occupé == false)
                    {
                        // deplacement 
                        coupsPossibles.Add( new Deplacement(position, posColonne, posLigne, posColonne + 1, posLigne + 1));
                    }
                    else
                    {
                        if (position.Case[posColonne + 1, posLigne + 1].joueur_proprietaire != this.joueur_proprietaire)
                        {
                            // prise
                            coupsPossibles.Add( new Prise(position, posColonne, posLigne, posColonne + 1, posLigne + 1));
                        }
                    }
                }
                if (posLigne < 7 && posColonne > 0)
                {
                    if (position.Case[posColonne - 1, posLigne + 1].occupé == false)
                    {
                        // deplacement 
                        coupsPossibles.Add( new Deplacement(position, posColonne, posLigne, posColonne - 1, posLigne + 1));
                    }
                    else
                    {
                        if (position.Case[posColonne - 1, posLigne + 1].joueur_proprietaire != this.joueur_proprietaire)
                        {
                            // prise
                            coupsPossibles.Add( new Prise(position, posColonne, posLigne, posColonne - 1, posLigne + 1));
                        }
                    }
                }
                if (posLigne < 7)
                {
                    if (position.Case[posColonne, posLigne + 1].occupé == false)
                    {
                        // deplacement 
                        coupsPossibles.Add( new Deplacement(position, posColonne, posLigne, posColonne + 0, posLigne + 1));
                    }
                    else
                    {
                        if (position.Case[posColonne, posLigne + 1].joueur_proprietaire != this.joueur_proprietaire)
                        {
                            // prise
                            coupsPossibles.Add( new Prise(position, posColonne, posLigne, posColonne + 0, posLigne + 1));
                        }
                    }
                }
                if (posColonne < 7)
                {
                    if (position.Case[posColonne + 1, posLigne].occupé == false)
                    {
                        // deplacement 
                        coupsPossibles.Add( new Deplacement(position, posColonne, posLigne, posColonne + 1, posLigne + 0));

                        if (this.aDéjaEtéDéplacé == false)
                        {
                            if (position.Case[posColonne + 2, posLigne].occupé == false)
                            {
                                if ((position.Case[posColonne + 3, posLigne].occupé == true) && (position.Case[posColonne + 3, posLigne].lettre == 'T'))
                                {
                                    // petit roque
                                    coupsPossibles.Add( new PetitRoque(position, posColonne, posLigne, posColonne + 2, posLigne + 0));
                                }
                            }
                        }

                    }
                    else
                    {
                        if (position.Case[posColonne + 1, posLigne].joueur_proprietaire != this.joueur_proprietaire)
                        {
                            // prise
                            coupsPossibles.Add( new Prise(position, posColonne, posLigne, posColonne + 1, posLigne + 0));
                        }
                    }
                }
                if (posColonne > 0)
                {
                    if (position.Case[posColonne - 1, posLigne].occupé == false)
                    {
                        // deplacement
                        coupsPossibles.Add( new Deplacement(position, posColonne, posLigne, posColonne - 1, posLigne + 0));

                        if (this.aDéjaEtéDéplacé == false)
                        {
                            if (position.Case[posColonne - 2, posLigne].occupé == false && position.Case[posColonne - 3, posLigne].occupé == false)
                            {
                                if ((position.Case[posColonne - 4, posLigne].occupé == true) && (position.Case[posColonne - 4, posLigne].lettre == 'T'))
                                {
                                    // grand roque
                                    coupsPossibles.Add( new GrandRoque(position, posColonne, posLigne, posColonne - 2, posLigne + 0));
                                }
                            }
                        }
                    }
                    else
                    {
                        if (position.Case[posColonne - 1, posLigne].joueur_proprietaire != this.joueur_proprietaire)
                        {
                            // prise
                            coupsPossibles.Add( new Prise(position, posColonne, posLigne, posColonne - 1, posLigne + 0));
                        }
                    }
                }
        }

        /// <summary>
        /// Clône du roi pour une position suivante</summary>
        /// <param name="deplacement"> Le roi vient-il de se déplacer ?</param>
        /// <returns>
        /// Une case de position </returns>
        override public Case positionSuivanteCase(Boolean deplacement=false)
        {
            
            if (deplacement == true || this.déplacé == true)
            {
                return new Roi(this.proprietaire,true);
            }
            else
            {
                return new Roi(this.proprietaire,false);
            }
            
        }


    }
}
