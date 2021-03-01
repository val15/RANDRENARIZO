#include "Arbre.h"

Arbre::Arbre()
{
    m_numEnsembleActuel=0;
}

void Arbre::ajouter(Ensemble e)
{
    //Ajout de l'ensemble e dans l'arbre
    m_TE.push_back(e);
}

void Arbre::afficher()
{
    //Affichage de tous les ensembles existant dans l'arbre
    //C'est la fonction afficher de la classe Ensemble que nous allons appeler ici
    for(int c=0;c<(int) m_TE.size();c++)
    {
        cout<<"E"<<c<<" = ";
        m_TE.at(c).afficher();
        cout<< endl;
    }
}

void Arbre::afficherEtGetFils(int niveau,int numero)
{
    //m_numEnsembleActuel nandray valeur 0 tany @ constructeur par defaut de l'Arbre
    //Ajouter l'élément qui se trouve au niveau et au numero de l'ensemble m_TE dans le tableau d'ensemble finaux
    //m_TEnsembreFinaux qui va contenir le résultat du produit cartésien final
    m_TEnsembreFinaux.at(m_numEnsembleActuel).ajouter((m_TE.at(niveau).at(numero)));
    //Si ce niveau a des fils
    if(getFilsExiste(niveau))
    {
        //Répéter l'ajout au déssus, jusqu'au nombre de fils du niveau actuel
        for(int c=0;c<getNbFils(niveau);c++)
            afficherEtGetFils(niveau+1,c);
    }
    else//S'il n'y a pas de fils dans le niveau
    {
        //======================="pas de fils"=========================
          m_numEnsembleActuel++;
    }
    /*En résumer ce  fonction va tester l'élément du niveau s'il a des fils. Au cas où ce teste est vrai il va ajouter tous les
     premiers fils après son père. Dans ce cas là, il va ajouter un à un aussi tous les derniers fils dans le m_TEnsembreFinaux
     avec des m_numEnsembleActuel différents. C'est après cela que ce même teste va passer au père suivant des derniers fils et
     refaire la même action. Si tous les pères des dérniers fils ont tous déjà testé, le teste va passer au père suivant des
     pères des derniers fils et refaire les mêmes actions. Ce teste se teminera au dernier élément du plus petit niveau. */

}

void Arbre::produire()
{
    //récupération du nombre de combinaison possible
    m_nbEnsembleFInaux=1;
    for(int c=0;c<(int)m_TE.size();c++)
        m_nbEnsembleFInaux*=m_TE.at(c).size();
    //==============================================

    for(int c=0;c<=m_nbEnsembleFInaux;c++)
    {
        //On va insérer des ensembles vides dans l'ensemble finaux nommé m_TEnsembreFinaux
        Ensemble et;
        m_TEnsembreFinaux.push_back(et);
    }

    for(int c=0;c<(int) m_TE.at(0).size();c++)
    {
        afficherEtGetFils(0,c);
    }
//=========================Teste si tous les éléments de chaque niveau sont complets=========================================
//=============Au cas où le teste est vrai, on doit les completer pour atteindre le résultat finale==========================
   int occurence=1;
   for(int c=1;c<(int)m_TE.size();c++)
        occurence*=m_TE.at(c).size();

   for(int d=0;d<m_nbEnsembleFInaux;d+=occurence)
    {
        for(int c=d+1;c<d+occurence;c++)
        {
            //Si la taille de l'ensemble du niveau c est différente à celle de son père
            if(m_TEnsembreFinaux.at(c).size()!=m_TEnsembreFinaux.at(c-1).size())
            {
                //On doit calculer la différence. Le résultat va être le nombre d'éléments manquants à ce niveau c
                int difference= m_TEnsembreFinaux.at(c-1).size()-m_TEnsembreFinaux.at(c).size();
                //Il faut ajouter maintenant tous ces éléments manquants au niveau c
                for(int e=difference-1;e>=0;e--)
                {
                    m_TEnsembreFinaux.at(c).ajouterAuDebut(m_TEnsembreFinaux.at(c-1).at(e));
                }
            }
        }
    }
//===============================================================================================================================

    //Affiche chaque element de l'ensemble finaux
    for(int c=0;c<=m_nbEnsembleFInaux;c++)
        m_TEnsembreFinaux.at(c).afficher();
}

int Arbre::getNbFils(int niveau)
{
    return m_TE.at(niveau+1).size();
}

bool Arbre::getFilsExiste(int niveau)
{
    bool existe;
    existe=false;
    if(niveau<(int) m_TE.size()-1)
        existe=true;
    return existe;

}

Arbre::~Arbre()
{

}
