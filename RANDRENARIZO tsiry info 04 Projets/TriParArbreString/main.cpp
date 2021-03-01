/*
 Auteur : val15
 Date De Creation : 18/02/16

Role :
fichier principal du progamme: elle reçoi en parametre un fichier .txt pour la ranger ou dans le cas ou il n'y a pas de
parametre, elle va lister tout les fichier .txt dans le repertoir actuel pour povoir permetre à l'utilisateur d'en selectionner
un et de traiter

 */

#include <iostream>
#include <string>
#include <dirent.h>
#include "Element.h"
#include "Arbre.h"
#include "Chainne.h"
#include "Fichier.h"


using namespace std;


void questionerEtRancer(string nomFichier)
{
    //si le fichier à une extension on suppime son extension pour le decler
        string extension=nomFichier.substr(nomFichier.find("."));
        nomFichier=nomFichier.substr(0,nomFichier.find("."));

        cout << "voulez vous renomer le deuxieme fichier en un autre non que : " << nomFichier << "rangE" << extension <<" ? : " << "o/n" << endl;
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
            cout << "nom du fichier rangé "<<  nomFichier+"rangE.txt"<< endl ;
            nouveauNom=nomFichier+"rangE";
        }

        Arbre a0;
        a0.lireUnFichier(nomFichier+extension);
        a0.parcourirEtVerifierEtRangerDansUnFichier(nouveauNom+extension);
}

int main(int argc, char *argv[])
{
    //integration d'un fichier en entrée
    if(argc==1)//dans le cas ou aucun fichier n'a été donnée comme parametre
    {
        //lister le contenu d'un repertoire
        DIR* rep = opendir(".");
        vector <string> lstFichier;
        vector <string> lstFichierTxt;
        if (rep != NULL)
        {
            struct dirent* ent;//element actuel

            while ((ent = readdir(rep)) != NULL)
            {
              //on ne prend que les fichiers qui ont une extension .txt
                lstFichier.push_back(ent->d_name);
            }
            closedir(rep);
        }
        for(int c=0;c<(int)lstFichier.size();c++)
        {
            string nom;
              nom=lstFichier.at(c);
            if(nom.find(".txt")!=string::npos)
                lstFichierTxt.push_back(nom);
        }
         if(lstFichierTxt.size()>0)//si il y des fichiers .txt
         {
            int n;
            do{
                cout << "liste de tout les fichier dans le repertoir actuel : " << endl;
                for(int c=0;c<(int)lstFichierTxt.size();c++)
                {
                    cout << c << ":\t" << lstFichierTxt.at(c) << endl;
                }
                cout << "\n"<<"choisisez un fichier "<< 0 <<"-" << lstFichierTxt.size()-1 << endl;

                cin >> n;
                if(!((n>=0) && (n<(int)lstFichierTxt.size())))
                {
                    cout<< "movaise chois" << endl;
                }
            }
            while(!((n>=0) && (n<(int)lstFichierTxt.size())));

            string nomFichier(lstFichierTxt.at(n));
            cout << "fichier choisi : " <<nomFichier<< endl;

            questionerEtRancer(nomFichier);
        }
        else
            cout << "aucun fichier .txt" << endl;
    }
    else if(argc>1)//dans le cas ou il y a des parametres, on travaille dirrectement sur ces dérniers
    {
        for( int c=1;c<argc;c++)
            questionerEtRancer(argv[c]);
    }


   /* Arbre a0;
    a0.lireUnFichier("mot20.txt");
    a0.affichage();
    a0.parcourirEtVerifierEtRanger();

    if(a0.rechercherUneChainneApartirDe(Chainne("rika"),0)!=-1)
       cout << "element trouver " << endl;*/
    //a0.rechercherElementIntrovable();

    //a0.parcourir();

    /*string str1("cc");
    string str2("cc");
    if(str1==str2)
        cout << "plus grand" << endl;*/
    return 0;


}
