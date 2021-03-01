/*
 Auteur : val15
 Date De Creation : 23/02/16

Role :
represente une Chainne de racataitre, avec et pouvant etre rucharger en comparaison

 */


#ifndef CHAINNE_H
#define CHAINNE_H
#include <math.h>
#include <iostream>

#include <string>

using namespace std;
class Chainne
{
public :
    Chainne();
	Chainne(string contenu);
    void afficher();
    int getLongeur();
    string getContenu();
    void setContenu(string contenu);
    void comparerAvec(Chainne chn);
    int at(int i);
    bool estEgale(Chainne chb);
    bool estInferieur(Chainne chb);
    bool estSuperieur(Chainne chb);


    Chainne operator=(Chainne const& elementACopier);//surcharge operateur d'attribution
protected:


private:
    string m_contenu;
    int m_longeur;
    int m_somme;



};


//suchage des operateurs de comparaisons
bool operator==(Chainne a, Chainne b);
bool operator<(Chainne a, Chainne b);
bool operator<=(Chainne a, Chainne b);
bool operator>(Chainne a, Chainne b);
bool operator>=(Chainne a, Chainne b);

#endif //Chainne_H
