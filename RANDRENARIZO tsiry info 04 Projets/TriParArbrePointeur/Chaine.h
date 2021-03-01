/*
 Auteur : val15
 Date De Creation : 23/02/16

Role :
represente une chaine de racataitre, avec et pouvant etre rucharger en comparaison

 */


#ifndef CHAINE_H
#define CHAINE_H
#include <math.h>
#include <iostream>

#include <string>

using namespace std;
class Chaine
{
public :
    Chaine();
	Chaine(string contenu);
    void afficher();
    int getLongeur();
    string getContenu();
    void comparerAvec(Chaine chn);
    int at(int i);
    bool estEgale(Chaine chb);
    bool estInferieur(Chaine chb);
    bool estSuperieur(Chaine chb);


    Chaine operator=(Chaine const& elementACopier);//surcharge operateur d'attribution
protected:


private:
    string m_contenu;
    int m_longeur;
    int m_somme;



};


//suchage des operateurs de comparaisons
bool operator==(Chaine a, Chaine b);
bool operator<(Chaine a, Chaine b);
bool operator<=(Chaine a, Chaine b);
bool operator>(Chaine a, Chaine b);
bool operator>=(Chaine a, Chaine b);

#endif //CHAINE_H
