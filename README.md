# 2NET Semestre 2 - Mini projet individuel
Année académique 2014 - 2015

## Contexte
Ce mini projet que vous êtes entrain de faire est basée sur un cas d'étude : 

OnMyWay est une compagnie connue qui gère des restaurants dans plusieurs villes. Après huit années d'expansion de leurs infrastructures, OnMyWay possèdent aujourd'hui 24 restaurants en France.

Les serveurs de la société n'ont aucun logiciel pour s'occupée des plats pris et des additions. Vous devez créer une application pour les aider sur la base journalière.

Soyez prudent, l'UX est très importante.

## Ce qui est attendue
**Usage de C#, WPF et Entity Framework**, vous devez implémenter les fonctionnalités suivantes.

Votre application dois : 
* Possédez une interface graphique globale affichant : 
  * Le plan du restaurant avec les tables et les chaises;
  * Le status de chaque table « Eating » ou « Empty ».
    * Le status « Eating » est affecté lorsque la commande est prise et avant que les plats n'aient été payés.
    * Le status « Empty » est affecté dans tous les autres cas.
* Afficher une autre interface après avoir cliqué sur une table « Empty » :
  * Qui affichera tous les plats pour créer une nouvelle commande;
  * Qui affichera tous les serveurs et serveuses afin de selectionner celui ou celle choisi.
* Afficher une autre interface après avoir cliqué sur une table « Eating » :
  * Qui affichera tous les plats pris ainsi que la facture.
* Afficher un boutton « Admin panel » accessible sans aucune vérification des pouvoirs : 
  * Ajouter, modifier ou supprimer des plats ;
  * Ajouter, modifier ou supprimer des serveurs ou serveuses ;
  * Modifier le plan du restaurants ;
  * Afficher les statistiques des serveurs et serveuses :
    * Par tables pris en charge ;
    * Par argent gagnés.

**L'application doit en moyenne ressembler à « Modern UI ».**

## Ce que vous devez faire et savoir
### Comprendre les attentes de votre client
Avant de commencer, soyez sûre de bien comprendre les attentes de votre client et de prendre le temps de réfléchir sur le sujet. Si vous en sentez le besoin, écrivez des trucs et dessinez des schémas.

**PENSEZ SIMPLE! N'ALLEZ PAS DANS LA COMPLEXITÉ.**

### Un peu d'aide
La MSDN est une encyclopédie assez bien documentée avec beaucoup de morceau de code et d'exemples.

### Un petit rappel
Les mini-projets ont une première phase de recherche. Quelques unes des taches demandées ne sont pas dans vos leçons ; Vous aurez probablement besoin de faire quelques recherche pour cela.

N.B. : Chercher quelque chose sur Google n'est pas de copier et coller des bouts de code. Lisez, comprenez, et faites en votre propre version répondant à vos besoins. Les copier-coller seront considérés comme de la triche et du vol.

### La présentation
La présentation dure 10 minutes.

Vous allez préparer un diaporama de présentation et une démo ou plus de votre projet répondant aux critères précédemment expliqués. Pour cet oral, votre évaluateur serra considéré comme le manager de cette société. En considérant que cette société n'est pas une société d'informatique, utilisez vos talents pour démontrer la qualité de votre projet sans trop argumenter sur ce qu'il y a sous le capot.

## Évaluation et rendu
###Pas de triche
Ceci est un projet de groupe, cependant vous n'êtes pas autorisé d'héberger votre projet en ligne, d'utilisez le code des autres groupes ou copier coller du code d'internet. Si quelqu'un est pris entrain de tricher, il recevra la note de 0 avec la mention « cheater ».

###Répartition des points
Votre application est évalué sur le code et la présentation.

Le code est évalué sur une base de 25 points.

La présentation est évalué sur une base de 15 points.

| Objectifs                                   | Points |
| :-----------------------------------------: | :----: |
| L'interface globale                         | 2      |
| Globale : Le plan du restaurant             | 2      |
| Globale : Le status des tables              | 2      |
| L'interface d'ajout d'une nouvelle commande | 3      |
| L'interface de paiement de la facture       | 3      |
| Admin : CRUD des plats                      | 1      |
| Admin : CRUD des serveurs et serveuses      | 1      |
| Admin : Modifier le plan du restaurant      | 5      |
| Admin : Statistique                         | 3      |
| Un bon design UI (modern UI)                | 3      |

| Présentation                                                                 | Points |
| :--------------------------------------------------------------------------: | :----: |
| La présentation est correctement préparée (Powerpoint, ...)                  | 2      |
| La démo de l'application est fonctionnelle (pas de bugs, pas de craches)     | 5      |
| Le développeur justifie du choix de l'UX                                     | 5      |
| Le développeur peux présenter des perspectives d'évolutions de l'application | 2      |
| Répartition des temps de parole                                              | 1      |

### Le rendu
Dès que vous avez terminé le projet, envoyez le par e-mail à votre **professeur** avec l'objet [MP22NET] < id > - < name > < firstname > - < campus >. N'oubliez pas d'ajouter la boite mail 2NET@supinfo.com dans le champs « copie carbonne » (cc)

Par exemple : 

**[MP22NET] 59263 - BERTHIER Renaud - LILLE**

**Vous n'avez pas besoin d'inclure la présentation dans votre rendu.**

### Date limite
Le projet doit être rendu au plus tard le 10 février 2015 23:59 (heure de Paris)

**Bonne Chance!**
