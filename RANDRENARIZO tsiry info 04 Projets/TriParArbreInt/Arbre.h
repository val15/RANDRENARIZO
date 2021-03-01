/*
 Auteur : val15
 Date De Creation : 18/02/16

Role :
pour la tri par arbre

 */


#ifndef ARBRE_H
#define ARBRE_H
//#include "Element.h"
#include "Element.h"
#include <vector>
#include "ObjetArbre.h"
#include <iostream>
#include <stdio.h>
#include <math.h>
using namespace std;

class Arbre
{
public :
	Arbre();
	void ajouterValeur(int v);//cette ajoute une valeur ou tableau principal
	void initialisationDuTableuDesElement();
	void majDuTableuDesElement();
	int nbNiveau();
	int getNumDernierElement();


	 void affichage();

	void affichageDeToutLesElement();

	void parcourir();
	void parcourirEtVerifier();

    //Arbre operator=(Arbre const& arbreACopier);
    bool fifsExist(Element e);//verifie si l'elelnt a un fifs
    Element getFifs(Element e,int f);
    bool pereExist(Element e);
    Element getPere(Element e);
    bool frereExiste(Element e);//verifie si le frere existe
    Element getFrere(Element e);
    bool elementExist(int numElemnt);
    Element getElemnt(int num);
   // Element* getElemntP(int num);

    void echangerValeur(Element e1,Element e2);
    void verifierEchager(Element e/*,Element eDejaVerifie*/);
    void verifierEchagerFils(Element e);
    void verifierEchagerPere(Element e);



private:
    vector <int> m_T;//le tableau principal
    vector <int> m_Ttempo;//le tableau temporaire
    vector <Element> m_TE;//ce tableau servira à creer la l'argre
    vector <int> m_TOrdonnE;//ce tableau contien le valeur ordonnées
    int m_nbNiveau;
    int m_numDernierElement;




};



#endif //ARBRE_H
