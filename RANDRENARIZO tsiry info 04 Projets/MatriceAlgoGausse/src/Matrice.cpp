#include "Matrice.h"

Matrice::Matrice()
{
    //ctor
}

Matrice::~Matrice()
{
    //dtor
}


int Matrice::get(int i,int j)
{
    return m_M[i-1].get(j);
}


float Matrice::getB(int i)
{
    return m_B[i-1];
}




void Matrice::setV(int i,int j,float v)
{
    m_M[i-1].setE(j,v);

}

void Matrice::setVB(int i,float v)
{

    m_B[i-1]=v;

}

void Matrice::afficher()
{
    for(int i=0;i<m_n;i++)
    {
        for(int j=0;j<m_n;j++)
        {
            cout << m_M[i].get(j+1) << "   ";
        }
        cout << "   = " << SystemeLineaire::getB(i+1);
        cout << endl;
    }
}
