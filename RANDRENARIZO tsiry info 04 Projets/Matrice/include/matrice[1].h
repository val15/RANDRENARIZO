#ifndef MATRICE_H
#define MATRICE_H


class matrice
{
    //supposant d'abort que c'est une matrice 3*3
    private:
        int m_M[3,3];

    public:
        matrice();
        void entrerValeur();
        void afficher();
        virtual ~matrice();
    protected:

};

#endif // MATRICE_H
