#include "Ensemble.h"
#include "Arbre.h"
#include <iostream>
using namespace std;
int main()
{
    Ensemble *e;
    int nbrEnsblAProduire;
    int valElmnt, iNbrElmt, c, d;
    cout<<"Saisir le nombre d'ensemble a produire : ";
    cin>>nbrEnsblAProduire;
    e=new Ensemble[nbrEnsblAProduire];
    //On va enregistrer tous les elements de chaque ensemble
    for(c=0; c<nbrEnsblAProduire; c++){
        cout<<"Saisir le nombre d'elements de l'ensemble E"<<c<<" : ";
        cin>>iNbrElmt;
        for(d=0; d<iNbrElmt; d++){
            cout<<"x"<<d<<" de E"<<c<<" = ";
            cin>>valElmnt;
            e[c].ajouter(valElmnt);
        }
    }
    //Declaration de l'arbre qui va contenir tous les ensembles dont nous avons déclaré au dessus
    Arbre a0;
    //Ajout de tous les ensembles à produire dans l'arbre déjà déclaré
   for(c=0; c<nbrEnsblAProduire; c++){
       a0.ajouter(e[c]);
   }
    cout<<"On a donc les "<<nbrEnsblAProduire<<" ensembles a produire suivants : "<<endl;
    //Affichage de tous les ensembles à produire
    a0.afficher();
    cout<<"Le produit cartesien ";
    for(c=0; c<nbrEnsblAProduire; c++){
            if(c<(nbrEnsblAProduire-1)) cout<<"E"<<c<<" X ";
            else cout<<"E"<<c<<" ="<<endl;
    }
    //La fonction prosuire suivante, de la classe Arbre est la fonction la plus importante de notre algorithme car elle nous
    //engendre le procédure de résolution du problème de produit cartésien
    a0.produire();
    return 0;
}
