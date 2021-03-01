#include "Arbre.h"

Arbre::Arbre()
{
    m_numEnsembleActuel=0;
}


void Arbre::ajouter(Ensemble e)
{
    m_TE.push_back(e);
}

void Arbre::afficher()
{
    for(int c=0;c<(int) m_TE.size();c++)
    {
        m_TE.at(c).afficher();
        cout<< endl;
    }
}

void Arbre::produire()
{
    /*for(int C=0;C<(int) m_TE.size();C++)
    {
       for(int c=0;c<m_TE.at(C).getSize();c++)
            cout << m_TE.at(C).get(c) << endl;

  //  }


    for(int c=0;c<(int) m_TE.size();c++)
    {

        for(int d=0;d<(int) e2.m_TE.size();d++)
            cout << m_TE.at(c) << " , " << e2.m_TE.at(d) << "," ;
    }*/


    m_nbEnsembleFInaux=1;
    for(int c=0;c<(int)m_TE.size();c++)
        m_nbEnsembleFInaux*=m_TE.at(c).size();

    for(int c=0;c<=m_nbEnsembleFInaux;c++)
    {
        Ensemble et;
        m_TEnsembreFinaux.push_back(et);
    }



    for(int c=0;c<(int) m_TE.at(0).size();c++)
    {
     //   vector <int> ttempo;
        afficherEtGetFils(0,c);
    //    cout << " tour n " << c << ":  " << ttempo.at(c) <<endl;

    }
   // m_TEnsembreFinaux.at(0).afficher();

   int occurence=1;
   for(int c=1;c<(int)m_TE.size();c++)
        occurence*=m_TE.at(c).size();

   for(int d=0;d<m_nbEnsembleFInaux;d+=occurence)
    {
        for(int c=d+1;c<d+occurence;c++)
        {
            if(m_TEnsembreFinaux.at(c).size()!=m_TEnsembreFinaux.at(c-1).size())
            {
                int difference= m_TEnsembreFinaux.at(c-1).size()-m_TEnsembreFinaux.at(c).size();
                cout << difference << endl;
                for(int e=difference-1;e>=0;e--)
                {
                    //cout << "e = " << e << endl;
                    m_TEnsembreFinaux.at(c).ajouterAuDebut(m_TEnsembreFinaux.at(c-1).at(e));
                }
            }
        }
    }

/*
    for(int d=0;d<m_nbEnsembleFInaux;d+=3)
    {
       // cout << "eea" << endl;
        for(int c=d+1;c<d+3;c++)
        {
            if(m_TEnsembreFinaux.at(c).size()!=m_TEnsembreFinaux.at(d).size())
            {
               // cout << m_TEnsembreFinaux.at(0).size();
                int difference= m_TEnsembreFinaux.at(d).size()-m_TEnsembreFinaux.at(c).size();
                cout << difference << endl;
                for(int e=difference-1;e>=0;e--)
                {
                    //cout << "e = " << e << endl;
                    m_TEnsembreFinaux.at(c).ajouterAuDebut(m_TEnsembreFinaux.at(d).at(e));
                }

            }
        }
    }*/

    for(int c=0;c<m_nbEnsembleFInaux;c++)
     {
       m_TEnsembreFinaux.at(c).afficher();
      //  m_TEnsembreFinaux.at(c).afficherProduitEntreEux();
    }




  /*  for(int c=0;c<(int)m_TFinal.size();c++)
        cout << m_TFinal.at(c) << endl;*/




   /* for(int c=0;c<(int) m_TE.at(0).getSize();c++)
    {
     //   vector <int> ttempo;
        afficherEtGetFils(0,c);
    //    cout << " tour n " << c << ":  " << ttempo.at(c) <<endl;

    }*/

    /*for(int n=0;n<m_TEnsembreFinaux.size();n++)
    {
        int indiceEnsembleDeReference;
        if(m_TEnsembreFinaux.at(1).getSize()!=(int)m_TE.size())
        {
            int difference=m_TE.size()-m_TEnsembreFinaux.at(1).getSize();
       // cout << difference  << endl;
       //on complete les elemnt implet
     //  m_TEnsembreFinaux.at(1).ajouter()
            for(int c=difference-1;c>=0;c--)
                m_TEnsembreFinaux.at(1).ajouterAuDebut(m_TEnsembreFinaux.at(0).at(c));
        }
    }*/

    //m_TEnsembreFinaux.at(1).afficher();
    /*
    for(int c=0;c<(int)m_TFinal.size();c++)
        cout << m_TFinal.at(c) << endl;



    for(int c=0;c<(int)m_TE.size();c++)
        m_TEnsembreFinaux.at(0).ajouter(m_TFinal.at(c));


    for(int c=0;c<(int)m_TE.size()-1;c++)
    {
        m_TEnsembreFinaux.at(1).ajouter(m_TFinal.at(c));
    }
    m_TEnsembreFinaux.at(1).ajouter(m_TFinal.at(m_TE.size()));

    for(int c=0;c<(int)m_TE.size()-2;c++)
    {
        m_TEnsembreFinaux.at(2).ajouter(m_TFinal.at(c));
    }
    m_TEnsembreFinaux.at(2).ajouter(m_TFinal.at(m_TE.size()+1));
    m_TEnsembreFinaux.at(2).ajouter(m_TFinal.at(m_TE.size()+2));

    for(int c=0;c<(int)m_TE.size()-3;c++)
    {
        m_TEnsembreFinaux.at(3).ajouter(m_TFinal.at(c));
    }
    m_TEnsembreFinaux.at(3).ajouter(m_TFinal.at(m_TE.size()+1));
    m_TEnsembreFinaux.at(3).ajouter(m_TFinal.at(m_TE.size()+2));
    m_TEnsembreFinaux.at(3).ajouter(m_TFinal.at(m_TE.size()+3));


    m_TEnsembreFinaux.at(0).afficher();
    cout << endl;
    m_TEnsembreFinaux.at(1).afficher();
    cout << endl;
    m_TEnsembreFinaux.at(2).afficher();
    cout << endl;
    m_TEnsembreFinaux.at(3).afficher();*/

}




void Arbre::afficherEtGetFils(int niveau,int numero)
{
    //m_TEnsembreFinaux.at(niveau).push_back(m_TE.at(niveau).at(numero));
    m_TEnsembreFinaux.at(m_numEnsembleActuel).ajouter((m_TE.at(niveau).at(numero)));
    //cout << m_TE.at(niveau).at(numero) << endl;
    m_TFinal.push_back(m_TE.at(niveau).at(numero));
    if(getFilsExiste(niveau))
    {
        for(int c=0;c<getNbFils(niveau);c++)
            afficherEtGetFils(niveau+1,c);
    }
    else
    {
        cout << "pas de fils" << endl;
        //cout << "niveu = " << niveau << " numero = " << numero << "dernier element = " << m_TE.at(niveau).at(numero) << endl;
      // if(numero ==(int) m_TE.at(m_TE.size()-1).getSize()-1)
          m_numEnsembleActuel++;
       //cout << "mun = " << numero << endl;
      // afficherEtGetPere(niveau,numero-1);
    }


    //return m_TE.at(niveau).at(numero);
}

void Arbre::afficherEtGetPere(int niveau,int numero)
{
    cout << m_TE.at(niveau).at(numero) << endl;
   if(getPereExiste(niveau))
    {
        afficherEtGetPere(niveau-1,numero-1);
    }
    else
    {
        cout << "pas de pere" << endl;
    }


    //return m_TE.at(niveau).at(numero);
}

int Arbre::getNbFils(int niveau)
{
    return m_TE.at(niveau+1).size();
}

bool Arbre::getFilsExiste(int niveau)
{
    bool existe;
    existe=false;
    if(niveau<(int) m_TE.size()-1)
        existe=true;
    return existe;

}

bool Arbre::getPereExiste(int niveau)
{
    bool existe;
    existe=false;
    if(niveau!=0)
        existe=true;
    return existe;

}

int Arbre::getFils(int niveau,int numeroFils)
{

    int r=-1;
    if(niveau<(int)m_TE.size()-1)
       r= m_TE.at(niveau+1).at(numeroFils);
    return r;
}

Arbre::~Arbre()
{

}
