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
	void produireAvec(Ensemble e2);
	//void set(int i);
	int at(int i);
	int size();
protected:


private:
    //vecteur ou Tableau des Valeurs (éléments) de l'Ensemble
    deque <int> m_TVE;

};

#endif //ENSEMBLE_H
