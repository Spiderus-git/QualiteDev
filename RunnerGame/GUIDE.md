# Langage imposé : C#

## Livrables 

• Code source du projet sur un dépôt Git avec historique de commits.
• Rapport technique décrivant l’architecture, les choix techniques et les tests réalisés.
• Un accès à l’application ou le .exe du jeu

## Contexte

RunnerGame est un jeu de type runner infini développé en C# avec SFML et .NET.
Le joueur contrôle un personnage qui doit éviter les obstacles en courant et sautant, tout en accumulant des points.

## Prérequis

Windows (64 bits recommandé) ou Linux/macOS
.NET SDK 9.0+ installé
SFML.NET (CSFML) installé et accessible
Un terminal (cmd, PowerShell, bash)
Git (optionnel, pour récupérer le dépôt)

## Gameplay

Déplacement : ← et → pour bouger.
Saut : Espace pour sauter.
Pause/Menu : Échap pour revenir au menu.

## Objectif

Éviter les obstacles et accumuler des points en courant.
Ne pas perdre toutes ses vies sinon le jeu redémarre.

## Lancer le jeu

Lancer RunnerGame.exe

ou

Aller dans le bash

cd RunnerGame

dotnet clean

dotnet restore

dotnet build

dotnet run

## Tests Unitaires effectués avec NUnit et NUnit3TestAdapter

- Test de la gestion des collisions avec les obstacles (tests/CollisionTests)
- Test de la gestion du score (tests/ScoreTests)
- Test de la gestion des assets (Textures et police)(tests/AssetsTests)
- Test pour empêcher la sortie d’écran du joueur (tests/PlayerTests)

## Pour effectuer les tests dans le bash 

cd RunnerGame.tests

dotnet clean

dotnet restore

dotnet build

dotnet test
