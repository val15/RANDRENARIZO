using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace simpleFischer
{
    /// <summary>
    /// Joueur Ordinateur </summary>
    /// <remarks>
    /// Le joueur Ordinateur choisit son coup en interrogeant un processus asynchrone UCI 
    /// </remarks>
    public class Ordinateur : Joueur
    {
        /// <summary>
        /// pointeur vers le processus UCI </summary>
        private Process proc;

        /// <summary>
        /// état du processus : Prêt ou non </summary>
        private Boolean procReady = false;

        /// <summary>
        /// Constructeur de la classe. </summary>
        /// <param name="nouvelle_partie"> Pointeur vers la partie à laquelle le joueur participe</param>
        public Ordinateur(Partie nouvelle_partie) 
           : base (nouvelle_partie)
        {
                // chemin du moteur UCI 
                string tempFile = System.IO.Path.Combine(Application.StartupPath, "stockfish.exe");

                if (!File.Exists(tempFile))
                {
                    // extraire le moteur UCI stocké en ressource 
                    File.WriteAllBytes(tempFile, simpleFischer.Properties.Resources.stockfish);
                }

                proc = new Process();
                //  paramétrage de StartInfo
                proc.StartInfo.FileName = tempFile;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                // gestionnaire d'évenement de sortie de données
                proc.OutputDataReceived += new DataReceivedEventHandler(myProc_OutputDataReceived);
                // démarrer le processus
                proc.Start();
                // commencer à lire les sorties de données
                proc.BeginOutputReadLine();
                // premiere interrogation du processus: le UCI est il pret ? 
                proc.StandardInput.Write("isready\n");
        }

        /// <summary>
        /// Destructeur de classe. </summary>
        ~Ordinateur()
        {
            proc.Close();
        }

        /// <summary>
        /// Message de défaite de l'ordinateur par MAT </summary>
        override public String messageMat
        {
            get
            {
                return "ECHEC ET MAT! BRAVO!\n Vous avez terrassé l'ordinateur!\n L'être humain montre sa supériorité sur la machine!";
            }
        }

        /// <summary>
        /// Message de défaite de l'ordinateur par chute de drapeau </summary>
        override public String messageChuteDrapeau
        {
            get
            {
                return "Chute du drapeau !\n L'ordinateur a écoulé tout son temps !\n Bravo, vous êtes parvenu à le déstabiliser.";
            }
        }

        /// <summary>
        /// Evennement de données de sorties du processus </summary>
        /// <param name="e"> Conteneur des données </param>
        void myProc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {

            if (!String.IsNullOrEmpty(e.Data))
            {
                // extraire les mots de l'instruction
                String[] dataTableau = e.Data.Split(' '); 

                // identifier le premier mot 
                switch (dataTableau[0])
                {
                    case "readyok": // le moteur UCI est pret à jouer
                        procReady = true;
                        proc.StandardInput.Write("position startpos\n");
                        break;
                    case "bestmove": // le moteur UCI propose un meilleur coup
                        foreach (Coup c in partie.position.coupsPermis)
                        {  
                            if (c.notationAlgebriqueUCI.Substring(0,4) == dataTableau[1].Substring(0,4))
                            {
                                proc.StandardInput.Write("position moves " + c.notationAlgebriqueUCI + "\n");
                                // le joueur ordinateur jour le coup proposé
                                partie.jouerCoup(c);                                
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Donne le trait au joueur Ordinateur</summary>
        override public void trait()
        {
            // désactiver le mode manuel de l'échiquier
            partie.activerCoupsEchiquier = false;

            while (procReady != true)
            {
                // attendre que le moteur UCI soit initialisé
                Application.DoEvents();
            }

           if (partie.dernierCoup != null)
           {
                // informer le moteur UCI du coup joué par l'adversaire
                proc.StandardInput.Write("position moves " + partie.dernierCoup.notationAlgebriqueUCI + "\n");
           }
           // calcul du meilleur coup
           proc.StandardInput.Write("go movetime 3000\n");
            
        }



        
    }
}
