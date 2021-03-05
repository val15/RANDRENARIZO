using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace simpleFischer
{
    /// <summary>
    /// Position de l'échiquier </summary>
    /// <remarks>
    /// La position est définie par la répartition des pièces sur l'échiquier.
    /// Elle détermine les coups possibles.
    /// Elle peut constituer un échec, un MAT ou un PAT.
    /// </remarks>
    public class Position
    {

        /// <summary>
        /// Tableau des cases (colonne, ligne) </summary>
        private  Case[,] tableauPositionCase;
                
        /// <summary>
        /// référence au joueur qui a le trait </summary>
        private Joueur joueurTrait;

        /// <summary>
        /// liste des coups permis au joueur pour cette position </summary>
        private ArrayList tableauCoupsPermis= null;

        /// <summary>
        /// Constructeur de la classe (position quelconque) </summary>
        /// <param name="s"> Emplacement de la description du paramètre de s</param>
        public Position(Joueur trait)
        {

            // initialiser les 64 cases de l'échiquier
            this.tableauPositionCase = new Case[8, 8];

            this.joueurTrait = trait;
        }

        /// <summary>
        /// Constructeur de la classe (position de départ de partie) </summary>
        public Position()
        {

            // initialiser les 64 cases de l'échiquier
            this.tableauPositionCase = new Case[8, 8];
            // trait aux blancs
            this.joueurTrait = Partie.blancs;
            //this.dernierCoupJoué = null;

            this.Case[0, 0] = new Tour(Partie.blancs);
            this.Case[1, 0] = new Cavalier(Partie.blancs);
            this.Case[2, 0] = new Fou(Partie.blancs);
            this.Case[3, 0] = new Dame(Partie.blancs);
            this.Case[4, 0] = new Roi(Partie.blancs);
            this.Case[5, 0] = new Fou(Partie.blancs);
            this.Case[6, 0] = new Cavalier(Partie.blancs);
            this.Case[7, 0] = new Tour(Partie.blancs);
            this.Case[0, 1] = new Pion(Partie.blancs);
            this.Case[1, 1] = new Pion(Partie.blancs);
            this.Case[2, 1] = new Pion(Partie.blancs);
            this.Case[3, 1] = new Pion(Partie.blancs);
            this.Case[4, 1] = new Pion(Partie.blancs);
            this.Case[5, 1] = new Pion(Partie.blancs);
            this.Case[6, 1] = new Pion(Partie.blancs);
            this.Case[7, 1] = new Pion(Partie.blancs);

            this.Case[0, 2] = new Case();
            this.Case[1, 2] = new Case();
            this.Case[2, 2] = new Case();
            this.Case[3, 2] = new Case();
            this.Case[4, 2] = new Case();
            this.Case[5, 2] = new Case();
            this.Case[6, 2] = new Case();
            this.Case[7, 2] = new Case();

            this.Case[0, 3] = new Case();
            this.Case[1, 3] = new Case();
            this.Case[2, 3] = new Case();
            this.Case[3, 3] = new Case();
            this.Case[4, 3] = new Case();
            this.Case[5, 3] = new Case();
            this.Case[6, 3] = new Case();
            this.Case[7, 3] = new Case();

            this.Case[0, 4] = new Case();
            this.Case[1, 4] = new Case();
            this.Case[2, 4] = new Case();
            this.Case[3, 4] = new Case();
            this.Case[4, 4] = new Case();
            this.Case[5, 4] = new Case();
            this.Case[6, 4] = new Case();
            this.Case[7, 4] = new Case();

            this.Case[0, 5] = new Case();
            this.Case[1, 5] = new Case();
            this.Case[2, 5] = new Case();
            this.Case[3, 5] = new Case();
            this.Case[4, 5] = new Case();
            this.Case[5, 5] = new Case();
            this.Case[6, 5] = new Case();
            this.Case[7, 5] = new Case();

            this.Case[0, 6] = new Pion(Partie.noirs);
            this.Case[1, 6] = new Pion(Partie.noirs);
            this.Case[2, 6] = new Pion(Partie.noirs);
            this.Case[3, 6] = new Pion(Partie.noirs);
            this.Case[4, 6] = new Pion(Partie.noirs);
            this.Case[5, 6] = new Pion(Partie.noirs);
            this.Case[6, 6] = new Pion(Partie.noirs);
            this.Case[7, 6] = new Pion(Partie.noirs);
            this.Case[0, 7] = new Tour(Partie.noirs);
            this.Case[1, 7] = new Cavalier(Partie.noirs);
            this.Case[2, 7] = new Fou(Partie.noirs);
            this.Case[3, 7] = new Dame(Partie.noirs);
            this.Case[4, 7] = new Roi(Partie.noirs);
            this.Case[5, 7] = new Fou(Partie.noirs);
            this.Case[6, 7] = new Cavalier(Partie.noirs);
            this.Case[7, 7] = new Tour(Partie.noirs);

        }



        /// <summary>
        /// Joueur qui dispose du trait (lecture seule) </summary>
        public Joueur trait
        {
            get
            {
                return joueurTrait;
            }
        }

        /// <summary>
        /// Tableau des cases de l'échiquier </summary>
        public Case[,] Case
        {
            get
            {
                return tableauPositionCase;
            }
        }

        /// <summary>
        /// Liste des coups permis par la position (lecture seule) </summary>
        public ArrayList coupsPermis
        {
            get
            {
                if (this.tableauCoupsPermis == null)
                {
                    this.tableauCoupsPermis = new ArrayList();
                    int x = 0;
                    int y = 0;
                    for (x = 0; x < 8; x++)
                    {
                        for (y = 0; y < 8; y++)
                        {
                            // la case est elle occupée par une pièce ?
                            if (this.Case[x, y].occupé == true)
                            {
                                // la pièce appartient elle au joueur qui a le trait ?
                                if (this.Case[x, y].joueur_proprietaire == this.trait)
                                {
                                    // ajouter les coups de la pièce aux coups permis
                                    this.Case[x, y].coups(this, this.coupsPermis, x, y);
                                }
                            }
                        }
                    }
                }
                return this.tableauCoupsPermis;
                
            }
        }

        /// <summary>
        /// Liste des coups permis formellement légaux (lecture seule) </summary>
        public ArrayList coupsPermisLégaux
        {
            get
            {
                ArrayList tableauCoupsPermisLégaux=new ArrayList();
                foreach (Coup c in this.coupsPermis)
                {
                    if (c.légal == true)
                    {
                        tableauCoupsPermisLégaux.Add(c);
                    }
                }
                return tableauCoupsPermisLégaux;
            }
        }

        /// <summary>
        /// La légalité de la position (lecture seule) </summary>
        public Boolean légale
        {            
            get
            {
                 foreach (Coup c in this.coupsPermis)
                 {
                    if (c.régicide == true)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Le roi est il en échec ? (lecture seule) </summary>
        public Boolean échec
        {
            get
            {
                    // on teste s'il y a échec immédiat
                    Position posTest = this.positionSuivante(null);
                    // simuler les coups comme si on passait son tour sans jouer

                    if (posTest.légale == true)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
            }
        }

        /// <summary>
        /// Y a t-il échec et MAT ? (lecture seule) </summary>
        public Boolean MAT
        {
            get
            {
                if (this.coupsPermisLégaux.Count == 0 && this.échec == true)
                    {
                       return true;
                    }
                    else
                    {
                       return false;
                    }
            }
        }

        /// <summary>
        /// Y a t-il PAT ? (lecture seule) </summary>
        public Boolean PAT
        {
            get
            {

                if (this.coupsPermisLégaux.Count == 0 && this.échec == false)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
        

            }
        }

        /// <summary>
        /// Créer une position clône pour le coup suivant</summary>
        /// <param name="coup">Référence au coup suivant </param>
        /// <returns>
        /// La position clône</returns>
        public Position positionSuivante(Coup coup)
        {
            Position posSuivante;
            
            // inverser le trait
            if (this.trait == Partie.blancs)
            {
                posSuivante = new Position(Partie.noirs);
            }
            else
            {
                posSuivante = new Position(Partie.blancs);
            }

            // créer les cases de la nouvelle position
            int x;
            int y;
            for (x = 0; x < 8; x++)
            {
                for (y = 0; y < 8; y++)
                {   
                    // le clone de la case peut être sensiblement différent
                    // ex: pion qui pouvait être pris en passant et ne le peut plus
                    posSuivante.Case[x, y]=this.Case[x, y].positionSuivanteCase(false);
                }
            }

            return posSuivante;

        }


    }
}
