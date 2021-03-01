#include <iostream>
#include <string>
#include <sstream>

#include "Matrice.h"
using namespace std;
#include "Ligne.h"
Matrice::Matrice()
{

}

Matrice::~Matrice()
{
    //dtor
}

Matrice::Matrice(int i,int j)
{
    m_i=i;
    m_j=j;
    m_determ=1;
    for(int c=0;c<m_i;c++)
    {
        Ligne l0(m_j);
        m_M.push_back(l0);
    }

    //construction de la matrice inverse
    for(int i=1;i<=m_i;i++)
    {
        Ligne l0(m_j);
        for (int j=1;j<=m_j;j++)
        {
            if(i==j)
                l0.setE(j,1);
            else
                l0.setE(j,0);
        }
        m_I.push_back(l0);
    }


    if(m_j==0)//vecteur cologne
    {
        for(int i=0;i<m_i;i++)
        {
            m_V.push_back(0);
        }
    }
    if(m_i==0)//vecteur ligne
    {
        for(int j=0;j<m_j;j++)
        {
            m_V.push_back(0);
        }
    }
}


Matrice::Matrice(int i,int j,int m)//matrice avec B
{

    m_i=i;
    m_j=j;
    for(int c=0;c<m_i;c++)
    {
        Ligne l0(m_i);
        m_M.push_back(l0);
        m_B.push_back(0);
    }
}




void Matrice::entrerValeur()
{



    for(int i=0;i<m_i;i++)
    {
        for(int j=0;j<m_i;j++)
        {
          cout << "M(" << i+1 << "," << j+1 << ") = ";

            float a;
            cin >> a;
            Matrice::setV(i+1,j+1,a);
        }
    }
}

void Matrice::afficher()
{
    if(m_j==0)
    {
        for(int i=1;i<=m_i;i++)
        {
            cout << Matrice::getV(i) << "\n";
        }

    }
    else if(m_i==0)
    {
        for(int j=1;j<=m_j;j++)
            cout << Matrice::getV(j) << "\t";
    }
    else
    {
        for(int i=0;i<m_i;i++)
        {
            for(int j=0;j<m_j;j++)
                std::cout << m_M[i].get(j+1) << "\t";
            cout << endl;
        }
    }

}

void Matrice::afficherAvecInverse()
{
    for(int i=0;i<m_i;i++)
    {
        for(int j=0;j<m_j;j++)
            std::cout << m_M[i].get(j+1) << "\t";
        cout << "\t";
        for(int j=0;j<m_i;j++)
            std::cout <<  m_I[i].get(j+1) << "\t";
        cout << endl;
    }
}


void Matrice::afficherEgale()
{
    for(int i=0;i<m_i;i++)
    {
        for(int j=0;j<m_j;j++)
        {
            cout << m_M[i].get(j+1) << "\t";
        }
        cout << "=" << "\t" << Matrice::getB(i+1);
        cout << endl;
    }
}


void Matrice::afficherI()
{
    for(int i=0;i<m_i;i++)
    {
        for(int j=0;j<m_j;j++)
        {
            std::cout << m_I[i].get(j+1) << "\t";
        }
        std::cout << endl;
    }
}

float Matrice::getV(int i,int j)
{
    return m_M[i-1].get(j);
}

float Matrice::getV(int i)
{
    return m_V[i-1];
}

float Matrice::getB(int i)
{
    return m_B[i-1];
}

float Matrice::getI(int i,int j)
{
    return m_I[i-1].get(j);
}

int Matrice::getNbLigne()
{
    return m_i;
}

int Matrice::getNbColonne()
{
    return m_j;
}



void Matrice::setV(int i,int j,float v)
{
    m_M[i-1].setE(j,v);
}

void Matrice::setV(int i,float v)
{
    m_V[i-1]=v;
}

void Matrice::setVB(int i,float v)
{
    m_B[i-1]=v;
}

void Matrice::setVI(int i,int j,float v)
{
    m_I[i-1].setE(j,v);
}

void  Matrice::affciherElement(int i,int j)
{
    cout << m_M[i-1].get(j);
}



void Matrice::calculeInverse()
{

    for(int k=1;k<=m_i;k++)
    {
        //rangement
        Matrice::rangerAvecLInverse();
           // Matrice::ranger();

        //elimination des facteur
        float p=1.0/Matrice::getV(k,k);
        cout << " p (" << k << ") = " << p << endl;
        for(int c=1;c<=m_j;c++)
        {
            Matrice::setV(k,c,Matrice::getV(k,c)*p);
            Matrice::setVI(k,c,(float)Matrice::getI(k,c)*p);
        }
        //Matrice::afficher();
        cout << endl;



         //modification des autre ligne
        for(int l=1;l<=m_i;l++)
        {
            if(l!=k)
            {
                float f=Matrice::getV(l,k)/Matrice::getV(k,k);
                cout << " fl(" << l << ") = " << f << endl;
                cout << " l(" << l << ") = "<< "l("<<l<<") -  " << f << "* l(" << k <<")" << endl;
                for(int c=1;c<=m_j;c++)
                {
                    Matrice::setV(l,c,Matrice::getV(l,c)-f*Matrice::getV(k,c));
                   Matrice::setVI(l,c,(float)Matrice::getI(l,c)-(float)f*Matrice::getI(k,c));
                }

            }
        }
        Matrice::afficherAvecInverse();
        cout << endl;
        cout << endl;
    }
}

void Matrice::calculeEgale()
{

    for(int k=1;k<=m_i;k++)
    {
        //rangement
        Matrice::ranger();
        Matrice::afficherEgale();


        //elimination des facteur
        float p=1.0/Matrice::getV(k,k);
        cout << " p (" << k << ") = " << p << endl;
        for(int c=1;c<=m_j;c++)
        {
            Matrice::setV(k,c,Matrice::getV(k,c)*p);

        }
        Matrice::setVB(k,Matrice::getB(k)*p);
        Matrice::afficherEgale();
        cout << endl;



         //modification des autre ligne
        for(int l=1;l<=m_i;l++)
        {
            if(l!=k)
            {
                float f=Matrice::getV(l,k)/Matrice::getV(k,k);
                cout << " fl(" << l << ") = " << f << endl;
                cout << " l(" << l << ") = "<< "l("<<l<<") -  " << f << "* l(" << k <<")" << endl;
                for(int c=1;c<=m_j;c++)
                {
                    Matrice::setV(l,c,Matrice::getV(l,c)-f*Matrice::getV(k,c));

                }
                Matrice::setVB(l,Matrice::getB(l)-f*Matrice::getB(k));

            }
        }
         Matrice::afficherEgale();
        cout << endl;
        cout << endl;
    }
}




void Matrice::calculeDeter()
{

    for(int k=1;k<=m_i;k++)
    {
        m_determ=Matrice::getV(k,k)*m_determ;
        //elimination des facteur
        float p=1.0/Matrice::getV(k,k);
        cout << " p (" << k << ") = " << p << endl;
        for(int c=1;c<=m_j;c++)
        {
            Matrice::setV(k,c,Matrice::getV(k,c)*p);
        }
        Matrice::afficher();
        cout << endl;



         //modification des autre ligne
        for(int l=1;l<=m_i;l++)
        {
            if(l!=k)
            {
                float f=Matrice::getV(l,k)/Matrice::getV(k,k);
                cout << " fl(" << l << ") = " << f << endl;
                cout << " l(" << l << ") = "<< "l("<<l<<") -  " << f << "* l(" << k <<")" << endl;
                for(int c=1;c<=m_j;c++)
                {
                    Matrice::setV(l,c,Matrice::getV(l,c)-f*Matrice::getV(k,c));
                }

            }
        }
        Matrice::afficher();
        cout <<"determ =" << m_determ <<  endl;

    }
}

void Matrice::afficherTransposer()
{
    Matrice mtt(m_i,m_j);
    cout << "transposition " << endl;
    for(int i=1;i<=m_i;i++)
    {
        for(int j=1;j<=m_j;j++)
        {
            mtt.setV(i,j,Matrice::getV(j,i));
        }
    }
    mtt.afficher();
        /*for(int j=1;j<=m_n;j++)
        {
            mt.setV(j,i,Matrice::get(i,j));
            cout << mt.get(j,i) << "\t" << endl;
        }
        cout << endl;*/

}

void Matrice::afficherSommeAvec(Matrice s1)
{
    cout << "somme " << endl;

    Matrice::afficher();
    cout << " + " << endl;
    s1.afficher();
    cout << "=" << endl;
    Matrice ms=Matrice(m_i,m_j);
    for(int i=1;i<=m_i;i++)
    {
        for (int j=1;j<=m_j;j++)
        {
            ms.setV(i,j,Matrice::getV(i,j)+s1.getV(i,j));
        }
    }
    ms.afficher();
}

void Matrice::afficherMultiplierAvec(float mf)
{
    cout << "multiplication" << endl;
    Matrice::afficher();
    cout << "*" << endl;
    cout << mf << endl;
    cout << "=" << endl;

    for(int i=1;i<=m_i;i++)
    {
        for(int j=1;j<=m_j;j++)
        {
            Matrice::setV(i,j,Matrice::getV(i,j)*mf);
        }

    }
    Matrice::afficher();

}

void Matrice::afficherMultiplierAvec(Matrice mf)
{
    cout << "multiplication" << endl;
    Matrice::afficher();
    cout << "*" << endl;
    mf.afficher();

    cout <<endl<< "=" << endl;


    if(m_j==0 && mf.getNbLigne()==0)//vecteur ligne * vecteur cologne
    {
        float r=0;
        for(int c=1;c<=m_i;c++)
        {
            r=Matrice::getV(c)*mf.getV(c)+r;
        }
        cout << r << endl;
    }
    if(mf.getNbColonne()==0)//matrice par une vecteur cologne
    {
        Matrice mm(Matrice::getNbLigne(),0);
        for(int i=1;i<=m_i;i++)
        {
            float s=0;
            for(int c=1;c<=m_j;c++)
            {
                s=Matrice::getV(i,c)*mf.getV(c)+s;
                cout << Matrice::getV(i,c)<< " * "<< mf.getV(c) << "\t";

            }
            mm.setV(i,s);
        }
        mm.afficher();
    }
    if(mf.getNbLigne()==0)//matrice par une vecteur cologne
    {
        Matrice mm(0,Matrice::getNbColonne());
        for(int j=1;j<=m_j;j++)
        {
            float s=0;
            for(int c=1;c<=m_i;c++)
            {
                s=mf.getV(c)*Matrice::getV(c,j)+s;
                cout << mf.getV(c) << " * "<< Matrice::getV(c,j) << "\t";

            }
            mm.setV(j,s);
        }
        mm.afficher();
    }
    else//matrice * matrice
    {
        Matrice mm(Matrice::getNbLigne(),mf.getNbColonne());
        for(int i=1;i<=m_i;i++)
        {
            for(int j=1;j<=mf.getNbColonne();j++)
            {
                float s=0;
                for(int c=1;c<=m_j;c++)
                {
                    s=Matrice::getV(i,c)*mf.getV(c,j)+s;
                  //  cout << Matrice::getV(i,c)<< " * "<< mf.getV(c,j) << "\t";
                }
               // cout << endl;
                mm.setV(i,j,s);

            }
        }
        mm.afficher();
    }

}





void Matrice::ranger()
{
    while (Matrice::verification()!=0)
    {
        cout << "rangement " << endl;
        int indice=Matrice::verification();
        for(int c=1;c<=m_i;c++)
        {
            if(c==indice)
            {
                if(c<m_j)
                    Matrice::inverser(c,c+1);
                else
                    Matrice::inverser(c,1);
            }
        }
    }
}

void Matrice::rangerAvecLInverse()
{
    while (Matrice::verification()!=0)
    {
        cout << "rangement " << endl;
        int indice=Matrice::verification();
        for(int c=1;c<=m_i;c++)
        {
            if(c==indice)
            {
                if(c<m_j)
                    Matrice::inverserAvecLInverse(c,c+1);
                else
                    Matrice::inverserAvecLInverse(c,1);
            }
        }
    }
}

int Matrice::verification()//retourne k si il y a un zero sue les pivot
{
    int MofaiseIndice=0;
    for(int k=1;k<=m_j;k++)
    {
        if(Matrice::getV(k,k)==0)
            MofaiseIndice=k;
        else
            MofaiseIndice=0;
    }
    return MofaiseIndice;
}

void Matrice::inverser(int l1,int l2)//on remplace l1 par celle de l2
{

    vector <float> v0=m_M[l1-1].get();//l1-1 car l'indice commence à 0 donc l1=l1-1
    m_M[l1-1].setL(m_M[l2-1].get());
     m_M[l2-1].setL(v0);

     float vb0=Matrice::getB(l1);
     Matrice::setVB(l1,Matrice::getB(l2));
     Matrice::setVB(l2,vb0);
}


void Matrice::inverserAvecLInverse(int l1,int l2)//on remplace l1 par celle de l2
{

    vector <float> v0=m_M[l1-1].get();//l1-1 car l'indice commence à 0 donc l1=l1-1
    m_M[l1-1].setL(m_M[l2-1].get());
     m_M[l2-1].setL(v0);

    /*vector <float> v1=m_I[l1-1].get();//l1-1 car l'indice commence à 0 donc l1=l1-1
    m_I[l1-1].setL(m_I[l2-1].get());
     m_I[l2-1].setL(v1);*/

}











void Matrice::resolutionSymplex()
{
    while(!getToutLesZSontNegativOuNulle())
    {


        cout << "le systeme est :" << endl;
        Matrice::afficherSymplex();

        cout << "calcule de K:" << endl;
        int jve=getIndiceColonneDuPlusGrandValeur(m_i);//jve = indice de la variable d'entree
        //calcule de tout les k
        for(int i=1;i<m_i;i++)
        {
           // cout << "k=c/ve "<< " = " << Matrice::getV(i,m_j-1)/Matrice::getV(i,jve);
            Matrice::setV(i,m_j,Matrice::getV(i,m_j-1)/Matrice::getV(i,jve));// k=c/ve
            //cout << endl;
        }
        Matrice::afficherSymplex();
        int ivs=getIndiceLigneDuPlusPetitValeur(m_j);//jve = indice de la variable de sortie
        //ivs++;

        //cout << "pivot : " << "M("<<ivs<< ","<<  jve << ")  =" << Matrice::getV(ivs,jve);

        //élimination de Gausse à partir des indice du pivot
        elinationAPartirDupivot( ivs,jve);
        Matrice::setVaribleDeBase(ivs,jve);//modification du base
        Matrice::afficherSymplex();

      //  resolutionSymplex();
    }


      /*if(getToutLesZSontNegativOuNulle())
        cout << "tous negative ou nulle" << endl;
      else
      {
          cout << "il y a encore un positif" << endl;
          resolutionSymplex();
      }*/


}

bool Matrice::getToutLesZSontNegativOuNulle()
{
    bool toutNegativeOuNulle=true;
    for(int j=1;j<=m_j;j++)
    {
        if(Matrice::getV(m_i,j)<=0)
            toutNegativeOuNulle=toutNegativeOuNulle*true;
        else
            toutNegativeOuNulle=toutNegativeOuNulle*false;
    }
    return toutNegativeOuNulle;
}


void Matrice::elinationAPartirDupivot(int ip,int jp)
{


        cout << "élimination de Gausse avec M ("<< ip <<","<<jp<< ") commepivot"<<endl;

        //on remet tout les k à 0
        for(int i=1;i<=m_i;i++)
           Matrice::setV(i,m_j,0);
        //elimination des facteur
        float p=1.0/Matrice::getV(ip,jp);
        for(int c=1;c<m_j;c++)
        {
            Matrice::setV(ip,c,Matrice::getV(ip,c)*p);

        }

        Matrice::afficherSymplex();
        cout << endl;



         //modification des autre ligne
        for(int l=1;l<=m_i;l++)
        {
            if(l!=ip)
            {
                float f=Matrice::getV(l,jp)/Matrice::getV(ip,jp);
                cout << " fl(" << l << ") = " << f << endl;
                cout << " l(" << l << ") = "<< "l("<<l<<") -  " << f << "* l(" << ip <<")" << endl;
                for(int c=1;c<=m_j;c++)
                {
                    Matrice::setV(l,c,Matrice::getV(l,c)-f*Matrice::getV(ip,c));

                }


            }
        }
         Matrice::afficherSymplex();
        cout << endl;
}


int Matrice::getIndiceColonneDuPlusGrandValeur(int l)
{
    int jg=0;
    float gv=0;
    for(int j=1;j<=m_j;j++)
    {
        if(Matrice::getV(l,j)>gv)
        {
           gv= Matrice::getV(l,j);
           jg=j;
        }
    }

    cout << "le plus grand valeur = " <<gv<< endl;
    cout << "indice du plus grand valeur = " << jg << endl;

    return jg;
}


int Matrice::getIndiceLigneDuPlusPetitValeur(int c)
{
    int ip=0;
    float pv=10000000000;
    for(int i=1;i<m_i;i++)
    {
        if(Matrice::getV(i,c)<pv)
        {
           pv= Matrice::getV(i,c);
           ip=i;
        }
    }

    cout << "le plus petit valeur = " <<pv<< endl;
    cout << "indice du plus petit valeur = " << ip << endl;

    return ip;
}

void Matrice::afficherSymplex()
{



    for(int i=0;i<m_i;i++)
    {
        if(i<m_i-1)
            cout << m_variableDeBase[i] << "\t";
        else
            cout << "z\t";
        for(int j=0;j<m_j;j++)
            std::cout << m_M[i].get(j+1) << "\t";
        cout << endl;
    }


}

void Matrice::initialisationDesBase()
{
    //initialisation des variable de base
    for(int i=1;i<m_i;i++)
    {
        string e="e(";
        int x=i;            //The integer
        string indice;          //The string
        ostringstream temp;  //temp as in temporary
        temp<<x;
        indice=temp.str();      //str is temp as string

        string fin =")";
        string Nom = e + indice+fin;
        m_variableDeBase.push_back(Nom);
    }
}


void Matrice::setVaribleDeBase(int i,int indicex)
{
    cout << "maj de la base" << endl;
        string x="x(";
        int ind=indicex;            //The integer
        string indice;          //The string
        ostringstream temp;  //temp as in temporary
        temp<<ind;
        indice=temp.str();      //str is temp as string

        string fin =")";
        string Nom = x + indice+fin;
        m_variableDeBase[i-1]=Nom;

}



