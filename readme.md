iiAethra
=========

iiAethra is a C# library supporting the modification of files relating to The Aethra Chronicles, the 1994 CRPG game.


| Name          | Read | Write | Comment |
|---------------|:----:|-------|:--------|
| AETHRA.CFG    | ✔   |   ✔   |
| C1.RSC        | ✗   |   ✗   |
| C2.RSC        | ✗   |   ✗   |
| CHARPIC.DAT   | ✔   |   ✗   |
| D1.RSC        | ✗   |   ✗   |
| DRAGONS.PIC   | ✔   |   ✗   |
| ENCNTER.DAT   | ✔   |   ✔   |
| FLOOR.PIC     | ✔   |   ✗   |
| FRONTS.PIC    | ✔   |   ✗   |
| GAME.EXE      | ✗   |   ✗   |
| GAME.OVR      | ✗   |   ✗   |
| ICONS.PIC     | ✗   |   ✗   | Multiple image sizes
| INFO1.DAT     | ✔   |   ✔   | Malformed file - writing back the original data fails
| INFO2.DAT     | ✔   |   ✔   |
| ITEM.DAT      | ✔   |   ✗   |
| MAP.RSC       | ✗   |   ✗   |
| MAPS.PIC      | ✔   |   ✗   |
| MONPIC.PIC    | ✗   |   ✗   | Multiple image sizes
| NMONSTER.DAT  | ✔   |   ✗   |
| OPEN.PPC      | ✔   |   ✗   |
| PARCH.PIC     | ✔   |   ✗   |
| PARTY.DAT     | ✔   |   ✗   |
| PIC1.RSC      | ✔   |   ✗   |
| PILLAR.PPR    | ✗   |   ✗   |
| PORTS.RSC     | ✔   |   ✗   |
| ROS.RSC       | ✔   |   ✗   |
| SAVEGAME.DAT  | ✔   |   ✗   |
| SPECIALS.PIC  | ✔   |   ✗   |
| SPEFFS.DAT    | ✔   |   ✗   |
| STD.RSC       | ✔   |   ✗   | Some extracted files are malformed
| STORES.DAT    | ✔   |   ✗   |
| T1.RSC        | ✔   |   ✗   |
| TASKDESC.DAT  | ✗   |   ✗   | Malformed file
| TREE.DAT      | ✗   |   ✗   | Malformed file




## Download

Compiled downloads are not available.

## Compiling

To clone and run this application, you'll need [Git](https://git-scm.com) and [.NET](https://dotnet.microsoft.com/) installed on your computer. From your command line:

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