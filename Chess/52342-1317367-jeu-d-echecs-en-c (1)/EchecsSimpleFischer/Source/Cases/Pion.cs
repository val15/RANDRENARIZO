using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace simpleFischer
{
    /// <summary>
    /// Pièce Pion.</summary>
    /// <remarks>
    /// Le Pion se déplace d'une case en avant, ou une case en diagonale pour les prises.
    /// Il peut également effectuer les coups suivants: Deplacement double, Prise en passant, Promotion.
    /// </remarks>
    public class Pion : Piece
    {
        /// <summary>
        /// Stocke la propriété name</summary>
        private Boolean coupDouble=false;

        /// <summary>
        /// Constructeur de la classe. </summary>
        /// <param name="proprietaire">Joueur de même couleur qui peut manipuler la pièce</param>
        public Pion(Joueur proprietaire)
            : base(proprietaire)
        {

        }

        /// <summary>
        /// Le pion a fait un déplacement double au coup précédent </summary>
        public Boolean vientDeFaireCoupDouble
        {
            get
            {
                return coupDouble;
            }
            set
            {
                coupDouble = value;
            }
        }

        /// <summary>
        /// Code de la pièce : Première lettre du nom  </summary>
        override public char lettre
        {
            get
            {
                return 'P';
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
            // les coups d'un pion sont orientés différement selon qu'il est blanc ou noir
            // ces coups peuvent être:
            // _ un déplacement
            // _ un déplacement double (seulement depuis la position de départ)
            // _ une prise (en diagonale)
            // _ une prise en passant (seulement sur un pion qui vient de faire déplacement double)
            // _ une promotion

            if (this.joueur_proprietaire == Partie.blancs)
            {

                if (posLigne < 7)
                {
                    if (position.Case[posColonne, posLigne + 1].occupé == false)
                    {
                        if (posLigne == 6)
                        {
                            // promotion
                            coupsPossibles.Add(new Promotion(position,posColonne,posLigne,posColonne+ 0,posLigne+ 1));
                        }
                        else
                        {
                            // déplacement simple
                            coupsPossibles.Add(new Deplacement(position,posColonne, posLigne,posColonne+ 0,posLigne+ 1));
                        }
                    }
                    if (posColonne < 7)
                    {
                        if (position.Case[posColonne + 1, posLigne + 1].occupé == true)
                        {
                            if (position.Case[posColonne + 1, posLigne + 1].joueur_proprietaire != this.joueur_proprietaire)
                            {
                                if (posLigne == 6)
                                {
                                    // promotion
                                    coupsPossibles.Add(new Promotion(position,posColonne,posLigne,posColonne+ 1,posLigne+ 1));
                                }
                                else
                                {
                                    // prise à droite
                                    coupsPossibles.Add(new Prise(position,posColonne, posLigne,posColonne+ 1,posLigne+ 1));
                                }
                            }
                        }
                    }
                    if (posColonne > 0)
                    {
                        if (position.Case[posColonne - 1, posLigne + 1].occupé == true)
                        {
                            if (position.Case[posColonne - 1, posLigne + 1].joueur_proprietaire != this.joueur_proprietaire)
                            {
                                if (posLigne == 6)
                                {
                                    // promotion
                                    coupsPossibles.Add(new Promotion(position,posColonne, posLigne,posColonne -1,posLigne+ 1));
                                }
                                else
                                {
                                    // prise à gauche
                                    coupsPossibles.Add(new Prise(position, posColonne, posLigne,posColonne -1,posLigne+ 1));
                                }
                            }
                        }
                    }

                }
                // déplacement double
                if (posLigne == 1)
                {
                    if (position.Case[posColonne, 2].occupé == false)
                    {
                        if (position.Case[posColonne, 3].occupé == false)
                        {
                            coupsPossibles.Add( new DeplacementDouble(position, posColonne, posLigne, posColonne, posLigne+2));
                        }
                    }
                }

                
                if (posLigne == 4)
                {
                    if (posColonne < 7)
                    {
                        if (position.Case[posColonne + 1, 4].occupé == true)
                        {
                            if (position.Case[posColonne + 1, 4].joueur_proprietaire != this.joueur_proprietaire)
                            {
                                if (position.Case[posColonne + 1, 4].lettre == 'P')
                                {
                                    Pion p = (Pion)position.Case[posColonne + 1, 4];
                                    if (p.vientDeFaireCoupDouble == true)
                                    {
                                        // prise en passant à gauche
                                        coupsPossibles.Add(new PriseEnPassant(position,posColonne, posLigne, posColonne+ 1,posLigne+ 1));
                                    }
                                }
                            }
                        }
                    }
                    if (posColonne >0 )
                    {
                        if (position.Case[posColonne - 1, 4].occupé == true)
                        {
                            if (position.Case[posColonne - 1, 4].joueur_proprietaire != this.joueur_proprietaire)
                            {
                                if (position.Case[posColonne - 1, 4].lettre == 'P')
                                {
                                    Pion p = (Pion)position.Case[posColonne - 1, 4];
                                    if (p.vientDeFaireCoupDouble == true)
                                    {
                                        // prise en passant à gauche
                                        coupsPossibles.Add(new PriseEnPassant(position,posColonne, posLigne, posColonne -1,posLigne+ 1));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (posLigne > 0)
                {
                    if (position.Case[posColonne, posLigne - 1].occupé == false)
                    {
                        if (posLigne == 1)
                        {
                            // promotion
                            coupsPossibles.Add(new Promotion(position,posColonne, posLigne, posColonne+ 0,posLigne -1));
                        }
                        else
                        {
                            // déplacement simple
                            coupsPossibles.Add(new Deplacement(position,posColonne,posLigne,posColonne+ 0,posLigne -1));
                        }
                    }
                    if (posColonne < 7)
                    {
                        if (position.Case[posColonne + 1, posLigne - 1].occupé == true)
                        {
                            if (position.Case[posColonne + 1, posLigne - 1].joueur_proprietaire != this.joueur_proprietaire)
                            {
                                if (posLigne == 1)
                                {
                                    // promotion
                                    coupsPossibles.Add(new Promotion(position,posColonne,posLigne,posColonne+ 1,posLigne -1));
                                }
                                else
                                {
                                    // prise à droite
                                    coupsPossibles.Add(new Prise(position, posColonne, posLigne,posColonne+ 1,posLigne -1));
                                }
                            }
                        }
                    }
                    if (posColonne > 0)
                    {
                        if (position.Case[posColonne - 1, posLigne -1].occupé == true)
                        {
                            if (position.Case[posColonne - 1, posLigne - 1].joueur_proprietaire != this.joueur_proprietaire)
                            {
                                if (posLigne == 1)
                                {
                                    // promotion
                                    coupsPossibles.Add(new Promotion(position, posColonne, posLigne, posColonne -1, posLigne -1));
                                }
                                else
                                {
                                    // prise à gauche
                                    coupsPossibles.Add(new Prise(position,posColonne, posLigne, posColonne -1, posLigne -1));
                                }
                            }
                        }
                    }

                }
                // déplacement double
                if (posLigne == 6)
                {
                    if (position.Case[posColonne, 4].occupé == false)
                    {
                        if (position.Case[posColonne, 5].occupé == false)
                        {
                            coupsPossibles.Add(new DeplacementDouble(position, posColonne, posLigne,posColonne+ 0,posLigne -2));
                        }
                    }
                }
                if (posLigne == 3)
                {
                    if (posColonne < 7)
                    {
                        if (position.Case[posColonne + 1, 3].occupé == true)
                        {
                            if (position.Case[posColonne + 1, 3].joueur_proprietaire != this.joueur_proprietaire)
                            {
                                if (position.Case[posColonne + 1, 3].lettre == 'P')
                                {
                                    Pion p = (Pion)position.Case[posColonne + 1, 3];
                                    if (p.vientDeFaireCoupDouble == true)
                                    {
                                        // prise en passant à gauche
                                        coupsPossibles.Add(new PriseEnPassant(position,posColonne,posLigne, posColonne+ 1,posLigne -1));
                                    }
                                }
                            }
                        }
                    }
                    if (posColonne > 0)
                    {
                        if (position.Case[posColonne - 1, 3].occupé == true)
                        {
                            if (position.Case[posColonne - 1, 3].joueur_proprietaire != this.joueur_proprietaire)
                            {
                                if (position.Case[posColonne - 1, 3].lettre == 'P')
                                {
                                    Pion p = (Pion)position.Case[posColonne - 1, 3];
                                    if (p.vientDeFaireCoupDouble == true)
                                    {
                                        // prise en passant à gauche
                                        coupsPossibles.Add(new PriseEnPassant(position, posColonne, posLigne,posColonne -1,posLigne -1));
                                    }
                                }
                            }
                        }
                    }
                }


            }

        }

        /// <summary>
        /// Clône du pion pour une position suivante</summary>
        /// <param name="deplacement"> Le pion vient-il de se déplacer ?</param>
        /// <returns>
        /// Une case de position </returns>
        override public Case positionSuivanteCase(Boolean deplacement = false)
        {
            // clone pour position suivante
            Pion posSuivanteCase = new Pion(this.proprietaire);

            return posSuivanteCase;

        }


    }
}
