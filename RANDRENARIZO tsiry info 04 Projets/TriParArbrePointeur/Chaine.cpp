#include "Chaine.h"


Chaine::Chaine()
{

}
Chaine::Chaine(string contenu)
{
    m_contenu=contenu;
    m_longeur=m_contenu.size();
}

void Chaine::afficher()
{
    cout << m_contenu << endl;
}

int Chaine::at(int i)
{
    return (int) m_contenu.at(i);
}

string Chaine::getContenu()
{
    return m_contenu;
}

int Chaine::getLongeur()
{
    return m_longeur;
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
            if(at(c)<chn.at(c))
            {
                estPluspetit=true;
                fin=true;
            }
            else if(at(c)>chn.at(c))
                fin = true;
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
                if(c==l)
                    fin=true;
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
    if(getLongeur()<chn.getLongeur())
        l=getLongeur();
    if(getLongeur()>chn.getLongeur())
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
            if(at(c)>chn.at(c))
            {
                estSuperieur=true;
                fin=true;
            }
            else if(at(c)<chn.at(c))
                fin = true;
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
                if(c==l)
                    fin=true;
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
        if(at(c)<chn.at(c))
        {
            cout << m_contenu << " est plus petit " << endl;
            fin=true;
        }
        else if(at(c)>chn.at(c))
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


Chaine Chaine::operator=(Chaine const& elementACopier)
{
    if(this != &elementACopier) //On vérifie que notre objet n'est   pas le même que celui reçu en argument
    {
        m_contenu=elementACopier.m_contenu;
        m_longeur=elementACopier.m_longeur;
        m_somme=elementACopier.m_somme;
    }
    return *this;//On renvoie l'objet lui-même
}