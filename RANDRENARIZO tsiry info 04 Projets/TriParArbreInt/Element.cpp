#include "Element.h"
#include "Arbre.h"
using namespace std;


Element::Element(int valeur,int niveau, int numero,bool parcouru)
{
    m_valeur=valeur;
    m_niveau=niveau;
    m_numero=numero;
   // m_arbre=arbre
   m_parcouru=parcouru;

}



Element::Element()
{
        m_valeur=0;
    m_niveau=0;
    m_numero=0;
   // m_arbre=arbre
   m_parcouru=false;
}

int Element::getNiveau()
{
    return m_niveau;
}

int Element::getNumero()
{
    return m_numero;

}

void Element::setValeur(int v)
{
    m_valeur=v;
}

void Element::changerValeur()
{

}


int Element::getValeur()
{
    return m_valeur;

}

void Element::setParcouru(bool p)
{
    m_parcouru=p;
}

bool Element::getParcouru()
{
    return m_parcouru;
}

void Element::afficher()
{
    cout << "numero : " << m_numero << endl;
     cout << "niveau : " << m_niveau << endl;
      cout << "valeur : " << m_valeur << endl;
      if(m_parcouru)
        cout << "parcouru" << endl;
      else
        cout << "non parcouru" << endl;

}


Element Element::operator=(Element const& elementACopier)
{
    if(this != &elementACopier) //On vérifie que notre objet n'est   pas le même que celui reçu en argument
    {
        m_valeur=elementACopier.m_valeur;
        m_niveau=elementACopier.m_niveau;
        m_numero=elementACopier.m_numero;
        m_parcouru=elementACopier.m_parcouru;


         //On renvoie l'objet lui-même
    }
    return *this;
}
