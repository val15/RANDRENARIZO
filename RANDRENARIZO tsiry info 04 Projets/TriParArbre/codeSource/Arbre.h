/*
 Auteur : val15
 Date De Creation : 18/02/16

Role :
pour le tri par arbre, cet élément représente l'arbre, on effectue de dans tout les opérations de trie et de rangement

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
	void lireUnFichier(string nomFIchier);
	void lireUnFichierEtRancgerDansUnFichierDontLeNomEstRaneE(string nomFIchier);
	void ajouterValeur(string v);//ajoute un valeur au tableau principal; elle reçoit un string comme parametre
	void initialisationDuTableuDesElement();

	int nbNiveau();
	int getNumDernierElement();


	 void affichage();//affiche l'arbre
	void affichageDeToutLesElement();//affiche tout les element de l'arbre

	void parcourir();//sert à parcourir et à afficher chaque element contenu dans l'arbre
	void parcourirEtVerifierEtRanger();//sert à parcourir, a verifier et à ranger l'arbre
	void parcourirEtVerifierEtRangerDansUnFichier(string fichier);//sert à parcourir, a vérifier et à ranger l'arbre et à la fin écrit le résultat dans un fichier

    bool fifsExist(Element e);//verifie si l'elelement a un fifs
    Element getFifs(Element e,int f);//retourne l'element fils
    bool pereExist(Element e);//verifie si l'element a un pere
    Element getPere(Element e);//retounre le pere
    bool frereExiste(Element e);//verifie si l'element a un frere
    Element getFrere(Element e);//retourne le frere
    bool elementExist(int numElemnt);//verifie si l'element existe
    Element getElemnt(int num);//commence par 1

    void echangerValeur(Element e1,Element e2);
    void verifierEtChangmentBoublan();//cette fonction sert à déterminer le doublan et remplace le deuxiemme element doublan par l'element qui a été retirer de l'arbre
    void verifierEchager(Element e);//cette fonction verifie et echange la valeur de l'élement mais aussi celle de son pere et de sons fils
    void verifierEchagerFilsRacine(Element e);//verifie et change la valeur de fils
    void verifierEchagerPere(Element e);//verifie et change la valeur de pere
    int rechercherElementIntrovable();//retourne l'indice de l'element introuvable, ou -1 si il n'y a aucin element introvable
    int rechercherUneChaine(Chaine ch);//retourne -1 si l'element n'est pas trouver
    int rechercherChaineNbFois(Chaine ch);//retourne le dernier indice du doublan
    int rechercherUneChaineApartirDe(Chaine ch,int indiceDebut);
    int rechercherDoublant();//retourne de dernier indice de l'element doublant



private:
    vector <Chaine> m_T;//le tableau principal
    vector <Chaine> m_Ttempo;//le tableau temporaire
    vector <Element> m_TE;//ce tableau servira à creer la l'arbre
    vector <Chaine> m_TOrdonnE;//ce tableau contien le valeur ordonnées
    int m_nbNiveau;
    int m_numDernierElement;




};



#endif //ARBRE_H
