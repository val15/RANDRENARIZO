Jeu d'échecs en c#------------------
Url     : http://codes-sources.commentcamarche.net/source/52342-jeu-d-echecs-en-cAuteur  : orioljimenezDate    : 03/08/2013
Licence :
=========

Ce document intitulé « Jeu d'échecs en c# » issu de CommentCaMarche
(codes-sources.commentcamarche.net) est mis à disposition sous les termes de
la licence Creative Commons. Vous pouvez copier, modifier des copies de cette
source, dans les conditions fixées par la licence, tant que cette note
apparaît clairement.

Description :
=============

Bonjour, 
<br />
<br />Comme beaucoup d'entre nous qui partagent la passion de
s &eacute;checs et celle de l'informatique, j'ai souvent eu l'envie de me penche
r sur la programmation des jeux d'&eacute;chec.
<br />C'est maintenant chose fa
ite, avec ce petit jeu dont je vous transmet la source.
<br />
<br />Les r&egr
ave;gles sont simples: 
<br />Dans un premier temps on choisit sa couleur, blan
cs ou noirs. La partie commence.
<br />On joue son coup en cliquant sur une pi&
egrave;ce et sur sa case d'arriv&eacute;e. Puis l'ordinateur joue son coup. Et a
insi de suite.
<br />La partie s'arr&ecirc;te en cas de chute de pendule (un jo
ueur n'a plus de temps) , de MAT ou de PAT.
<br />
<br />J'ai choisi le langag
e C# et .NET pour b&eacute;n&eacute;ficier de la facilit&eacute; &agrave; cr&eac
ute;er une interface graphique, de la programmation objet, et de performances ac
ceptables dans le calcul des coups.
<br />
<br />Le principe est le suivant : 

<br />Une partie est une succession de positions. Chaque position est constitu
&eacute;e de 64 cases, occup&eacute;es ou non. Ces 64 cases d&eacute;terminent l
es coups possibles pour cette position. Chaque coup entra&icirc;ne une nouvelle 
position de 64 cases, permettant de nouveaux coups, etc. 
<br />
<br />On a do
nc une arborescence de positions-coups, qui se d&eacute;veloppe tout au long de 
la partie. 
<br />Gr&acirc;ce &agrave; la programmation en objet de cette arbor
escence, il est possible de calculer plusieurs coups &agrave; l'avance par une s
imple boucle r&eacute;cursive sur les diff&eacute;rents coups et la position qu'
ils entrainent. 
<br />Cependant, en voulant appliquer l'algorithme MinMax (uti
lis&eacute; par presque tous les jeux d'&eacute;checs pour calculer le coup de l
'ordinateur), j'ai compris que le calcul de millions de coups, n&eacute;cessaire
 &agrave; un jeu intelligent de l'ordinateur, &eacute;tait beaucoup trop gourman
d en ressources m&eacute;moire. 
<br />
<br />J'ai donc opt&eacute; pour l'int
erconnexion du jeu avec un moteur UCI (Universal Chess Interface). Il s'agit d'u
n processus autonome &agrave; qui l'on transmet les coups du joueur sous forme d
e notation alg&eacute;brique, et qui calcule la r&eacute;ponse la plus appropri&
eacute;e. De nos jours, la majorit&eacute; des logiciels d'&eacute;chec emploien
t des moteurs UCI. Il en existe de nombreux et des comp&eacute;titions ont lieu 
r&eacute;guli&egrave;rement. J'ai choisi  StockFish (Licence GPL), qui est int&e
acute;gr&eacute; au jeu sous forme de ressource.
<br /><a name='source-exemple'
></a><h2> Source / Exemple : </h2>
<br /><pre class='code' data-mode='basic'>

Précisions sur le ZIP:

Source/Cases/ : Les classes des cases et pièces (roi,
 dame..) des positions de jeu. 
Source/Coups/ : Les classes des différents type
s de coup (prise, roque, etc)
Source/Joueur/ : Les classes des joueurs, le joue
ur Ordinateur gère le processus UCI.
Source/Dialogues/ : Les fenêtres de dialog
ue
Source/ControlesUtilisateurs/ : Les classes des éléments graphiques du jeu (
échiquier, pendule, feuille de jeu..)  
Source/Partie.cs : La classe principale
 du jeu. 
Source/Position.cs : La classe qui décrit une position de jeu. 
Reso
urces/stockfish.exe : L'exécutable du moteur UCI.
</pre>
<br /><a name='conclu
sion'></a><h2> Conclusion : </h2>
<br />J'ai baptis&eacute; le jeu &laquo; Sim
ple Fischer &raquo; en r&eacute;f&eacute;rence &agrave; son interface minimale e
t &agrave; la pendule type Fischer qui est employ&eacute;e. 
<br />
<br />Beau
coup d'&eacute;volutions seraient int&eacute;ressantes: 
<br />_ Historique des
 positions (possibilit&eacute; de revenir en arri&egrave;re apr&egrave;s une err
eur)
<br />_ Enregistrer/Charger une position 
<br />_ R&eacute;glage de la di
fficult&eacute; de jeu
<br />_ R&eacute;aliser son propre moteur UCI  (le langa
ge C me paraitrait id&eacute;al).
