# LauncherBorneArcade
	The Launcher allows you to view club news and launch all 7Fault games from one place.

## Utilisation:
	

	Structure à respecter
		├── Launcher
		|	├── Games
		|	|	├── Jeu1
		|	|	|	├── GameMeta
		|	|	|	|	├── logo.png
		|	|	|	|	├── demo.mp4
		|	|	|	|	├── controls.txt
		|	|	|	|	├── description.txt
		|	|	|	├── ...
		|	|	├── Jeu2
		|	|	|	├── GameMeta
		|	|	|	|	├── logo.png
		|	|	|	|	├── demo.mp4
		|	|	|	|	├── controls.txt
		|	|	|	|	├── description.txt
		|	|	|	├── ...
		|	|	├── ...
		|	|	├── news.txt
		|	├── ...


	For the games to be detected by the launcher, you just have to drag their builds in the Games/ folder.

	There is a zone for the news of the club (the text displayed is the one of Games/news.txt)

	For the logo of the game to be displayed, you have to put a image file named logo.png file in Games/<game name>/GameMeta/ (otherwise a default 7Fault logo will be shown).
	The logo will be displayed as a square, so it is best that the image is squared.

## Logo.png
	Un logo du jeu qui sera affiché comme icône.

## demo.mp4
	Une vidéo au format 16x9 qui sera affichée dans l'écran d'information pour présenter le gameplay du jeu.
	Ne mettre que du gameplay et privilégier une durée courte (max 20 secondes).

## description.txt
	Un fichier texte dont le contenu sera affiché dans l'écran d'information pour présenter le jeu.
	Limiter la taille maximale du texte à une dizaine de lignes au maximum.

## controls.txt
	Un fichier texte qui contient toutes les informations caractéristiques au jeu, à savoir :
	- Le nombre de joueurs
	- Le genre du jeu
	- Le / les collections dont le jeu fait partie (facultatif)
	- Les contrôles

	Le texte doit utiliser un format similaire à du XML à l'exception du fait que les balises de fin et de début sont identiques.

	Exemple :

	<nbJoueurs>1<nbJoueurs>
	<Genre>Platformer<Genre>
	<Collection>Jeux sous le sapin 2022<Collection>
	

### <nbJoueurs>




## TODO:
	-Classement des jeux les plus lancés/nouveaux pour les mettre en premier
	-extract l'image de l'exe si elle n'existe pas dans le GameMeta (https://ourcodeworld.com/articles/read/1422/how-to-extract-the-icon-from-an-executable-with-c-sharp-in-winforms)
	-mettre tous les jeux dans un git pour pouvoir en ajoutant et en update facilement 
	-on pourrait même envisager d'avoir un script qui pull automatiquement quand on lance le launcher
	-Design du panneau de contrôle du volume

## Bug à fix/préventions:
	-Mettre les cadres de preview de jeu gris quand nbgames < 3
	-Tester pour choisir le bon exe si il y en a plusieurs dans les dossiers du jeu
	-Trycatch pour les loadsAssets ?

	
## Last Update:
	#Icone pour le launcher
	#Changement du fond d'écran
	#Changement de la police
	#Correction d'un memory leak
	#Optimisation via la mise en cache les textures pour éviter de les reload
	#Affichage secondaire des jeux en grille
	#Support pour afficher la description des jeux
	#Contrôle du volume système depuis le launcher
	#Hold pour faire défiler
	#Affichage des contrôles du jeu
	#Affichage d'une vidéo demo
	#Support du tri par nombre de joueurs
	#Support du tri par genre
	#Support du tri par collection
	#Possibilité d'avoir plusieurs news qui s'affichent à tour de rôle
	#Affichage de tous les jeux en mode debug
	#Affichage des collections en grille
