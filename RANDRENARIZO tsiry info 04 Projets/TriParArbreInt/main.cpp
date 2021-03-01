#include <iostream>
#include <string>
#include "Element.h"
#include "Arbre.h"
#include "Chaine.h"

using namespace std;

int main()
{

    Arbre a0;

   a0.ajouterValeur(35);
    a0.ajouterValeur(40);
    a0.ajouterValeur(254);
    a0.ajouterValeur(1100);
   a0.ajouterValeur(55);
    a0.ajouterValeur(6);
    a0.ajouterValeur(7);
     a0.ajouterValeur(8);
      a0.ajouterValeur(209);
       a0.ajouterValeur(146);
       a0.ajouterValeur(207);
       a0.ajouterValeur(125);
       a0.ajouterValeur(13);
       a0.ajouterValeur(143);
       a0.ajouterValeur(177);
       a0.ajouterValeur(11);
       a0.ajouterValeur(189);
       a0.ajouterValeur(105);
       a0.ajouterValeur(196);
       a0.ajouterValeur(1);
       a0.initialisationDuTableuDesElement();

    //**teste existance et affectation
    /*
        if(a0.elementExist(2))
        {
            Element ep=a0.getElemnt(2);
            cout << "element : " << endl;
            ep.afficher();
            if(a0.fifsExist(ep))
            {
                Element ef=a0.getFifs(ep,1);
                cout << "fils de l' element : " << endl;
                ef.afficher();
                if(a0.pereExist(ef))
                {
                    Element ePer=a0.getPere(ef);
                    cout << "pere du fifs de l' element : " << endl;
                    ePer.afficher();
                }
                else
                    cout << "pere innexistante" << endl;

                if(a0.frereExiste(ef))
                {
                    Element eff=a0.getFrere(ef);
                    cout << "frere du fifs de l' element : " << endl;
                    eff.afficher();
                }
                else
                    cout << "frere innexistante" << endl;
            }
            else
                cout << "fils innexistant" << endl;


        }
        else
            cout << "element innexistant" << endl;
   */




    a0.affichage();
    a0.parcourirEtVerifier();
  // cout << (int)'b' << endl;

 /* if('a'<'b')
        cout << "a est ppeti" << endl;*/
    //cout << maChaine0.at(0);
  /*  Chaine chn0("abcz");
    Chaine chn1("abcl");*/
   // chn0.comparerAvec(chn1);
   /*if(chn0.estEgale(chn1))
        cout << "egaux" << endl;
   else
        cout << "different" << endl;*/
   /* if(chn0.estInferieur(chn1))
        cout << "est inferieur " << endl;*/
  /*  if(chn0.estSuperieur(chn1))
        cout << "est superieur " << endl;*/

    /*if(chn0==chn1)
        cout << "egaux" << endl;*/

   /* if(chn0<chn1)
        cout << "est inferieu" << endl;*/



    return 0;
}
