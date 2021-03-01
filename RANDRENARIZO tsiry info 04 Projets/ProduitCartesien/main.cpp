#include <QCoreApplication>
#include "Ensemble.h"
#include "Arbre.h"
#include <iostream>


using namespace std;


int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);





    Ensemble e0,e1;
    e0.ajouter(1);
    e0.ajouter(2);

    e1.ajouter(1);
    e1.ajouter(3);
    e1.ajouter(9);
    e1.ajouter(27);


    Arbre a0;
    a0.ajouter(e0);
    a0.ajouter(e1);
    a0.produire();

    return a.exec();
    return 0;
}
