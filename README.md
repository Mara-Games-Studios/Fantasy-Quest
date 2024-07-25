# Paparaz Quetka
Simple single-player fantasy adventure 2D game based on Belarusian folklore. Made with Unity by [Mara Games](https://github.com/Mara-Games-Studios)!

Genre: *Adventure, Quest*

Tags: *2D, Indie, Storytelling, Single-player, Fairytale, Folklore.*

# Table of Contents
- [Overview](##Overview)
  - [Play](##Play)
  - [Credits](##Credits)
    
- [Screenshots](#Screenshots)
- [Project Installation](#Project-Installation)
- [Code Conventions](#Code-Convensions)
   - [Formatting](##Formatting)
   - [Code Style Rules](##Code-Style-Rules)
   - [Continious Intergation](##Continious-integration)
     
# Overview
Paparats Kvetka is a quest game about a boy Simon. His gole is to save his little sister from horrible disease. In order to find the cure he goes on an adventure on Kupala night, when there is a chance to find magic flower. 

The game is a classic point and click quest with elements of dynamic mini games and arcade style. 

The player will take on the role of a cat who helps Simon in achieving his goals in a colorful Belarusian mythical world. Player's goal is to protect the boy from dangers and help him in solving puzzles and communicating with various fairy tale creatures.

## Play

Still in development ! Stay tuned !

## Credits

We are Mara Games!

Game design: Voronovich Roman, Novikov Andrey;

Art: Baranchik-Krasovskaya Marina, Zinovenko Diana, Nadeeva Anna, Pasyutina Kseniya;

Programming: Grincevich Kseniya, Krivecki Igor', Papeta Maksim, Khimich Nikolay.

# Screenshots

<p align="center">
</p>

# Project Installation
To Install and Run

Clone repository
```
git clone https://github.com/Mara-Games-Studios/Fantasy-Quest
```
And open project with `Unity 2022.3.12f1`

# Code Convensions

## Formatting

### *General provisions*
Project uses two formatters: `.NET format` and `CSharpier`.

* Rules for `.NET format` descripted in [.editorconfig](https://github.com/Mara-Games-Studios/Fantasy-Quest/blob/main/Fantasy-Quest/.editorconfig) file.
* Rules for `CSharpier` descripted in [.csharpierrc.json](https://github.com/Mara-Games-Studios/Fantasy-Quest/blob/main/Fantasy-Quest/.csharpierrc.json) file.

<b>.Net format</b> it's a buildin formatter for .NET and can be found [here](https://github.com/dotnet/format).\
<b>CSharpier</b> it's a tool that's can be found [here](https://csharpier.com/) and installed for any popular IDE.

### *CI integration*

Project uses Continious Integration with Github Actions.\
Both <b>.NET format</b> and <b>CSharpier</b> checks included in CI checks and *triggers when any pull request for main branch* was created.

### *Visual Studio configuration*

For comfortable developing, in visual studio recomended configure next settings:
1. Set flags in VS settings: 

    * Tools | Options | Text Editor | General | Follow projects coding convensions
    * Tools | Options | Text Editor | CodeClenup | Run Code cleanup profile on save

2. Download CSharpier VS Extention:

    * Download and install for Visual Studio follow by [offisial site](https://marketplace.visualstudio.com/items?itemName=csharpier.CSharpier) instructions
    * Configure Reformat with CSharpier on Save under Tools | Options | CSharpier | General

## Code style rules

### Naming

Project's naming convention follows by [this](https://unity.com/how-to/naming-and-code-style-tips-c-scripting-unity) article's recomendatins, exept that private class fields do not use underlying prefix.

### Namespace

1. All scripts must have namespace and be placed in relevant for that script folder.
2. Namespace must match file path to script excluding `Assets` and `Scripts` folders.

### AddComponentMenu

1. All monobehaviour scripts must have `[AddComponentMenu("")]` attribute.
2. Label in attribute must have two parts:
    * Namespace separated with `/` that started with `Scripts`
    * Component name that comtains namespace with class name
```csharp
    // Example attribute
    [AddComponentMenu("Scripts/Level/Boss/Level.Boss.Model")]
``` 

For comfortable executing this rule, shud be used [This Unity Package](https://github.com/Kiuh/Item-Templates-For-Unity) or [This Visual Studio Extention](https://marketplace.visualstudio.com/items?itemName=nikolay-khimich.unity-class-template).

## Continious integration
The project uses 2 ci parts, one for formatting that described in *Formatting* section, one for build unity project.

### Unity Build CI
After any pull request for main branch, CI try to build project for Windows x32 and x64, and tells if build was failed.
After pull request is merged into main branch, CI create two artifacts for each tested build that can be downloaded.

