/*
 Auteur : val15
 Date De Creation : 18/02/16

Role :
pour le tri par arbre, cette elemnt represente l'argre, on effectue de dans tout les operation de trie et de rangement

 */


#ifndef ARBRE_H
#define ARBRE_H
#include "Element.h"
#include <vector>
#include <iostream>
#include <stdio.h>
#include <math.h>
#include "Chaine.h"
#include "Fichier.h"
using namespace std;

class Arbre
{
public :
	Arbre();
	~Arbre();
	void lireUnFichier(string nomFIchier);
	void lireUnFichierEtRancgerDansUnAutre(string nomFIchier);
	void ajouterValeur(string v);//cette ajoute une valeur au tableau principal; elle reçoi un string comme parametre
	void initialisationDuTableuDesElement();

	int nbNiveau();
	int getNumDernierElement();


	 void affichage();//affiche l'arbre
	void affichageDeToutLesElement();//affiche tout les elemnt de l'arbre

	void parcourir();//sert à parcourir et à afficher chaque element contenu dans l'arbre
	void parcourirEtVerifierEtRanger();//sert à parcourir, a verifier et à ranger l'arbre
	void parcourirEtVerifierEtRangerDansUnFichier(string fichier);//sert à parcourir, a verifier et à ranger l'arbre et à la fin ecri le resultat dans un fichier

    bool fifsExist(Element* e);//verifie si l'elelement a un fifs
    Element* getFifs(Element* e,int f);//retourne l'ement fils
    bool pereExist(Element* e);//verifie si l'element a un pere
    Element* getPere(Element* e);//retounre le pere
    bool frereExiste(Element* e);//verifie si le frere existe
    Element* getFrere(Element* e);//retourne le frere
    bool elementExist(int numElemnt);//verifie si l'element existe
    Element* getElemnt(int num);//commence par 1

    void echangerValeur(Element* e1,Element* e2);
    void verifierEchager(Element* e);//cette fonction verifie et echange la valeur de l'élement mais aussi cette de son pere et de sons fils
    void verifierEchagerFilsRacine(Element* e);//verifie et change la valeur de fils
    void verifierEchagerPere(Element* e);//verifie et change la valeur de pere



private:
    vector <Chaine*> *m_T;//le tableau principal
    vector <Chaine*> *m_Ttempo;//le tableau temporaire
    vector <Element*> *m_TE;//ce tableau servira à creer la l'arbre
    vector <Chaine*> *m_TOrdonnE;//ce tableau contien le valeur ordonnées
    int m_nbNiveau;
    int m_numDernierElement;




};



#endif //ARBRE_H
