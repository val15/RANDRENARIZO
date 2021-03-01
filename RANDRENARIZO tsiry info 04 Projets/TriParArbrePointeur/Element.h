/*
 Auteur : val15
 Date De Creation : 18/02/16

Role :
represente un element de l'arbre

 */

#include "Arbre.h"

#include "Chaine.h"
#include <string>
#ifndef ELEMENT_H
#define ELEMENT_H


class Element
{
public :
    Element();
    ~Element();
	Element(Chaine *valeur,int niveau, int numero,bool parcouru);

	int getNiveau();
	int getNumero();
	Chaine* getValeur();

	void setValeur(Chaine* v);
    void afficher();
    bool getParcouru();
    void setParcouru(bool p);



    Element operator=(Element const& elementACopier);//operateur d'attribution



private:
    Chaine *m_valeur;
    int m_niveau;
    int m_numero;//represent le numero du l'element par rappor à son position dans l'arbre
    bool m_parcouru;



};

#endif //ELEMENT_H
