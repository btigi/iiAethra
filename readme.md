iiAethra
=========

iiAethra is a C# library supporting the modification of files relating to The Aethra Chronicles, the 1994 CRPG game.


| Name          | Read | Write | Comment |
|---------------|:----:|-------|:--------|
| AETHRA.CFG    | ✔   |   ✔   |
| C1.RSC        | ✗   |   ✗   |
| C2.RSC        | ✗   |   ✗   |
| CHARPIC.DAT   | ✔   |   ✔   |
| D1.RSC        | ✗   |   ✗   |
| DRAGONS.PIC   | ✔   |   ✔   |
| ENCNTER.DAT   | ✔   |   ✔   |
| FLOOR.PIC     | ✔   |   ✔   |
| FRONTS.PIC    | ✔   |   ✔   |
| GAME.EXE      | ✗   |   ✗   |
| GAME.OVR      | ✗   |   ✗   |
| ICONS.PIC     | ✔   |   ✔   | Multiple image sizes
| INFO1.DAT     | ✔   |   ✔   | Malformed file - writing back the original data fails
| INFO2.DAT     | ✔   |   ✔   |
| ITEM.DAT      | ✔   |   ✔   |
| MAP.RSC       | ✗   |   ✗   |
| MAPS.PIC      | ✔   |   ✔   |
| MONPIC.PIC    | ✔   |   ✔   | Multiple image sizes
| NMONSTER.DAT  | ✔   |   ✔   |
| OPEN.PPC      | ✔   |   ✔   |
| PARCH.PIC     | ✔   |   ✔   |
| PARTY.DAT     | ✔   |   ✔   |
| PIC1.RSC      | ✔   |   ✔   |
| PILLAR.PPR    | ✗   |   ✗   |
| PORTS.RSC     | ✔   |   ✔   |
| ROS.RSC       | ✔   |   ✔   |
| SAVEGAME.DAT  | ✔   |   ✔   |
| SPECIALS.PIC  | ✔   |   ✔   |
| SPEFFS.DAT    | ✔   |   ✔   |
| STD.RSC       | ✔   |   ✗   | Some extracted files are malformed
| STORES.DAT    | ✔   |   ✔   |
| T1.RSC        | ✔   |   ✔   |
| TASKDESC.DAT  | ✗   |   ✗   | Malformed file
| TREE.DAT      | ✗   |   ✗   | Malformed file

Note: Real48 round-tripping is currently inaccurate.


## Usage

Install the [nuget package](https://www.nuget.org/packages/ii.Aethra/) e.g.

`dotnet add package ii.Aethra`

Aethra Chronicles doesn't have consistent file type extensions e.g. a DAT file can contain Guild and Shop info, scroll text, item information, quest description, encounters or more, each in differernt formats. Despite this there's a clear link between the file you want to edit and the class you'll need to edit it.

To edit a file you should instantiate the relevant class and call the `Read` method passing the filename. This will return an object model, which you can amend, before calling the `Write` method.

```csharp
var itemManager = new ItemDat();
var items = itemManager.Read(@"D:\Games\aethra\install\item.dat");
items.First().WoodsLore = 1;
itemReader.Write(items, @"D:\Games\aethra\install\item.dat");
```

## Compiling

To clone and run this repository you'll need [Git](https://git-scm.com) and [.NET](https://dotnet.microsoft.com/) installed on your computer. From your command line:

```
# Clone this repository
$ git clone https://github.com/btigi/iiAethra

# Go into the repository
$ cd src

# Build  the app
$ dotnet build
```

## Licencing

iiAethra is licenced under the MIT License. Full licence details are available in licence.md

iiAethra uses [this](https://stackoverflow.com/a/64043637) Stackoverflow answer by [Phil Jollans](https://stackoverflow.com/users/1626109/phil-jollans) for conversion of a Turbao Pascal Real48 type to a C# type under the [CC BY-SA 4.0 license](https://creativecommons.org/licenses/by-sa/4.0/) as per the [Stackoverflow License agreement](https://stackoverflow.com/help/licensing).

The code is available in the [github repository](https://github.com/btigi/iiAethra)