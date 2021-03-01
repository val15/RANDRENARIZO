#include "Chaine.h"

Chaine::Chaine(string contenu)
{
    m_contenu=contenu;
    m_longeur=m_contenu.size();
}

void Chaine::afficher()
{
    cout << m_contenu << endl;
}

char Chaine::at(int i)
{
    return m_contenu.at(i);
}

string Chaine::getContenu()
{
    return m_contenu;
}

int Chaine::getLongeur()
{
    return m_longeur;
}


void Chaine::decomposerParRappotA(int l)
{
   /* for(int c=0;c<m_longeur;c++)
    {
        int i=m_contenu.at(c);
       // cout << i << "   " << endl;
        cout <<(long) i*pow(10,((l-1-c)*3)) << endl;
        m_somme+=i*pow(10,((l-1-c)*3));
        //cout << m_somme << endl;
    }
    cout << "somme = " << m_somme << endl;*/
}


bool Chaine::estEgale(Chaine chb)
{
    bool estEgale=false;

    if(m_longeur==chb.getLongeur())
    {
        int c=0;
        bool fin=false;
        while(!fin)
        {
            if(at(c)!=chb.at(c))
                fin=true;
            else
                c++;
            if(c==getLongeur())
            {

                estEgale=true;
                fin=true;
            }
        }
    }
    return estEgale;
}

bool operator==(Chaine  a, Chaine b)
{
    return a.estEgale(b);
}

bool Chaine::estInferieur(Chaine chn)
{
    int l=0;
    l=l+1-1;
    //Chaine chn(ch.getContenu());
    if(getLongeur()<chn.getLongeur())
        l=getLongeur();
    else if(getLongeur()>chn.getLongeur())
        l=chn.getLongeur();
    else
        l=m_longeur;
     int c=0;
    bool fin=false;
     bool estPluspetit=false;

    if(!estEgale(chn))
    {
        while(!fin)
        {
            if(m_contenu.at(c)<chn.at(c))
            {
                estPluspetit=true;
                fin=true;
            }
            else
            {
                c++;
                if(getLongeur()<chn.getLongeur() && c==l)
                {
                    estPluspetit=true;
                    fin =true;
                }
                if(getLongeur()>chn.getLongeur() && c==l)
                {

                    fin =true;
                }
            }

        }
    }

    return estPluspetit;
}

bool operator<(Chaine  a, Chaine b)
{
    return a.estInferieur(b);
}


bool operator<=(Chaine  a, Chaine b)
{
    return a.estInferieur(b)|| a.estEgale(b);
}

bool Chaine::estSuperieur(Chaine chn)
{
    int l=0;
    l=l+1-1;
    //Chaine chn(ch.getContenu());
    if(getLongeur()<chn.getLongeur())
        l=getLongeur();
    else if(getLongeur()>chn.getLongeur())
        l=chn.getLongeur();
    else
        l=m_longeur;
     int c=0;
     bool fin=false;
     bool estSuperieur=false;

    if(!estEgale(chn))
    {
        while(!fin)
        {
            if(m_contenu.at(c)>chn.at(c))
            {
                estSuperieur=true;
                fin=true;
            }
            else
            {
                c++;
                if(getLongeur()>chn.getLongeur() && c==l)
                {
                    estSuperieur=true;
                    fin =true;
                }
                if(getLongeur()<chn.getLongeur() && c==l)
                {
                    fin =true;
                }
            }

        }
    }
    return estSuperieur;
}

bool operator>(Chaine  a, Chaine b)
{
    return a.estSuperieur(b);
}

bool operator>=(Chaine  a, Chaine b)
{
    return a.estSuperieur(b) || a.estEgale(b);
}

void Chaine::comparerAvec(Chaine chn)
{
    int l=0;
    l=l+1-1;
    //Chaine chn(ch.getContenu());
    if(getLongeur()<chn.getLongeur())
        l=getLongeur();
    else if(getLongeur()>chn.getLongeur())
        l=chn.getLongeur();
    else
        l=m_longeur;

    bool fin=false;
    int c=0;
    while(!fin)
    {
        if(m_contenu.at(c)<chn.at(c))
        {
            cout << m_contenu << " est plus petit " << endl;
            fin=true;
        }
        else if(m_contenu.at(c)>chn.at(c))
        {
            cout << chn.getContenu() << " est plus petit " << endl;
            fin=true;
        }
        else
        {

            c++;
            if(getLongeur()== chn.getLongeur() && c==l)
            {
                cout << "les deux chaines sont egaux " << endl;
                fin=true;
            }

            if(getLongeur()<chn.getLongeur() && c==l)
            {
                cout << m_contenu << " est plus petit " << endl;
                fin =true;
            }

            if(getLongeur()>chn.getLongeur() && c==l)
            {
                cout << chn.getContenu() << " est plus petit " << endl;
                fin =true;
            }


        }

    }
}
