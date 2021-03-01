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
	void ajouter(Ensemble e);//pour ajouter un ensemble dans l'arbre
	void afficher();
	void produire();
	bool getFilsExiste(int niveau);
	int getNbFils(int niveau);
	void afficherEtGetFils(int niveau,int numero);
protected:

private:
    vector <Ensemble> m_TE;
    vector <Ensemble> m_TEnsembreFinaux;
    vector <int> m_TFinal;
    int m_numEnsembleActuel;
    int m_nbEnsembleFInaux;
};

#endif //ARBRE_H
