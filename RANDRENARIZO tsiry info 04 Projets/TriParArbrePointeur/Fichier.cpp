#include "Fichier.h"

Fichier::Fichier()
{

}
Fichier::Fichier(string nomFichier)
{
    m_nomFichier=nomFichier;
}

int Fichier::getTaille()
{
    int taille;
    ifstream ifs(m_nomFichier.c_str(), ios_base::in);
     if(!ifs)//si il y a une erreur lors de l'ouverture
       cout << "erreur lors de l'ouverture du fichier " << endl;
    else
    {
         ifs.seekg(0, ios::end);//on se deplace a la fin du fichier
        taille = ifs.tellg();
        ifs.close();
    }
    return taille;
}

int Fichier::getNbLinge()
{
    int nbLigne=0;
    ifstream ifs(m_nomFichier.c_str(), ios_base::in);
     if(!ifs)//si il y a une erreur lors de l'ouverture
       cout << "erreur lors de l'ouverture du fichier " << endl;
    else
    {
         ifs.seekg(0, ios::end);//on se deplace a la fin du fichier
        int taille = ifs.tellg();
        ifs.seekg(0, ios::beg);//on remet le curseur au debut
        for(int c=0;c<taille;c++)
        {
            ifs.seekg(c, ios::beg);
            //cout << c << endl;
            char m;
            ifs.get(m);
           // cout << m;
            if(m=='\n')
                nbLigne++;

        }
       ifs.close();
    }
    return nbLigne;
}

string Fichier::lireUneLinge(int numLigne)
{
    string ligne;
    ifstream ifs(m_nomFichier.c_str(), ios_base::in);
   if(!ifs)//si il y a une erreur lors de l'ouverture
       cout << "erreur lors de l'ouverture du fichier " << endl;
    else
    {
            ifs.seekg(0, ios::end);//on se deplace a la fin du fichier
            int taille = ifs.tellg();
            int p=0;//
            int l=0;
            ifs.seekg(0, ios::beg);//on remet le curseur au debut
            if(numLigne==0)
                getline(ifs, ligne);
            else if(numLigne<taille)
            {
                ifs.seekg(0, ios::beg);//on remet le curseur au debut
                bool fin=false;
                while(!fin )
                {
                    ifs.seekg(p, ios::beg);
                    char m;
                    ifs.get(m);
                    if(m=='\n')
                    {
                        l++;
                        if(l==numLigne)
                        {
                            getline(ifs, ligne);
                            fin=true;
                        }
                    }
                    p++;
                }
        }
        ifs.close();
    }
    return ligne;
}

string Fichier::lireTout()
{
    string contenu;
    ifstream ifs(m_nomFichier.c_str(), ios_base::in);
   if(!ifs)//si il y a une erreur lors de l'ouverture
       cout << "erreur lors de l'ouverture du fichier " << endl;
    else
    {
            ifs.seekg(0, ios::end);
            int taille = ifs.tellg();

            for(int c=0;c<=taille;c++)
            {
                ifs.seekg(c, ios::beg);
                char m;
                ifs.get(m);
                contenu+=m;
            }
    }
    return contenu;
}


void Fichier::ecrireAlaFin(string texte)
{
    ofstream ofs(m_nomFichier.c_str(), ios_base::app);
   if(!ofs)//si il y a une erreur lors de l'ouverture
       cout << "erreur lors de l'ouverture du fichier " << endl;
    else
    {
        ofs << texte << endl;
        ofs.close();
    }
}

void Fichier::ecrireAuDebut(string texte)
{
    ofstream ofs(m_nomFichier.c_str(),ios_base::out);
   if(!ofs)//si il y a une erreur lors de l'ouverture
       cout << "erreur lors de l'ouverture du fichier " << endl;
    else
    {
        ofs << texte << endl;
        ofs.close();
    }
}

