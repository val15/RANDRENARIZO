#include "Arbre.h"



Arbre::Arbre()
{


}


void Arbre::lireUnFichier(string nomFIchier)
{
    Fichier f(nomFIchier);

    for(int c=0;c<f.getNbLinge();c++)
    {
        //ce code est utile pour eliminer les doublant (une anomalie qui n'existe que sous windows)
        if(c!=0)
        {
            if(f.lireUneLinge(c)!=f.lireUneLinge(c-1))
            {
                if(f.lireUneLinge(c-1)!=" ")
                    ajouterValeur(f.lireUneLinge(c));
            }

        }
        else
            ajouterValeur(f.lireUneLinge(c));
    }

    initialisationDuTableuDesElement();

}

void Arbre::lireUnFichierEtRancgerDansUnFichierDontLeNomEstRaneE(string nomFIchier)//le fichier range orrat comme mon le mon du ficher+rangE
{
    Fichier f(nomFIchier);
    for(int c=0;c<f.getNbLinge();c++)
        ajouterValeur(f.lireUneLinge(c));
    initialisationDuTableuDesElement();
    parcourirEtVerifierEtRangerDansUnFichier(nomFIchier+"RangE");
}

void Arbre::ajouterValeur(string v)
{
    string chn(v);
    m_T.push_back(chn);
    m_Ttempo.push_back(chn);
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

     //ajout de tout les élement dans l'arbre, determination des nivaux, du dernier niveau ainsi que du numereo du dernier element
    for(int d=0; d<(int) m_Ttempo.size() ;d++)
    {
        for(int c=0;c<=(int)m_Ttempo.size()/2;c++)
        {
            if(d+1 >=  pow(2,c) && d+1 <  pow(2,c+1))
            {
                int n=c+1;
                //ajout dans le tableau des elements
                Element ne(m_Ttempo.at(d),n,d+1,false);
                m_TE.push_back(ne);
            }

        }
    }

      //determinaton du niveau
   Element de= m_TE.at(m_TE.size()-1);
    m_nbNiveau=de.getNiveau();

    //determination de dernier element
    if(m_TE.size()==pow(2,m_nbNiveau)-1)
       m_numDernierElement=pow(2,m_nbNiveau)-1;
     else if(m_TE.size()==pow(2,m_nbNiveau)-2)
        m_numDernierElement=pow(2,m_nbNiveau)-2;
    else
    {
       if(m_TE.size()>=pow(2,m_nbNiveau-1))
            m_numDernierElement=pow(2,m_nbNiveau-1)-1;
    }


}

void Arbre::affichage()
{
    cout << "affichage : " << endl;
    //juste à titre informatif
    cout << "taille : " << m_TE.size() << endl;
    cout << "nbNiveau : " << m_nbNiveau << endl;
    cout << "num derneir elemnt : " << m_numDernierElement << endl;
    for(int c=0;c<=m_nbNiveau;c++)
    {
        for(int d=0;d<(int) m_TE.size();d++)
        {
            if(m_TE.at(d).getNiveau()== c+1)
                cout<< "("<< m_TE.at(d).getNumero() << ":"<< m_TE.at(d).getValeur() << ")";
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
    //num fils = numElement * 2 + fils 1 ou fils 0
    return m_TE.at(el.getNumero()*2+(f-1) -1);//-1 car le tableau commence tjr par 0
}

bool Arbre::frereExiste(Element e)
{
    Element el=e;
    if(((int)el.getNumero()%2)==0 && el.getNumero() !=(int) m_TE.size())//element a un frere si il est paire et qu'il n'est pas le dernier element
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
    if(e.getNumero()==1)//seul la racine n'a pas de pere
        return false;
    else
        return true;
}
Element Arbre::getPere(Element e)
{
     Element el=e;
     //pere = num elemnt / 2 (partie entiére)
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
            if(fifsExist(elementActuel) && !getFifs(elementActuel,1).getParcouru()) //si fil existe on dessent
            {
                elementActuel=getFifs(elementActuel,1);
                elementActuel.setParcouru(true);
                m_TE.at(elementActuel.getNumero()-1)=elementActuel;//maj du TE
                cout << elementActuel.getValeur() << "\t";
            }
            else if(!fifsExist(elementActuel) && frereExiste(elementActuel))//on par à gauche
            {
                elementActuel=getFrere(elementActuel);
                elementActuel.setParcouru(true);
                m_TE.at(elementActuel.getNumero()-1)=elementActuel;//maj du TE
                cout << elementActuel.getValeur() << "\t";
            }
            else  if(!frereExiste(elementActuel) && !fifsExist(elementActuel))//si frere et fils n'existe pas,n'existe pas, et que pere n'a pas encore ete parcouru on monte
            {
                elementActuel=getPere(elementActuel);
                m_TE.at(elementActuel.getNumero()-1)=elementActuel;//maj du TE
            }
            else  if(!frereExiste(elementActuel)&& fifsExist(elementActuel) && getFifs(elementActuel,1).getParcouru() )//si frere n'existe pas que fifs existe  mais que fils a deja ete parcorue on monte
            {

                 elementActuel=getPere(elementActuel);
                m_TE.at(elementActuel.getNumero()-1)=elementActuel;//maj du TE
            }
            else if(elementActuel.getParcouru() && frereExiste(elementActuel) && getFifs(elementActuel,1).getParcouru() )//on va vers son frere
            {
                    elementActuel=getFrere(elementActuel);
                    elementActuel.setParcouru(true);
                    m_TE.at(elementActuel.getNumero()-1)=elementActuel;//maj du TE
                    cout << elementActuel.getValeur() << "\t";
            }
        }

        m_TE.clear();//pour eviter des bugs memoire
        initialisationDuTableuDesElement();
}

void Arbre::parcourirEtVerifierEtRanger()
{
    affichage();
    while(m_TE.size()!=0)
    {
        Element elementActuel=m_TE.at(0);//c'est la racine
        elementActuel.setParcouru(true);
        verifierEchagerFilsRacine(elementActuel);//verification special pour la racine
        //m_TE.at(0)=elementActuel;//maj du TE
       //affichageDeToutLesElement();

      //  elementActuel=m_TE.at(1);




      //  int t=0;

        while(elementActuel.getNumero()!=getNumDernierElement())
        {
            //cout << "While" << endl;
           if(fifsExist(elementActuel) && !getFifs(elementActuel,1).getParcouru()) //si fil existe on dessent
            {

                elementActuel=getFifs(elementActuel,1);
                elementActuel.setParcouru(true);
                m_TE.at(elementActuel.getNumero()-1)=elementActuel;//maj du TE
                verifierEchager(elementActuel);

            }
            else if(!fifsExist(elementActuel) && frereExiste(elementActuel))//on par à gauche
            {
                elementActuel=getFrere(elementActuel);
                elementActuel.setParcouru(true);
                m_TE.at(elementActuel.getNumero()-1)=elementActuel;//maj du TE
                verifierEchager(elementActuel);
            }
            else  if(!frereExiste(elementActuel) && !fifsExist(elementActuel) )//si frere et fils n'existe pas,n'existe pas,
            {
                elementActuel=getPere(elementActuel);
            }
            else  if(!frereExiste(elementActuel)&& fifsExist(elementActuel) && getFifs(elementActuel,1).getParcouru())//si frere n'existe pas que fifs existe  mais que fils a deja ete parcorue on monte
            {
                 elementActuel=getPere(elementActuel);
            }
            else if(elementActuel.getParcouru() && frereExiste(elementActuel) && getFifs(elementActuel,1).getParcouru() )//on va vers son frere
            {
                    elementActuel=getFrere(elementActuel);
                    elementActuel.setParcouru(true);
                    m_TE.at(elementActuel.getNumero()-1)=elementActuel;//maj du TE
                   verifierEchager(elementActuel);
            }
        }

        affichage();
       Element de=getElemnt(m_TE.size());
       Element pe=getElemnt(1);
       m_TOrdonnE.push_back(pe.getValeur());//on met le premier element dans le tableau des valeur ordonnées
       pe.setValeur(de.getValeur());//o met le dernier element à la place du premier elment
        m_TE.at(0)=pe; ;//change la place du dernier element avec le premier
        m_TE.pop_back();//on supprime de derneir elment



        if(m_TE.size()!=0)
        {
            if(m_Ttempo.size()!=0)
            m_Ttempo.clear();
            for(int c=0;c<(int)m_TE.size();c++)
                m_Ttempo.push_back(m_TE.at(c).getValeur());//on enleve l'élement du tableau temporaire
            m_TE.clear();//pour eviter des erreur memoire
            initialisationDuTableuDesElement();//on remet tout les élement encore non classée dans l'arbre
        }


        //t++;
    }

        //afficher du resultat
    cout << endl <<"resultat : " << endl;
    for(int c=0;c<(int)m_TOrdonnE.size();c++)
        cout << m_TOrdonnE.at(c) << endl;
}


void Arbre::parcourirEtVerifierEtRangerDansUnFichier(string fichier)
{
    parcourirEtVerifierEtRanger();
    //le ficher
    remove(fichier.c_str());//on vide le fichier
    Fichier fs(fichier);
    for(int c=0;c<(int)m_TOrdonnE.size();c++)
    {
       // cout << m_TOrdonnE.at(c).getContenu() << endl;
        fs.ecrireAlaFin(m_TOrdonnE.at(c));
    }
}


void Arbre::echangerValeur(Element e1,Element e2)
{
  //  string vtempo=e1.getValeur().getContenu();

      Element ne1(e2.getValeur(),e1.getNiveau(),e1.getNumero(),e1.getParcouru());
      Element ne2(e1.getValeur(),e2.getNiveau(),e2.getNumero(),e2.getParcouru());


        m_TE.at(e1.getNumero()-1)=ne1;
        m_TE.at(e2.getNumero()-1)=ne2;

     //   verifierEtChangmentBoublan();



}


void Arbre::verifierEtChangmentBoublan()
{
        if(rechercherDoublant()!=-1)
       {
           //cout << "elemnt à l'indice : " << rechercherDoublant() << "est un doublan" <<endl;
           if(rechercherElementIntrovable()!=-1)
            {
                //cout << "elemnt à l'indice : " << rechercherElementIntrovable() << "introuvable" <<endl;
                //on echange la place du doublan avec l'introvable
                m_TE.at(rechercherDoublant()).setValeur(m_T.at(rechercherElementIntrovable()));

            }
       }
}

int Arbre::rechercherElementIntrovable()
{
   /* int i=0;
    while(i<=(int)m_TE.size())
    {
        Chainne chEnCour=m_TE.at(i).getValeur();
        cout << m_T.find(chEnCour) << endl;
    }*/
    int i=0;
    int indice=-1;
    while(i<(int)m_T.size())
    {
        Chainne chec=m_T.at(i);
        if(rechercherUneChainne(chec)!=-1)
           {
        //       cout << "trouver " << endl;
               i++;
           }
        else
        {
          /* cout << chec.getContenu() << "  non trouver " << endl;
           cout << " fin du boucle " << endl;*/
           indice=i;
           i=(int)m_T.size();
        }

           /* if(i==(int)m_T.size())
                cout << chec.getContenu() << "  non trouver " << endl;*/
    }
    return indice;
}

int Arbre::rechercherDoublant()
{
    /*int c=0;

    while(c<m_T.size())
    {
        int fois=0;
        Chainne ch=m_T.at(c).getValeur();
        if(rechercherUneChainneApartirDe(j,Chainne))
            fois++;
    }*/
//    int nbFois=0;
   /* int c=0;
    while(c<(int)m_T.size())
    {
        //int i;
        Chainne ch=m_T.at(c);
        if(rechercherUneChainneApartirDe(ch,0)!=-1)
        {
            cout << "indice " << rechercherUneChainneApartirDe(ch,0) <<endl;
            if(rechercherUneChainneApartirDe(ch,rechercherUneChainneApartirDe(ch,0))!=-1)
                cout <<" doublant touver : " << ch.getContenu() << endl;
        }
        c++;

    }*/
    int indiceD=-1;
    int c=0;
    while(c<(int)m_T.size())
    {
        if(rechercherChainneNbFois(m_T.at(c))!=-1)
        {
            indiceD=rechercherChainneNbFois(m_T.at(c));
            c=(int)m_T.size();
        }
        c++;

           //cout << "doblant "<< m_T.at(c).getContenu() << " indice dernier : " << rechercherChainneNbFois(m_T.at(c)) << endl;
    }
    return indiceD;

}

int Arbre::rechercherChainneNbFois(Chainne ch)
 {
    //int indice(-1);
    int indiceDoublant=-1;
    int i=0;
    int fois=0;
    while(i<(int)m_TE.size())
    {
        if(m_TE.at(i).getValeur()==ch)
        {
        //    indice = i;
            //i=m_TE.size();
            fois++;
            if(fois==2)
            {
                indiceDoublant=i;
                i=(int)m_TE.size();
            }


        }
            i++;

    }
    return indiceDoublant;
   // cout << ch.getContenu() << " * " << fois << endl;
 }

int Arbre::rechercherUneChainne(Chainne ch)
{
    int indice(-1);
    int i=0;
    while(i<(int)m_TE.size())
    {
        if(m_TE.at(i).getValeur()==ch)
        {
            indice = i;
            i=m_TE.size();
        }
        else
        {
            i++;

        }

    }
    return indice;
}

int Arbre::rechercherUneChainneApartirDe(Chainne ch,int indiceDebut)
{
    int indice=-1;
    int i=indiceDebut;
    while(i<(int)m_TE.size())
    {
        if(m_TE.at(i).getValeur()==ch)
        {
            indice = i;
            i=m_TE.size();
        }
        else
        {
            i++;
        }

    }
    return indice;
}

void Arbre::verifierEchager(Element e)
{
    if(pereExist(getElemnt(e.getNumero())))//il faut verifier le pere avans le fils
    {
        Element ep=getPere(e);
        if(e.getValeur()<ep.getValeur())//si valeur du pere est grand
        {
            echangerValeur(e,ep);

        }

        if(e.getNumero()%2==0)//si l'element est pere, on ne verifie plus le fils pour eviter le boucle recursif
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


}

void Arbre::verifierEchagerPere(Element e)
{
    if(pereExist(getElemnt(e.getNumero())))
    {
        Element ep=getPere(e);
        if(e.getValeur()<ep.getValeur())//si valeur du pere est grand
            echangerValeur(e,ep);
        verifierEchagerPere(ep);
    }
}

void Arbre::verifierEchagerFilsRacine(Element e)
{
    if(fifsExist(e))
    {
        Element ef=getFifs(e,1);
        if(e.getValeur()>ef.getValeur())// si fils est plus petit
            echangerValeur(e,ef);
        //verifierEchager(ef);
    }
}
