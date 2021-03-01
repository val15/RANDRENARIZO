#include "Ensemble.h"
Ensemble::Ensemble()
{
}
void Ensemble::ajouter(int e)
{
    //Ajout de l'élément e(paramètre) dans le tableau des valeurs de l'ensemble
    m_TVE.push_back(e);
}
void Ensemble::ajouterAuDebut(int e)
{
    //Ajout de l'élément e au début du tableau des valeurs de l'ensemble
    m_TVE.push_front(e);
}
void Ensemble::afficher()
{
    //Affichage de tous les elements de l'ensemble
    for(int c=0;c<(int)m_TVE.size();c++)
    {
        if(c<(int)m_TVE.size()-1)
            cout << m_TVE.at(c) << " , ";
        else
            cout << m_TVE.at(c);
    }
    cout << endl;
}
void Ensemble::produireAvec(Ensemble e2)
{
    //distribution de tous les éléments de l'ensemble à prosuire par rapport à chaque élément de e2
    for(int c=0;c<(int) m_TVE.size();c++)
    {
        for(int d=0;d<(int) e2.m_TVE.size();d++)
            cout << m_TVE.at(c) << " , " << e2.m_TVE.at(d) << "," ;
    }
}
int Ensemble::at(int i)
{
    //retourne la valeur de l'élément au i-ème position de l'ensemble
    return m_TVE.at(i);
}
int Ensemble::size()
{
    //retourne la taille de l'ensemble
    return m_TVE.size();
}
Ensemble::~Ensemble()
{
}
