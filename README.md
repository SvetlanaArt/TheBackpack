# TheBackpack
 
The Backpack module.

## Ver

Unity editor ver 2022.3.7f1

Assets:

- Text Mesh Pro 3.0.6
- DOTween (HOTween V2)  1.2.765
- UniTask 2.5.10

## Gameplay

The gameplay includes the ability to pick up and drag items into the backpack using Drag&Drop, as well as throwing them out of the backpack.

https://github.com/user-attachments/assets/648f984f-2a48-4df6-83fe-232d4354679b

## Structure

The project structure is organised with a generalised hierarchy. All resources (prefabs, textures, models, sounds) are stored in the **‘Game’** folder. All scripts, resources and objects in the scene have a generalised hierarchy of titles, organised into categories:

**Core/** - contains the main interfaces, manager classes and resources that define the core of the application
**EntryPoint/** - the entry point of the application
**Server/** - communication with the server
**GameElements/** - interactive game elements
**Environment/** - background game elements
**UI/** - interface elements

## Sources

The models are taken from https://assetstore.unity.com/
The sounds are taken from https://mixkit.co/free-sound-effects/
