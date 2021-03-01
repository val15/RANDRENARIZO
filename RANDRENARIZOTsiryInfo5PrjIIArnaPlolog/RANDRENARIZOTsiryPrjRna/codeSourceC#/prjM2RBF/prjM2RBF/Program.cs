using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjM2RBF
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("TESTE AVEC LES PARAMETRE DEJA DEFINIES :");
              ReseauRadiale rr = new ReseauRadiale(3, 4, 2); 

              rr.CentreDeGravitEe.setE(0, 0, -3.0);
              rr.CentreDeGravitEe.setE(0, 1, -3.5);
              rr.CentreDeGravitEe.setE(0, 2, -3.8);

              rr.CentreDeGravitEe.setE(1, 0, -1.0);
              rr.CentreDeGravitEe.setE(1, 1, -1.5);
              rr.CentreDeGravitEe.setE(1, 2, -1.8);

              rr.CentreDeGravitEe.setE(2, 0, 2.0);
              rr.CentreDeGravitEe.setE(2, 1, 2.5);
              rr.CentreDeGravitEe.setE(2, 2, 2.8);

              rr.CentreDeGravitEe.setE(3, 0, 4.0);
              rr.CentreDeGravitEe.setE(3, 1, 4.5);
              rr.CentreDeGravitEe.setE(3, 2, 4.8);

            rr.EcartsTypes.setE(0, 2.22);
            rr.EcartsTypes.setE(1, 3.33);
            rr.EcartsTypes.setE(2, 4.44);
            rr.EcartsTypes.setE(3, 5.55);



            rr.Poids.setE(0, 0, 5.0);
            rr.Poids.setE(0, 1, -5.1);

            rr.Poids.setE(1, 0, -5.2);
            rr.Poids.setE(1, 1, 5.3);

            rr.Poids.setE(2, 0, -5.4);
            rr.Poids.setE(2, 1, 5.5);

            rr.Poids.setE(3, 0, 5.6);
            rr.Poids.setE(3, 1, -5.7);


             rr.Biais.setE(0, 7.0);
            rr.Biais.setE(1, 7.1);



            rr.EntrEes.setE(0, 1.0);
            rr.EntrEes.setE(1, -2.0);
            rr.EntrEes.setE(2, 3.0);



            rr.traiter();
            rr.afficherInfo();


            //TESTE AVEC DES DONNEES BRUTES
            /*
             *  Le programme crée un modèle RBF qui prédit l'espèce d'une fleur 
             *  d'iris ("setosa," "versicolor" ou « virginica ») de quatre valeurs numériques 
             *  pour la longueur des sépales et la largeur et la pétale longueur et la largeur de la fleur.
             *  La source de données du programme de la démo comprend 30 articles qui constituent un
             *  sous-ensemble d'un ensemble de point de repère bien connu 150-point appelé données
             *  Iris de Fisher. Les éléments de 30 données ont été prétraités. Les quatre x-valeurs 
             *  numériques ont été normalisés afin que les valeurs inférieures à zéro la longueur 
             *  plus courte que la moyenne moyenne ou la largeur et une valeur supérieure à zéro 
             *  signifie plus-que-moyenne longueur ou la largeur. La valeur de y pour les espèces a 
             *  été encodée comme (0,0,1), (0,1,0), ou (1,0,0) pour setosa, versicolor et virginica, 
             *  respectivement.
             */
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("TESTE AVEC DES DONNEES BRUTES:");

            Matrice matriceDeDonnEes = new Matrice(30, 7);

            //0 0 1
            matriceDeDonnEes.setE(0, 0, -0.784);// la longueur des sépales
            matriceDeDonnEes.setE(0, 1, 1.255);//largeur du  pétale
            matriceDeDonnEes.setE(0, 2, -1.332);//longeur du pétale
            matriceDeDonnEes.setE(0, 3, -1.306);//largeur du fleur.
            //type du fleure                                    
            matriceDeDonnEes.setE(0, 4, 0);
            matriceDeDonnEes.setE(0, 5, 0);
            matriceDeDonnEes.setE(0, 6, 1);

            matriceDeDonnEes.setE(1, 0, -0.995);
            matriceDeDonnEes.setE(1, 1, -0.109);
            matriceDeDonnEes.setE(1, 2, -1.332);
            matriceDeDonnEes.setE(1, 3, -1.306);
            matriceDeDonnEes.setE(1, 4, 0);
            matriceDeDonnEes.setE(1, 5, 0);
            matriceDeDonnEes.setE(1, 6, 1);

            matriceDeDonnEes.setE(2, 0, -1.206);
            matriceDeDonnEes.setE(2, 1, 0.436);
            matriceDeDonnEes.setE(2, 2, -1.386);
            matriceDeDonnEes.setE(2, 3, -1.306);
            matriceDeDonnEes.setE(2, 4, 0);
            matriceDeDonnEes.setE(2, 5, 0);
            matriceDeDonnEes.setE(2, 6, 1);

            matriceDeDonnEes.setE(3, 0, -1.312);
            matriceDeDonnEes.setE(3, 1, 0.164);
            matriceDeDonnEes.setE(3, 2, -1.278);
            matriceDeDonnEes.setE(3, 3, -1.306);
            matriceDeDonnEes.setE(3, 4, 0);
            matriceDeDonnEes.setE(3, 5, 0);
            matriceDeDonnEes.setE(3, 6, 1);

            matriceDeDonnEes.setE(4, 0, -0.890);
            matriceDeDonnEes.setE(4, 1, 1.528);
            matriceDeDonnEes.setE(4, 2, -1.332);
            matriceDeDonnEes.setE(4, 3, -1.306);
            matriceDeDonnEes.setE(4, 4, 0);
            matriceDeDonnEes.setE(4, 5, 0);
            matriceDeDonnEes.setE(4, 6, 1);

            matriceDeDonnEes.setE(5, 0, -0.468);
            matriceDeDonnEes.setE(5, 1, 2.346);
            matriceDeDonnEes.setE(5, 2, -1.170);
            matriceDeDonnEes.setE(5, 3, -1.048);
            matriceDeDonnEes.setE(5, 4, 0);
            matriceDeDonnEes.setE(5, 5, 0);
            matriceDeDonnEes.setE(5, 6, 1);

            matriceDeDonnEes.setE(6, 0, -1.312);
            matriceDeDonnEes.setE(6, 1, 0.982);
            matriceDeDonnEes.setE(6, 2, -1.332);
            matriceDeDonnEes.setE(6, 3, -1.177);
            matriceDeDonnEes.setE(6, 4, 0);
            matriceDeDonnEes.setE(6, 5, 0);
            matriceDeDonnEes.setE(6, 6, 1);

            matriceDeDonnEes.setE(7, 0, -0.890);
            matriceDeDonnEes.setE(7, 1, 0.982);
            matriceDeDonnEes.setE(7, 2, -1.278);
            matriceDeDonnEes.setE(7, 3, -1.306);
            matriceDeDonnEes.setE(7, 4, 0);
            matriceDeDonnEes.setE(7, 5, 0);
            matriceDeDonnEes.setE(7, 6, 1);

            matriceDeDonnEes.setE(8, 0, -1.523);
            matriceDeDonnEes.setE(8, 1, -0.382);
            matriceDeDonnEes.setE(8, 2, -1.332);
            matriceDeDonnEes.setE(8, 3, -1.306);
            matriceDeDonnEes.setE(8, 4, 0);
            matriceDeDonnEes.setE(8, 5, 0);
            matriceDeDonnEes.setE(8, 6, 1);

            matriceDeDonnEes.setE(9, 0, -0.995);
            matriceDeDonnEes.setE(9, 1, 0.164);
            matriceDeDonnEes.setE(9, 2, -1.278);
            matriceDeDonnEes.setE(9, 3, -1.435);
            matriceDeDonnEes.setE(9, 4, 0);
            matriceDeDonnEes.setE(9, 5, 0);
            matriceDeDonnEes.setE(9, 6, 1);


            //0 1 0
            matriceDeDonnEes.setE(10, 0, 1.220);
            matriceDeDonnEes.setE(10, 1, 0.436);
            matriceDeDonnEes.setE(10, 2, 0.452);
            matriceDeDonnEes.setE(10, 3, 0.241);
            matriceDeDonnEes.setE(10, 4, 0);
            matriceDeDonnEes.setE(10, 5, 1);
            matriceDeDonnEes.setE(10, 6, 0);

            matriceDeDonnEes.setE(11, 0, 0.587);
            matriceDeDonnEes.setE(11, 1, 0.436);
            matriceDeDonnEes.setE(11, 2, 0.344);
            matriceDeDonnEes.setE(11, 3, 0.370);
            matriceDeDonnEes.setE(11, 4, 0);
            matriceDeDonnEes.setE(11, 5, 1);
            matriceDeDonnEes.setE(11, 6, 0);

            matriceDeDonnEes.setE(12, 0, 1.115);
            matriceDeDonnEes.setE(12, 1, 0.164);
            matriceDeDonnEes.setE(12, 2, 0.560);
            matriceDeDonnEes.setE(12, 3, 0.370);
            matriceDeDonnEes.setE(12, 4, 0);
            matriceDeDonnEes.setE(12, 5, 1);
            matriceDeDonnEes.setE(12, 6, 0);

            matriceDeDonnEes.setE(13, 0, -0.362);
            matriceDeDonnEes.setE(13, 1, -2.019);
            matriceDeDonnEes.setE(13, 2, 0.074);
            matriceDeDonnEes.setE(13, 3, 0.112);
            matriceDeDonnEes.setE(13, 4, 0);
            matriceDeDonnEes.setE(13, 5, 1);
            matriceDeDonnEes.setE(13, 6, 0);

            matriceDeDonnEes.setE(14, 0, 0.693);
            matriceDeDonnEes.setE(14, 1, -0.655);
            matriceDeDonnEes.setE(14, 2, 0.398);
            matriceDeDonnEes.setE(14, 3, 0.370);
            matriceDeDonnEes.setE(14, 4, 0);
            matriceDeDonnEes.setE(14, 5, 1);
            matriceDeDonnEes.setE(14, 6, 0);

            matriceDeDonnEes.setE(15, 0, -0.151);
            matriceDeDonnEes.setE(15, 1, -0.655);
            matriceDeDonnEes.setE(15, 2, 0.344);
            matriceDeDonnEes.setE(15, 3, 0.112);
            matriceDeDonnEes.setE(15, 4, 0);
            matriceDeDonnEes.setE(15, 5, 1);
            matriceDeDonnEes.setE(15, 6, 0);

            matriceDeDonnEes.setE(16, 0, 0.482);
            matriceDeDonnEes.setE(16, 1, 0.709);
            matriceDeDonnEes.setE(16, 2, 0.452);
            matriceDeDonnEes.setE(16, 3, 0.498);
            matriceDeDonnEes.setE(16, 4, 0);
            matriceDeDonnEes.setE(16, 5, 1);
            matriceDeDonnEes.setE(16, 6, 0);

            matriceDeDonnEes.setE(17, 0, -0.995);
            matriceDeDonnEes.setE(17, 1, -1.746);
            matriceDeDonnEes.setE(17, 2, -0.305);
            matriceDeDonnEes.setE(17, 3, -0.275);
            matriceDeDonnEes.setE(17, 4, 0);
            matriceDeDonnEes.setE(17, 5, 1);
            matriceDeDonnEes.setE(17, 6, 0);

            matriceDeDonnEes.setE(18, 0, 0.798);
            matriceDeDonnEes.setE(18, 1, -0.382);
            matriceDeDonnEes.setE(18, 2, 0.398);
            matriceDeDonnEes.setE(18, 3, 0.112);
            matriceDeDonnEes.setE(18, 4, 0);
            matriceDeDonnEes.setE(18, 5, 1);
            matriceDeDonnEes.setE(18, 6, 0);

            matriceDeDonnEes.setE(19, 0, -0.679);
            matriceDeDonnEes.setE(19, 1, -0.927);
            matriceDeDonnEes.setE(19, 2, 0.020);
            matriceDeDonnEes.setE(19, 3, 0.241);
            matriceDeDonnEes.setE(19, 4, 0);
            matriceDeDonnEes.setE(19, 5, 1);
            matriceDeDonnEes.setE(19, 6, 0);


            //1 0 0
            matriceDeDonnEes.setE(20, 0, 0.482);
            matriceDeDonnEes.setE(20, 1, 0.709);
            matriceDeDonnEes.setE(20, 2, 1.155);
            matriceDeDonnEes.setE(20, 3, 1.659);
            matriceDeDonnEes.setE(20, 4, 1);
            matriceDeDonnEes.setE(20, 5, 0);
            matriceDeDonnEes.setE(20, 6, 0);

            matriceDeDonnEes.setE(21, 0, -0.046);
            matriceDeDonnEes.setE(21, 1, -0.927);
            matriceDeDonnEes.setE(21, 2, 0.669);
            matriceDeDonnEes.setE(21, 3, 0.885);
            matriceDeDonnEes.setE(21, 4, 1);
            matriceDeDonnEes.setE(21, 5, 0);
            matriceDeDonnEes.setE(21, 6, 0);

            matriceDeDonnEes.setE(22, 0, 1.326);
            matriceDeDonnEes.setE(22, 1, -0.109);
            matriceDeDonnEes.setE(22, 2, 1.101);
            matriceDeDonnEes.setE(22, 3, 1.143);
            matriceDeDonnEes.setE(22, 4, 1);
            matriceDeDonnEes.setE(22, 5, 0);
            matriceDeDonnEes.setE(22, 6, 0);

            matriceDeDonnEes.setE(23, 0, 0.482);
            matriceDeDonnEes.setE(23, 1, -0.382);
            matriceDeDonnEes.setE(23, 2, 0.939);
            matriceDeDonnEes.setE(23, 3, 0.756);
            matriceDeDonnEes.setE(23, 4, 1);
            matriceDeDonnEes.setE(23, 5, 0);
            matriceDeDonnEes.setE(23, 6, 0);

            matriceDeDonnEes.setE(24, 0, 0.693);
            matriceDeDonnEes.setE(24, 1, -0.109);
            matriceDeDonnEes.setE(24, 2, 1.047);
            matriceDeDonnEes.setE(24, 3, 1.272);
            matriceDeDonnEes.setE(24, 4, 1);
            matriceDeDonnEes.setE(24, 5, 0);
            matriceDeDonnEes.setE(24, 6, 0);

            matriceDeDonnEes.setE(25, 0, 1.853);
            matriceDeDonnEes.setE(25, 1, -0.109);
            matriceDeDonnEes.setE(25, 2, 1.479);
            matriceDeDonnEes.setE(25, 3, 1.143);
            matriceDeDonnEes.setE(25, 4, 1);
            matriceDeDonnEes.setE(25, 5, 0);
            matriceDeDonnEes.setE(25, 6, 0);

            matriceDeDonnEes.setE(26, 0, -0.995);
            matriceDeDonnEes.setE(26, 1, -1.473);
            matriceDeDonnEes.setE(26, 2, 0.344);
            matriceDeDonnEes.setE(26, 3, 0.627);
            matriceDeDonnEes.setE(26, 4, 1);
            matriceDeDonnEes.setE(26, 5, 0);
            matriceDeDonnEes.setE(26, 6, 0);

            matriceDeDonnEes.setE(27, 0, 1.537);
            matriceDeDonnEes.setE(27, 1, -0.382);
            matriceDeDonnEes.setE(27, 2, 1.317);
            matriceDeDonnEes.setE(27, 3, 0.756);
            matriceDeDonnEes.setE(27, 4, 1);
            matriceDeDonnEes.setE(27, 5, 0);
            matriceDeDonnEes.setE(27, 6, 0);

            matriceDeDonnEes.setE(28, 0, 0.904);
            matriceDeDonnEes.setE(28, 1, -1.473);
            matriceDeDonnEes.setE(28, 2, 1.047);
            matriceDeDonnEes.setE(28, 3, 0.756);
            matriceDeDonnEes.setE(28, 4, 1);
            matriceDeDonnEes.setE(28, 5, 0);
            matriceDeDonnEes.setE(28, 6, 0);

            matriceDeDonnEes.setE(29, 0, 1.431);
            matriceDeDonnEes.setE(29, 1, 1.528);
            matriceDeDonnEes.setE(29, 2, 1.209);
            matriceDeDonnEes.setE(29, 3, 1.659);
            matriceDeDonnEes.setE(29, 4, 1);
            matriceDeDonnEes.setE(29, 5, 0);
            matriceDeDonnEes.setE(29, 6, 0);

            //matriceDeDonnEes.afficher();


            ReseauRadiale rrDonnE = new ReseauRadiale(4, 5, 3);
            rrDonnE.creerDonneesDEntraitemenEtTest(matriceDeDonnEes, 8);
            rrDonnE.entrainerLeReseau();
            rrDonnE.afficherInfo();

            Console.ReadLine();




        }
    }
}
