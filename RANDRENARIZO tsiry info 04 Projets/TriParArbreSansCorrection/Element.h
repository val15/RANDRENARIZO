/*
 Auteur : val15
 Date De Creation : 18/02/16

Role :
represente un element de l'arbre

 */

#include "Arbre.h"

#include "Chainne.h"
#include <string>
#ifndef ELEMENT_H
#define ELEMENT_H


class Element
{
public :
    Element();
	Element(Chainne valeur,int niveau, int numero,bool parcouru);

	int getNiveau();
	int getNumero();
	Chainne getValeur();

	void setValeur(Chainne v);

    void afficher();
    bool getParcouru();
    void setParcouru(bool p);



    Element operator=(Element const& elementACopier);//operateur d'attribution



private:
    Chainne m_valeur;
    int m_niveau;
    int m_numero;//represent le numero du l'element par rappor à son position dans l'arbre
    bool m_parcouru;



};

#endif //ELEMENT_H
