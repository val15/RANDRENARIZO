/*
 Auteur : val15
 Date De Creation : 24/02/16

Role :
cette classe nous servira à interagir avec un fichier

 */


#ifndef FICHIER_H
#define FICHIER_H
#include <string>
#include <iostream>
#include <fstream>
using namespace std;

class Fichier
{
public :
    Fichier();
	Fichier(string nomFichier);
	string lireUneLinge(int numlinge);
	string lireTout();
	void ecrireAlaFin(string texte);
	void ecrireAuDebut(string texte);

	int getTaille();
	int getNbLinge();

protected:


private:
    string m_nomFichier;

};

#endif //FICHIER_H
