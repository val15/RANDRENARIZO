#include "matrice.h"

matrice::matrice()
{
    //ctor

}

matrice::~matrice()
{
    //dtor
}

void matrice::entrerValeur()
{
    for(int i=0;i<3;i++)
    {
        for(int j=0;j<3;j++)
        {
            cin >> m_M[i,j];
        }
    }
}

void matrice::afficher()
{
    for(int i=0;i<3;i++)
    {
        for(int j=0;j<3;j++)
        {
            count << m_M[i,j];
        }
        count << endl;
    }
}
