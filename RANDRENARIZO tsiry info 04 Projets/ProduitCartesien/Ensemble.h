/*
 Auteur : val15
 Date De Creation : 10/03/16

Role :
represente une Ensemble

 */


#ifndef ENSEMBLE_H
#define ENSEMBLE_H
#include <deque>
#include <iostream>
#include <stdio.h>

using namespace std;

class Ensemble
{
public :
	Ensemble();
	~Ensemble();
	void ajouter(int e);
	void ajouterAuDebut(int e);
	void afficher();
    void afficherProduitEntreEux();

	void produireAvec(Ensemble e2);
	//void set(int i);
	int at(int i);
	int size();
protected:


private:
    deque <int> m_TE;

};

#endif //ENSEMBLE_H
