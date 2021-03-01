#include "Ligne.h"
#include <iostream>

Ligne::Ligne()
{
    //ctor
}

Ligne::Ligne(int t)
{
    m_t=t;
    for(int c=0;c<m_t;c++)
       l.push_back(0);//ctor
}
void Ligne::setE(int i,float v)
{
    l[i-1]=v;

}
float Ligne::get(int i)
{
    return l[i-1];
}

void Ligne::afficher()
{
    for(int c=1;c<=m_t;c++)
        cout << Ligne::get(c);

}


Ligne::~Ligne()
{
    //dtor
}
