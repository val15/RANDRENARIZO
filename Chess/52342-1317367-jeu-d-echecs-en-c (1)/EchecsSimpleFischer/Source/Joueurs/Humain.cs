using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simpleFischer
{
    /// <summary>
    /// Joueur Humain </summary>
    /// <remarks>
    /// Le joueur humain joue par le biais de l'interface de la classe UserControl_echiquier
    /// </remarks>
    public class Humain : Joueur
    {
        /// <summary>
        /// Constructeur de la classe. </summary>
        /// <param name="nouvelle_partie"> Pointeur vers la partie à laquelle le joueur participe</param>
        public Humain(Partie nouvelle_partie) 
           :base (nouvelle_partie)
        {

        }

        /// <summary>
        /// Message de défaite du joueur humain par MAT</summary>
        override public String messageMat
        {
            get
            {
                return "ECHEC ET MAT! \n Hélas, l'ordinateur a été plus fort que vous.\n Vous ferez mieux la prochaine fois!";
            }
        }

        /// <summary>
        /// Message de défaite du joueur humain par chute du drapeau </summary>
        override public String messageChuteDrapeau
        {
            get
            {
                return "Chute du drapeau !\n Tout le temps de votre pendule est écoulé !\n La prochaine fois, tâchez de réfléchir moins longtemps !";
            }
        }

        /// <summary>
        /// Trait au joueur humain : activation du mode manuel de l'échiquier</summary>
        override public void trait()
        {
            partie.activerCoupsEchiquier=true;
        }

    }
}
