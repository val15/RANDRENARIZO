#ifndef MATRICE_H
#define MATRICE_H


class Matrice
{
    public:
        Matrice();
        virtual ~Matrice();
        int get(int i,int j);
        float getB(int i);
        void setV(int i,int j,float v);
        void setVB(int i,float v);
        void afficher();
    protected:
    private:
        int m_n;
        vector <Ligne> m_M;
        vector <float> m_B;
};

#endif // MATRICE_H
