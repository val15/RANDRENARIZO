#include "Element.h"
#include "Arbre.h"
using namespace std;


Element::Element(Chaine *valeur,int niveau, int numero,bool parcouru)
{
    m_valeur=valeur;
    m_niveau=niveau;
    m_numero=numero;
   m_parcouru=parcouru;

}

int Element::getNiveau()
{
    return m_niveau;
}

int Element::getNumero()
{
    return m_numero;

}

void Element::setValeur(Chaine* v)
{
    m_valeur=v;
}

Chaine* Element::getValeur()
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
      cout << "valeur : " << m_valeur->getContenu() << endl;
      if(m_parcouru)
        cout << "parcouru" << endl;
      else
        cout << "non parcouru" << endl;

}


Element Element::operator=(Element const& elementACopier)
{
    if(this != &elementACopier) //On vérifie que notre objet n'est   pas le même que celui reçu en argument
    {
        m_niveau=elementACopier.m_niveau;
        m_numero=elementACopier.m_numero;
        m_parcouru=elementACopier.m_parcouru;
        delete m_valeur;
        m_valeur=new Chaine(*(elementACopier.m_valeur));
    }
    //cout << "appel du = " << endl;
    return *this;//On renvoie l'objet lui-même
}

Element::~Element()
{
    delete m_valeur;
}
