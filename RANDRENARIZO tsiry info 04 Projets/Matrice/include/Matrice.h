#ifndef MATRICE_H
#define MATRICE_H
#include "Ligne.h"
#include <vector>
using namespace std;

class Matrice
{
   //supposant d'abort que c'est une matrice 3*3
    private:

        vector <Ligne> m_M;
       // vector<float> m_M;
        //float m_M[3][3];
        vector <Ligne> m_I;// pour inverser la matrice
        vector<float> m_B;//utile dans le cas d'une equation


        vector<float> m_V;// pour les matrice vecteur

       // int m_n;
        int m_i;
        int m_j;
        float m_determ;


        //pour le simplex
        vector<string> m_variableDeBase;




    public:
        Matrice();
        Matrice(int i,int j);//M(i,j)=> matrie à i ligne et j colonne  M(i,0)=> vecteur ligne   M(0,j)=> vecteur colonne
        Matrice(int i,int j,int m);//matrice avec B => M(i,j)=B(i)
        void entrerValeur();
        void afficher();
        void afficherSymplex();
        void afficherAvecInverse();
        void afficherEgale();
        void afficherI();
        float getV(int i,int j);
        float getV(int i);//matrice vecteur
        float getB(int i);
        float getI(int i,int j);
        int getNbLigne();
        int getNbColonne();


        void setV(int i,int j,float v);
        void setV(int i,float v);//matrice vecteur
        void setVI(int i,int j,float v);
        void setVB(int i,float v);
        void affciherElement(int i,int j);

        void calculeInverse();
        void calculeEgale();
        void calculeDeter();


        void afficherTransposer();
        void afficherSommeAvec(Matrice s1);
        void afficherMultiplierAvec(float mf);
        void afficherMultiplierAvec(Matrice mf);

        int verification();
        void inverser(int l1,int l2);
        void ranger();
        void inverserAvecLInverse(int l1,int l2);
        void rangerAvecLInverse();


        void resolutionSymplex();
        int getIndiceColonneDuPlusGrandValeur(int l);
        int getIndiceLigneDuPlusPetitValeur(int c);
        void elinationAPartirDupivot(int ip,int jp);
        bool getToutLesZSontNegativOuNulle();
        virtual ~Matrice();

        void initialisationDesBase();
        void setVaribleDeBase(int i,int indicex);

    protected:
};

#endif // MATRICE_H
