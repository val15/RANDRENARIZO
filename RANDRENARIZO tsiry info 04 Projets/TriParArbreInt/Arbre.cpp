#include "Arbre.h"


Arbre::Arbre()
{


}

void Arbre::ajouterValeur(int v)
{
    m_T.push_back(v);
    m_Ttempo.push_back(v);

    //geeration du graphe

   //cout << "nb niveau = "<< m_nbNiveau << endl;
   //majDuTableuDesElement();
   /* if(m_T.size()>pow(2,5))
    {
        m_numDernierElement=pow(2,m_nbNiveau)-1;
    }*/
   // cout << "nb niveau = " << m_nbNiveau << endl;

    //m_nbNiveau=m_T.size()/2;
    //determination du nombre de niveau




}

int Arbre::nbNiveau()
{
    return m_nbNiveau;
}


int Arbre::getNumDernierElement()
{
    return m_numDernierElement;
}


void Arbre::initialisationDuTableuDesElement()
{
     //**determination du niveau d'un element (1=racine) et ajout dans

    for(int d=0; d<(int) m_Ttempo.size() ;d++)
    {
        for(int c=0;c<=(int)m_Ttempo.size()/2;c++)
        {
            if(d+1 >=  pow(2,c) && d+1 <  pow(2,c+1))
            {
                int n=c+1;
                //**ajout dans le tableau des element
                Element ne(m_Ttempo.at(d),n,d+1,false);
                m_TE.push_back(ne);

                //cout << "niveau de de l'element n "<< 1 << " = " << c << endl;
            }

        }
    }


    majDuTableuDesElement();



}

void Arbre::majDuTableuDesElement()
{
      //determinaton du niveau
   Element de= m_TE.at(m_TE.size()-1);
    m_nbNiveau=de.getNiveau();
    //determination de dernier element
   // cout << pow(2,m_nbNiveau-1) << endl;

    if(m_TE.size()==pow(2,m_nbNiveau)-1)
       m_numDernierElement=pow(2,m_nbNiveau)-1;
     else if(m_TE.size()==pow(2,m_nbNiveau)-2)
        m_numDernierElement=pow(2,m_nbNiveau)-2;
    else
    {
       if(m_TE.size()>=pow(2,m_nbNiveau-1))
        {
            m_numDernierElement=pow(2,m_nbNiveau-1)-1;

        }
    }


    cout << "taille : " << m_TE.size() << endl;
    cout << "nbNiveau : " << m_nbNiveau << endl;
    cout << "num derneir elemnt : " << m_numDernierElement << endl;



}



void Arbre::affichage()
{
    cout << "affichage : " << endl;

    //**cration gu graphe et entrer des elemnts
    //**ceation

    //**affichage
    for(int c=0;c<=m_nbNiveau;c++)
    {
        for(int d=0;d<(int) m_TE.size();d++)
        {
            if(m_TE.at(d).getNiveau()== c+1)
            {


               /* if(m_TE.at(d).getNumero()%2 == 0)//on pet le tab si l'element est en queqtion est ipaire
                {
                int nbTab=m_nbNiveau/2-(m_TE.at(d).getNiveau()-1);
                for(int e=0;e<=nbTab;e++)
                    cout << "\t";
                }*/
                cout<< "("<< m_TE.at(d).getNumero() << ":"<< m_TE.at(d).getValeur() << ")";



                  /*  int nbTab=m_nbNiveau-m_TE.at(d).getNiveau()-1;
                    for(int e=0;e<=nbTab;e++)
                        cout << "\t";*/



            }

        }
        cout << endl;

    }

}


void Arbre::affichageDeToutLesElement()
{
    for(int c=0;c<(int) m_TE.size();c++)
    {
        cout << "num element : " << m_TE.at(c).getNumero() << "\tniveu element : " << m_TE.at(c).getNiveau() << "\tvaleur element = " << m_TE.at(c).getValeur() << endl;
    }
}



bool Arbre::elementExist(int numElemnt)
{
    if(numElemnt > (int)m_TE.size())
        return false;
    else
        return true;
}

Element Arbre::getElemnt(int numElemnt)
{

    return m_TE.at(numElemnt-1);
}
/*
Element *Arbre::getElemntP(int num)
{
    return *m_TE.at(numElemnt-1);

}
*/

bool Arbre::fifsExist(Element e)
{
    Element el=e;


    if((el.getNumero()<=(int)m_TE.size()/2))
        return true;
    else
        return false;
}

Element Arbre::getFifs(Element e,int f)
{
    Element el=e;
    //cout << el.getNumero()*2) << endl;
    return m_TE.at(el.getNumero()*2+(f-1)  -1);//-1 car le tableau commence tjr par 0

}

bool Arbre::frereExiste(Element e)
{
    Element el=e;
    if(((int)el.getNumero()%2)==0 && el.getNumero() !=(int) m_TE.size())
        return true;
    else
        return false;

}

Element Arbre::getFrere(Element e)
{
    Element el=e;

    return m_TE.at(el.getNumero());//nb: indice de m_TE commence par 0
}


bool Arbre::pereExist(Element e)
{
    //cout << e.getNumero() << endl;
    if(e.getNumero()==1)
        return false;
    else
        return true;
}
Element Arbre::getPere(Element e)
{
     Element el=e;
    //Element ne(m_T.at(d),n,d+1);
    return m_TE.at(el.getNumero()/2 -1);//-1 car commence à 0
}




void Arbre::parcourir()
{
    //bool fin=false;

        Element elementActuel=m_TE.at(0);//c'est la racine
        elementActuel.setParcouru(true);
        m_TE.at(0)=elementActuel;//maj du TE


        cout << elementActuel.getValeur() << "\t";
        while(elementActuel.getNumero()!=getNumDernierElement() /*||fin*/)
        {
            /*if(elementActuel.getNumero()==m_numDernierElement)
              {
                      cout << "FIN ";
                 fin=true;
             }
             else*/ if(fifsExist(elementActuel) && !getFifs(elementActuel,1).getParcouru()) //si fil existe on dessent
            {
               // cout << "on dessent " ;
                elementActuel=getFifs(elementActuel,1);
                elementActuel.setParcouru(true);
                m_TE.at(elementActuel.getNumero()-1)=elementActuel;//maj du TE
                                //elementActuel.afficher();
                cout << elementActuel.getValeur() << "\t";
            }
            else if(!fifsExist(elementActuel) && frereExiste(elementActuel))//on par à gauche
            {
              //  cout << "on vas ver la gauche " ;
                elementActuel=getFrere(elementActuel);
                elementActuel.setParcouru(true);
                m_TE.at(elementActuel.getNumero()-1)=elementActuel;//maj du TE
                cout << elementActuel.getValeur() << "\t";
            }
            else  if(!frereExiste(elementActuel) && !fifsExist(elementActuel) /*&& pereExist(elementActuel)*/)//si frere et fils n'existe pas,n'existe pas, et que pere n'a pas encore ete parcouru on monte
            {
               // cout << "on monte1 " ;
                elementActuel=getPere(elementActuel);
                m_TE.at(elementActuel.getNumero()-1)=elementActuel;//maj du TE

            }
            else  if(!frereExiste(elementActuel)&& fifsExist(elementActuel) && getFifs(elementActuel,1).getParcouru() /*&& pereExist(elementActuel)*/)//si frere n'existe pas que fifs existe  mais que fils a deja ete parcorue on monte
            {
               // cout << "on monte2 " ;
                 elementActuel=getPere(elementActuel);
                m_TE.at(elementActuel.getNumero()-1)=elementActuel;//maj du TE
            }
            else if(elementActuel.getParcouru() && frereExiste(elementActuel) && getFifs(elementActuel,1).getParcouru() )//on va vers son frere
            {
               // cout << "on vas à goche " ;
                    elementActuel=getFrere(elementActuel);
                    elementActuel.setParcouru(true);
                    m_TE.at(elementActuel.getNumero()-1)=elementActuel;//maj du TE

                   // elementActuel.afficher();
                    cout << elementActuel.getValeur() << "\t";
            }
        }


            /*else if(!frereExiste(elementActuel)&& !fifsExist(elementActuel) && pereExist(elementActuel) && getPere(elementActuel).getParcouru() )
             {
                 cout << "FIN ";
                 fin=true;
             }*/



          /*  cout << "element actuel :  " << endl;
            elementActuel.afficher();*/


        //elementActuel=m_TE.at(0);//c'est la racine
        m_TE.clear();//pour eviter des bugs memoire
        initialisationDuTableuDesElement();


}


void Arbre::parcourirEtVerifier()
{
    //int C=0;
 //  bool fin=false;
    while(m_TE.size()!=0)
    {
        Element elementActuel=m_TE.at(0);//c'est la racine
        elementActuel.setParcouru(true);
        m_TE.at(0)=elementActuel;//maj du TE


        //cout << elementActuel.getValeur() << "\t";
        //
      //  elementActuel.afficher();
        verifierEchager(elementActuel);

       // verifierEchagerFils(elementActuel);

       // Element ef=getFifs(elementActuel,1);
       /* if(elementActuel.getValeur()> getFifs(elementActuel,1).getValeur())// si per est plus petit
       {
           int vt=elementActuel.getValeur();
           m_TE.at(0).setValeur(getFifs(elementActuel,1).getValeur());
           m_TE.at(1).setValeur(vt);
       }
            echangerValeur(elementActuel,getFifs(elementActuel,1));*/
        while(elementActuel.getNumero()!=getNumDernierElement() /*||fin*/)
        {
           // elementActuel.afficher();
            /*if(elementActuel.getNumero()==m_numDernierElement)
              {
                      cout << "FIN ";
                 fin=true;
             }
             else*/ if(fifsExist(elementActuel) && !getFifs(elementActuel,1).getParcouru()) //si fil existe on dessent
            {
                //cout << "on dessent " ;
                elementActuel=getFifs(elementActuel,1);
                elementActuel.setParcouru(true);
                m_TE.at(elementActuel.getNumero()-1)=elementActuel;//maj du TE
                                //elementActuel.afficher();
                //cout << elementActuel.getValeur() << "\t";
                verifierEchager(elementActuel);

            }
            else if(!fifsExist(elementActuel) && frereExiste(elementActuel))//on par à gauche
            {
               // cout << "on vas ver la gauche " ;
                elementActuel=getFrere(elementActuel);
                elementActuel.setParcouru(true);
                m_TE.at(elementActuel.getNumero()-1)=elementActuel;//maj du TE
                //cout << elementActuel.getValeur() << "\t";
                verifierEchager(elementActuel);
            }
            else  if(!frereExiste(elementActuel) && !fifsExist(elementActuel) /*&& pereExist(elementActuel)*/)//si frere et fils n'existe pas,n'existe pas, et que pere n'a pas encore ete parcouru on monte
            {
                //cout << "on monte1 " ;
                elementActuel=getPere(elementActuel);
                m_TE.at(elementActuel.getNumero()-1)=elementActuel;//maj du TE

            }
            else  if(!frereExiste(elementActuel)&& fifsExist(elementActuel) && getFifs(elementActuel,1).getParcouru() /*&& pereExist(elementActuel)*/)//si frere n'existe pas que fifs existe  mais que fils a deja ete parcorue on monte
            {
               // cout << "on monte2 " ;
                 elementActuel=getPere(elementActuel);
                m_TE.at(elementActuel.getNumero()-1)=elementActuel;//maj du TE
            }
            else if(elementActuel.getParcouru() && frereExiste(elementActuel) && getFifs(elementActuel,1).getParcouru() )//on va vers son frere
            {
               // cout << "on vas à goche " ;
                    elementActuel=getFrere(elementActuel);
                    elementActuel.setParcouru(true);
                    m_TE.at(elementActuel.getNumero()-1)=elementActuel;//maj du TE

                   // elementActuel.afficher();
                   // cout << elementActuel.getValeur() << "\t";
                   verifierEchager(elementActuel);
            }
        }


            /*else if(!frereExiste(elementActuel)&& !fifsExist(elementActuel) && pereExist(elementActuel) && getPere(elementActuel).getParcouru() )
             {
                 cout << "FIN ";
                 fin=true;
             }*/



          /*  cout << "element actuel :  " << endl;
            elementActuel.afficher();*/


        //elementActuel=m_TE.at(0);//c'est la racine


        affichage();
       Element de=getElemnt(m_TE.size());
       Element pe=getElemnt(1);
       m_TOrdonnE.push_back(pe.getValeur());//on pet le premier elment dans le tableau des valeur ordonnées
       pe.setValeur(de.getValeur());//o met le dernier elemnt à la place du dernier elment
        m_TE.at(0)=pe; ;//change la place du dernier element avec le premier
        m_TE.pop_back();//on supprime de derneir elment



        if(m_TE.size()!=0)
        {
            if(m_Ttempo.size()!=0)
            m_Ttempo.clear();
            for(int c=0;c<(int)m_TE.size();c++)
                m_Ttempo.push_back(m_TE.at(c).getValeur());
            m_TE.clear();//pour eviter des erreur memoire
            initialisationDuTableuDesElement();
            affichage();
        }
       /* else
            fin=true;*/

        /*cout << C << endl;*/
       // C++;

    }

    for(int c=0;c<(int)m_TOrdonnE.size();c++)
        cout << m_TOrdonnE.at(c) << "\t" << endl;
}

void Arbre::echangerValeur(Element e1,Element e2)
{
    int vtempo=e1.getValeur();
  /*  cout <<"vtempo = "<< vtempo << endl;
    cout <<"e2 = "<< e2.getValeur() << endl;*/
    m_TE.at(e1.getNumero()-1).setValeur(e2.getValeur());
    m_TE.at(e2.getNumero()-1).setValeur(vtempo);
    /*m_TE.at(e1.getNumero()-1)=e1;
    m_TE.at(e2.getNumero()-1)=e2;*/
  /*  cout <<"e1 = "<< e1.getValeur() << endl;
    cout <<"e2 = "<< e2.getValeur() << endl;*/

}

void Arbre::verifierEchager(Element e/*,Element eDejaVerifie=null*/)
{
    //cout << e.getNumero() << endl;

   // eDejaVerifie=NULL;
   if(pereExist(getElemnt(e.getNumero())))//il faut verifier le pere avans le fils
    {
        //cout << "pere exitse " << endl;
        Element ep=getPere(e);
        if(e.getValeur()<ep.getValeur())//si valeur du pere est grand
            echangerValeur(e,ep);
        if(e.getNumero()%2==0)//si l'element est pare, on ne verifie plus le fils pour eviter le boucle recursif
            verifierEchagerPere(ep);
        else
            verifierEchager(ep);
    }


    if(fifsExist(e))
    {
        Element ef=getFifs(e,1);
        if(e.getValeur()>ef.getValeur())// si fils est plus petit
            echangerValeur(e,ef);
        verifierEchager(ef);
    }
   // e=getElemnt(e.getNumero());




  /*  m_TE.at(e.getNumero()-1)=e;
    m_TE.at(ep.getNumero()-1)=ep;
    m_TE.at(ep.getNumero()-1)=ef;*/
/*
    verifierEchager(ep);//on verifie le pere
    verifierEchager(ef);//on verifie le fils*/
}

void Arbre::verifierEchagerPere(Element e)
{
    if(pereExist(getElemnt(e.getNumero())))
    {
       // cout << "pere exitse " << endl;
        Element ep=getPere(e);
        if(e.getValeur()<ep.getValeur())//si valeur du pere est grand
            echangerValeur(e,ep);
        verifierEchagerPere(ep);
    }
}


void Arbre::verifierEchagerFils(Element e)
{
    if(fifsExist(e))
    {
        Element ef=getFifs(e,1);
        if(e.getValeur()>ef.getValeur())// si fils est plus petit
            echangerValeur(e,ef);
        verifierEchager(ef);
    }
}








/*
Arbre Arbre::operator=(Arbre const& arbreACopier)
{
    if(this != &arbreACopier) //On vérifie que notre objet n'est   pas le même que celui reçu en argument
    {
        m_nbNiveau=arbreACopier.m_nbNiveau;
        m_T=arbreACopier.m_T;
        m_TE=arbreACopier.m_TE;


         //On renvoie l'objet lui-même
    }
    return *this;
}*/

