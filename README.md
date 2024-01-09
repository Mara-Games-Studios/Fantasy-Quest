# Paparaz Quetka

A game in quest game genre with fantasy theme.

# Code convensions

## Formatting

### *General provisions*
Project uses two formatters: `.NET format` and `CSharpier`.

* Rules for `.NET format` descripted in [.editorconfig](https://github.com/Mara-Games-Studios/Fantasy-Quest/blob/main/Fantasy-Quest/.editorconfig) file.
* Rules for `CSharpier` descripted in [.csharpierrc.json](https://github.com/Mara-Games-Studios/Fantasy-Quest/blob/main/Fantasy-Quest/.csharpierrc.json) file.

<b>.Net format</b> it's a buildin formatter for .NET and can be found [here](https://github.com/dotnet/format).\
<b>CSharpier</b> it's a tool that's can be found [here](https://csharpier.com/) and installed for any popular IDE.

### *CI integration*

Project uses Continious Integration with Github Actions.\
Both <b>.NET format</b> and <b>CSharpier</b> checks included in CI checks and *triggers when any pull request in main* was created.

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