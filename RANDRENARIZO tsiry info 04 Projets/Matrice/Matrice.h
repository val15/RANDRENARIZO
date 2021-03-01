/*
 Auteur : val15
 Date De Creation : 16/10/13

Role :
un objet qui sera une martice

 */


#ifndef MATRICE_H
#define MATRICE_H
#include <QString>

class Matrice
{
public :
    Matrice();
    ~Matrice();
    QString toString();

protected:


private:
    int m_nb_ligne;
    int m_nb_colonne;
    QString m_contenu;


};

#endif //MATRICE_H
