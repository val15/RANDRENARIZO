using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleFischer
{
    /// <summary>
    /// Classe Abstraite des Joueurs</summary>
    /// <remarks>
    /// Les joueurs être d'une de ces classes héritées: Humain ou Ordinateur
    /// </remarks>
    abstract public class Joueur
    {
        /// <summary>
        /// Stocke la propriété name</summary>
        protected Partie partie;

        /// <summary>
        /// Constructeur de la classe. </summary>
        /// <param name="nouvelle_partie"> Pointeur vers la partie à laquelle le joueur participe</param>
        public Joueur(Partie nouvelle_partie)
        {
            partie = nouvelle_partie;
        }

        /// <summary>
        /// Message de défaite par chute de drapeau </summary>
        virtual public String messageChuteDrapeau
        {
            get
            {
                return "Chute du drapeau !";
            }
        }

        /// <summary>
        /// Message d'égalité par PAT </summary>
        public String messagePat
        {
            get
            {
                return "PAT! Le score est a égalité.\n L'ordinateur n'a pas réussi à vous vaincre.";
            }
        }

        /// <summary>
        /// Message de défaite par MAT </summary>
        virtual public String messageMat
        {
            get
            {
                return "ECHEC ET MAT!";
            }
        }

        /// <summary>
        /// Donne le trait à un joueur </summary>
        abstract public void trait();

    }
}
