etablirConexion :-
        odbc_connect('dbprologm2', _,
                     [ user(tsiry),
                       password('12smL8'),
                       alias(dbprologm2),
                       open(once)
                     ]).
couperConexion :-
        odbc_disconnect('dbprologm2').

pere(Enfant,Pere):-
        odbc_query('dbprologm2',
                   'SELECT (enfant),(pere) FROM bio',
                   row(Enfant,Pere)).
parents(Enfant,Pere,Mere):-
        odbc_query('dbprologm2',
                   'SELECT (enfant),(pere),(mere) FROM bio',
                   row(Enfant,Pere,Mere)).
mere(Enfant,Mere):-
        odbc_query('dbprologm2',
                   'SELECT (enfant),(mere) FROM bio',
                   row(Enfant,Mere)).
enfant(Enfant,Pere):-
        odbc_query('dbprologm2',
                   'SELECT (enfant),(pere) FROM bio',
                   row(Enfant,Pere)).
enfant(Enfant,Mere):-
        odbc_query('dbprologm2',
                   'SELECT (enfant),(mere) FROM bio',
                   row(Enfant,Mere)).


femmeMere(Mere):-
         odbc_query('dbprologm2',
                   'SELECT (mere) FROM bio',
                   row(Mere)).
bio(Enfant,Sexe,Annee_naissance,Annee_mort,Pere,Mere):- %pour tester l'existance de l'element dans la base
         odbc_query('dbprologm2',
                   'SELECT (enfant),(sexe),(annee_naissance),(annee_mort),(pere),(mere) FROM bio',
                   row(Enfant,Sexe,Annee_naissance,Annee_mort,Pere,Mere)).
famme(Femme):-
       odbc_query('dbprologm2',
                   'SELECT (enfant) FROM bio WHERE sexe = "f"',
                   row(Femme)).
homme(Homme):-
       odbc_query('dbprologm2',
                   'SELECT (enfant) FROM bio WHERE sexe = "h"',
                   row(Homme)).


/***************** Les regles *******************/
/*enfant(enfant,parent)*/

/*ptenfant(petit-enfant,grand-parent)*/
/*R3*/ petitenfant(X,Y):-enfant(X,Z),enfant(Z,Y).
/*descendant(descendant,ancetre)*/
/*R4*/ descendant(X,Y):-enfant(X,Y).
/*R5*/ descendant(X,Y):-enfant(X,Z),descendant(Z,Y).

/*à faire ou pas: insertion et suppretion*/















