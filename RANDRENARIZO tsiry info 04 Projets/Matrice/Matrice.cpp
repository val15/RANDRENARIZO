#include "Matrice.h"

Matrice::Matrice()
{
    m_nb_ligne=0;
    m_nb_colonne=0;
    m_contenu="";
}

Matrice::~Matrice()
{

}

QString Matrice::toString()
{
    //QString mat,c,l;

   // mat="nonbre de ligne :"+l.setNum(m_nb_ligne)+" nonbre de collonne :"+c.setNum(m_nb_colonne);
    return "mat";
}
