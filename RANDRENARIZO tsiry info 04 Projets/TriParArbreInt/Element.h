/*
 Auteur : val15
 Date De Creation : 18/02/16

Role :
represente un element de l'arbre

 */

#include "Arbre.h"
#include "ObjetArbre.h"
#ifndef ELEMENT_H
#define ELEMENT_H


class Element
{
public :
    Element();
	Element(int valeur,int niveau, int numero,bool parcouru);
	//void setArbre(Arbre a);

	int getNiveau();
	int getNumero();
	int getValeur();
	//Elemnt getPere();
	//Eemnt getFils(int nFils);//nFils 1 ou 2
	void setValeur(int v);
	void changerValeur();//cette fonction change le valeur de l'elemnt et effectue les operation de verifiction de de changenemt sur les fils ou le pere
    void afficher();
    bool getParcouru();
    void setParcouru(bool p);



    Element operator=(Element const& elementACopier);//operateur d'attribution



private:
    int m_valeur;
    int m_niveau;
    int m_numero;//represent le numero du l'element par tappor à son niveau
    bool m_parcouru;



};

#endif //ELEMENT_H
