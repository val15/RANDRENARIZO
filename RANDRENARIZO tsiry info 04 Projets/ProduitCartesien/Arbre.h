/*
 Auteur : val15
 Date De Creation : 10/03/16

Role :
represente un arbre du produit cartesien

 */


#ifndef ARBRE_H
#define ARBRE_H
#include "Ensemble.h"
#include <vector>
#include <math.h>

class Arbre
{
public :
	Arbre();
	~Arbre();
	void ajouter(Ensemble e);//pour ajouter un ensemble à l'arbre
	void afficher();
	void produire();
	int getFils(int niveau,int numeroFils);
	bool getFilsExiste(int niveau);
	int getNbFils(int niveau);
	bool getPereExiste(int niveau);
	void afficherEtGetFils(int niveau,int numero);
	void afficherEtGetPere(int niveau,int numero);



protected:


private:
    vector <Ensemble> m_TE;
    vector <Ensemble> m_TEnsembreFinaux;

    vector <int> m_TFinal;

    int m_numEnsembleActuel;
    int m_nbEnsembleFInaux;


};

#endif //ARBRE_H
