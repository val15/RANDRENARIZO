/*
 Auteur : val15
 Date De Creation : 18/02/16

Rôle :
Fichier principal du programme : elle reçoit en paramètre un fichier .txt
pour la ranger ou, dans le cas où il n'y a pas de
paramétré, elle va lister tout les fichiers .txt dans le répertoire actuel
pour pouvoir permettre à l'utilisateur d'en sélectionner un et de traiter ce dérnier

 */

#include <iostream>
#include <string>
#include <dirent.h>
#include "Element.h"
#include "Arbre.h"
#include "Chainne.h"
#include "Fichier.h"


using namespace std;


void questionerEtRanger(string nomFichier)
{
    //si le fichier à une extension on supprime son extension pour determiner son nom
        string extension=nomFichier.substr(nomFichier.find("."));
        nomFichier=nomFichier.substr(0,nomFichier.find("."));

        cout << "Voulez-vous renommer le fichier qui vas contenir les noms dans l'ordre en un autre nom que : " << nomFichier << "RangE" << extension <<" ? : " << "o/n" << endl;
        char g;
        cin >> g;
        string nouveauNom;

        if(g=='o')
        {
            cout << "nouveau nom : " << endl;
            cin >> nouveauNom;
            cout << "nom du fichier rangé "<<  nouveauNom+".txt"<< endl ;
        }
        if(g=='n')
        {
            cout << "nom du fichier rangé "<<  nomFichier+"RangE.txt"<< endl ;
            nouveauNom=nomFichier+"RangE";
        }

        Arbre a0;
        a0.lireUnFichier(nomFichier+extension);
        a0.parcourirEtVerifierEtRangerDansUnFichier(nouveauNom+extension);
}

int main(int argc, char *argv[])
{
    //integration d'un fichier en entrée
    if(argc==1)//dans le cas ou aucun fichier n'a été donné comme parametre
    {
        //lister le contenu un repertoire
        DIR* rep = opendir(".");
        vector <string> lstFichier;
        vector <string> lstFichierTxt;
        if (rep != NULL)
        {
            struct dirent* ent;//element actuel

            while ((ent = readdir(rep)) != NULL)
                lstFichier.push_back(ent->d_name);
            closedir(rep);
        }
        for(int c=0;c<(int)lstFichier.size();c++)
        {
            string nom;
              nom=lstFichier.at(c);
            if(nom.find(".txt")!=string::npos)//on ne prend que les fichiers qui ont une extension .txt
                lstFichierTxt.push_back(nom);
        }
         if(lstFichierTxt.size()>0)//si il y des fichiers .txt
         {
            int n;
            do{
                cout << "liste de touts les fichiers dans le répertoire actuel : " << endl;
                for(int c=0;c<(int)lstFichierTxt.size();c++)
                {
                    cout << c << ":\t" << lstFichierTxt.at(c) << endl;
                }
                cout << "\n"<<"choisisez un fichier de "<< 0 <<"-" << lstFichierTxt.size()-1 << endl;

                cin >> n;
                if(!((n>=0) && (n<(int)lstFichierTxt.size())))
                {
                    cout<< "mauvais chois" << endl;
                }
            }
            while(!((n>=0) && (n<(int)lstFichierTxt.size())));

            string nomFichier(lstFichierTxt.at(n));
            cout << "fichier choisi : " <<nomFichier<< endl;

            questionerEtRanger(nomFichier);
        }
        else//si il n'y a aucun fichier .txt dans le repertoir
            cout << "aucun fichier .txt detecer" << endl;
    }
    else if(argc>1)//dans le cas où il y a des paramétrés, on travaille directement sur ces derniers
    {
        for( int c=1;c<argc;c++)
            questionerEtRanger(argv[c]);
    }

    return 0;


}
