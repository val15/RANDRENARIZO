#ifndef LIGNE_H
#define LIGNE_H
#include <vector>
using namespace std;


class Ligne
{

    public:
        Ligne();
        Ligne(int t);;
        void setE(int i,float v);
        float get(int i);
        void afficher();
        virtual ~Ligne();
    protected:
    private:
        vector<float> l;
        int m_t;
};

#endif // LIGNE_H
