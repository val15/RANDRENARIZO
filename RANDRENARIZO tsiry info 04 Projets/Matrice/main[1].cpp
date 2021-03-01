#include <iostream>
#include "Matrice.h"
#include "Ligne.h"
#include <vector>
using namespace std;

int main()
{
    Matrice s0(3,3);//matrice simple
  //  Matrice s0(4,4,4);//matrice avec egal
    s0.setV(1,1,2);
    s0.setV(1,2,-1);
    s0.setV(1,3,0);
    s0.setV(2,1,-1);
    s0.setV(2,2,2);
    s0.setV(2,3,-1);
    s0.setV(3,1,0);
    s0.setV(3,2,-1);
    s0.setV(3,3,2);

   /* s0.setV(1,1,3);
    s0.setV(1,2,2);
    s0.setV(2,1,2);
    s0.setV(2,2,-1);*/


   /*   s0.setVB(1,6);
     s0.setVB(2,-2);
     s0.setVB(3,-8);
     s0.setVB(4,-4);

*/
    //s0.afficherEgale();
   // s0.afficherI();
   s0.afficher();
    s0.calculeInverse();
    //s0.calculeEgale();

    //s0.afficher();



    return 0;
}
