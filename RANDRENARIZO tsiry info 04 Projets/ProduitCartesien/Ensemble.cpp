#include "Ensemble.h"

Ensemble::Ensemble()
{

}


void Ensemble::ajouter(int e)
{
    m_TE.push_back(e);
}

void Ensemble::ajouterAuDebut(int e)
{
    m_TE.push_front(e);
}

void Ensemble::afficher()
{
    for(int c=0;c<(int)m_TE.size();c++)
    {
        if(c<(int)m_TE.size()-1)
            cout << m_TE.at(c) << " , ";
        else
            cout << m_TE.at(c);
    }
    cout << endl;
}

void Ensemble::afficherProduitEntreEux()
{
    int p=1;
    for(int c=0;c<(int)m_TE.size();c++)
    {

        //if(c<(int)m_TE.size()-1)
            p= m_TE.at(c)*p;
        /*else
            cout << m_TE.at(c);*/
            cout << "p = "<< p << endl;
    }
    cout << endl;

}

void Ensemble::produireAvec(Ensemble e2)
{
    for(int c=0;c<(int) m_TE.size();c++)
    {
        for(int d=0;d<(int) e2.m_TE.size();d++)
            cout << m_TE.at(c) << " , " << e2.m_TE.at(d) << "," ;
    }
}

int Ensemble::at(int i)
{
    return m_TE.at(i);
}

int Ensemble::size()
{
    return m_TE.size();
}

Ensemble::~Ensemble()
{

}
