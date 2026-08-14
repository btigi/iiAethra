iiAethra
=========

iiAethra is a C# library supporting the modification of files relating to The Aethra Chronicles, the 1994 CRPG game.


| Name          | Read | Write | Comment |
|---------------|:----:|-------|:--------|
| AETHRA.CFG    | ✔   |   ✔   |
| C1.RSC        | ✔   |   ✔   | City/world visual tiles (PIC1 indices), 36 screens
| C2.RSC        | ✔   |   ✔   | City/world gameplay layers, 36 screens
| CHARPIC.DAT   | ✔   |   ✔   |
| D1.RSC        | ✔   |   ✔   | Dungeon layout (PIC1 indices from 1440), 108 screens
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
| MAP.RSC       | ✔   |   ✔   | Dungeon fog-of-war, 99 screens
| MAPS.PIC      | ✔   |   ✔   |
| MONPIC.PIC    | ✔   |   ✔   | Multiple image sizes
| NMONSTER.DAT  | ✔   |   ✔   |
| OPEN.PPC      | ✗   |   ✗   |
| PARCH.PIC     | ✔   |   ✔   |
| PARTY.DAT     | ✔   |   ✔   |
| PIC1.RSC      | ✔   |   ✔   | Map tiles via Graphics4
| PILLAR.PPR    | ✔   |   ✔   | Title-screen framing
| PORTS.RSC     | ✔   |   ✔   |
| ROS.RSC       | ✔   |   ✔   | NPC roster, 64 characters
| SAVEGAME.DAT  | ✔   |   ✔   |
| SPECIALS.PIC  | ✔   |   ✔   |
| SPEFFS.DAT    | ✔   |   ✔   |
| STD.RSC       | ✔   |   ✗   | Sounds
| STORES.DAT    | ✔   |   ✔   |
| T1.RSC        | ✔   |   ✔   |
| TASKDESC.DAT  | ✔   |   ✔   | Quest / dialog pages
| TREE.DAT      | ✔   |   ✔   | Combat scenery sprites

Note: Real48 round-tripping is currently inaccurate.


## Usage

Install the [nuget package](https://www.nuget.org/packages/ii.Aethra/) e.g.

`dotnet add package ii.Aethra`

Aethra Chronicles doesn't have consistent file type extensions e.g. a DAT file can contain Guild and Shop info, scroll text, item information, quest description, encounters or more, each in differernt formats. Despite this there's a clear link between the file you want to edit and the class you'll need to edit it.

To edit a file you should instantiate the relevant class and call the `Read` method passing the filename. This will return an object model, which you can amend, before calling the `Write` method.

```csharp
using System.Text;
using ii.Aethra;
using SixLabors.ImageSharp;

const string GamePath = @"D:\Games\Aethra";
var g4 = new Graphics4();

// --- AETHRA.CFG
var cfgReader = new Cfg();
var cfg = cfgReader.Read(Path.Combine(GamePath, "aethra.cfg"));




// --- C1.RSC (city / outdoors)
const string C1OutputPath = @"D:\data\Aethra\c1";
Directory.CreateDirectory(C1OutputPath);

var renderer = new MapRenderer();
var c1Images = renderer.RenderC1(Path.Combine(GamePath, "PIC1.RSC"), Path.Combine(GamePath, "C1.RSC"));

// Save each screen
for (var screenIndex = 0; screenIndex < c1Images.Count; screenIndex++)
{
    var sector = screenIndex / MapLayout.ScreensPerSector;
    var screenInSector = screenIndex % MapLayout.ScreensPerSector;
    var path = Path.Combine(C1OutputPath, $"C1_s{sector:D2}_{screenInSector:D2}_{screenIndex:D2}.png");
    c1Images[screenIndex].SaveAsPng(path);
}

// Save each sector
var c1Sectors = renderer.StitchSectors(c1Images, C1Rsc.SectorCount);
for (var sector = 0; sector < c1Sectors.Count; sector++)
{
    var sectorPath = Path.Combine(C1OutputPath, $"C1_sector_{sector:D2}.png");
    c1Sectors[sector].SaveAsPng(sectorPath);
}

// Save the world
var worldImage = renderer.StitchC1World(c1Images);
var worldPath = Path.Combine(C1OutputPath, "C1_world.png");
worldImage.SaveAsPng(worldPath);




// --- C1.RSC (raw data)
var c1Screens = new C1Rsc().Read(Path.Combine(GamePath, "C1.RSC"));

var c1SummaryPath = Path.Combine(C1OutputPath, "C1_layers.txt");
using (var writer = new StreamWriter(c1SummaryPath))
{
    writer.WriteLine($"# C1.RSC layer dump — {c1Screens.Count} screens, {C1Rsc.LayerCount} layers");
    writer.WriteLine("# raw values are 1-based PIC1 tile ids; pic1 is the layer 0 PIC1 index");
    writer.WriteLine("# screen sector screenInSector x y pic1 raw0 raw1 raw2 raw3");
    for (var screenIndex = 0; screenIndex < c1Screens.Count; screenIndex++)
    {
        var screen = c1Screens[screenIndex];
        var sector = screenIndex / MapLayout.ScreensPerSector;
        var screenInSector = screenIndex % MapLayout.ScreensPerSector;
        for (var tileY = 0; tileY < MapLayout.ScreenHeight; tileY++)
        {
            for (var tileX = 0; tileX < MapLayout.ScreenWidth; tileX++)
            {
                var any = false;
                for (var layer = 0; layer < C1Rsc.LayerCount; layer++)
                {
                    if (screen.GetTile(layer, tileX, tileY) != 0)
                    {
                        any = true;
                        break;
                    }
                }

                if (!any)
                {
                    continue;
                }

                writer.Write($"{screenIndex}\t{sector}\t{screenInSector}\t{tileX}\t{tileY}\t{screen.GetPic1Index(0, tileX, tileY)}");
                for (var layer = 0; layer < C1Rsc.LayerCount; layer++)
                {
                    writer.Write($"\t{screen.GetTile(layer, tileX, tileY)}");
                }

                writer.WriteLine();
            }
        }
    }
}




// --- C2.RSC (raw data)
const string C2OutputPath = @"D:\data\Aethra\c2";
Directory.CreateDirectory(C2OutputPath);

var c2Screens = new C2Rsc().Read(Path.Combine(GamePath, "C2.RSC"));

var c2SummaryPath = Path.Combine(C2OutputPath, "C2_layers.txt");
using (var writer = new StreamWriter(c2SummaryPath))
{
    writer.WriteLine($"# C2.RSC layer dump — {c2Screens.Count} screens, {C2Rsc.LayerCount} layers");
    writer.WriteLine("# raw values are gameplay flags");
    writer.WriteLine("# screen sector screenInSector x y raw0 raw1 raw2");
    for (var screenIndex = 0; screenIndex < c2Screens.Count; screenIndex++)
    {
        var screen = c2Screens[screenIndex];
        var sector = screenIndex / MapLayout.ScreensPerSector;
        var screenInSector = screenIndex % MapLayout.ScreensPerSector;
        for (var tileY = 0; tileY < MapLayout.ScreenHeight; tileY++)
        {
            for (var tileX = 0; tileX < MapLayout.ScreenWidth; tileX++)
            {
                var any = false;
                for (var layer = 0; layer < C2Rsc.LayerCount; layer++)
                {
                    if (screen.GetValue(layer, tileX, tileY) != 0)
                    {
                        any = true;
                        break;
                    }
                }

                if (!any)
                {
                    continue;
                }

                writer.Write($"{screenIndex}\t{sector}\t{screenInSector}\t{tileX}\t{tileY}");
                for (var layer = 0; layer < C2Rsc.LayerCount; layer++)
                {
                    writer.Write($"\t{screen.GetValue(layer, tileX, tileY)}");
                }

                writer.WriteLine();
            }
        }
    }
}




// --- CHARPIC.DAT (character portraits)
const string CharOutputPath = @"D:\data\Aethra\charpic";
Directory.CreateDirectory(CharOutputPath);
var portaits = g4.Read(@"D:\Games\aethra\CHARPIC.DAT", new List<(int width, int height, int count)>() { (24, 28, -1) });
for (int i = 0; i < portaits.Count; i++)
{
    var portrait = portaits[i];
    portrait.SaveAsPng(Path.Combine(CharOutputPath, $"CHARPIC_{i}.png"));
}




// --- D1.RSC (dungeon floors)
const string D1OutputPath = @"D:\data\Aethra\d1";
const string MapOutputPath = @"D:\data\Aethra\map";
Directory.CreateDirectory(D1OutputPath);
Directory.CreateDirectory(MapOutputPath);

var d1Images = renderer.RenderD1(Path.Combine(GamePath, "pic1.rsc"), Path.Combine(GamePath, "d1.rsc"));

// Save each screen
for (var screenIndex = 0; screenIndex < d1Images.Count; screenIndex++)
{
    var sector = screenIndex / MapLayout.ScreensPerSector;
    var screenInSector = screenIndex % MapLayout.ScreensPerSector;
    var path = Path.Combine(D1OutputPath, $"D1_s{sector:D2}_{screenInSector:D2}_{screenIndex:D2}.png");
    d1Images[screenIndex].SaveAsPng(path);
}

// Save each sector
var d1Sectors = renderer.StitchSectors(d1Images, D1Rsc.SectorCount);
for (var sector = 0; sector < d1Sectors.Count; sector++)
{
    var sectorPath = Path.Combine(D1OutputPath, $"D1_sector_{sector:D2}.png");
    d1Sectors[sector].SaveAsPng(sectorPath);
}




// --- D1.RSC (raw data)
var d1Screens = new D1Rsc().Read(Path.Combine(GamePath, "D1.RSC"));

var d1SummaryPath = Path.Combine(D1OutputPath, "D1_layers.txt");
using (var writer = new StreamWriter(d1SummaryPath))
{
    writer.WriteLine($"# D1.RSC layer dump — {d1Screens.Count} screens, {D1Rsc.LayerCount} layers");
    writer.WriteLine("# raw values are 1-based dungeon tile ids; pic1 is the layer 0 PIC1 index");
    writer.WriteLine("# screen sector screenInSector x y pic1 raw0 raw1 raw2 raw3 raw4 raw5");
    for (var screenIndex = 0; screenIndex < d1Screens.Count; screenIndex++)
    {
        var screen = d1Screens[screenIndex];
        var sector = screenIndex / MapLayout.ScreensPerSector;
        var screenInSector = screenIndex % MapLayout.ScreensPerSector;
        for (var tileY = 0; tileY < MapLayout.ScreenHeight; tileY++)
        {
            for (var tileX = 0; tileX < MapLayout.ScreenWidth; tileX++)
            {
                var any = false;
                for (var layer = 0; layer < D1Rsc.LayerCount; layer++)
                {
                    if (screen.GetRaw(layer, tileX, tileY) != 0)
                    {
                        any = true;
                        break;
                    }
                }

                if (!any)
                {
                    continue;
                }

                writer.Write($"{screenIndex}\t{sector}\t{screenInSector}\t{tileX}\t{tileY}\t{screen.GetPic1Index(0, tileX, tileY)}");
                for (var layer = 0; layer < D1Rsc.LayerCount; layer++)
                {
                    writer.Write($"\t{screen.GetRaw(layer, tileX, tileY)}");
                }

                writer.WriteLine();
            }
        }
    }
}




// --- DRAGONS.PIC
const string DragonsOutputPath = @"D:\data\Aethra\dragons";
Directory.CreateDirectory(DragonsOutputPath);

var dragons = g4.Read(Path.Combine(GamePath, "DRAGONS.PIC"), new List<(int width, int height, int count)>() { (120, 120, -1) });
for (int i = 0; i < dragons.Count; i++)
{
    var portrait = dragons[i];
    portrait.SaveAsPng(Path.Combine(DragonsOutputPath, $"DRAGONS_{i}.png"));
}




// --- ENCNTER.DAT
var encounterReader = new EncnterDat();
var encounters = encounterReader.Read(Path.Combine(GamePath, "ENCNTER.DAT"));




// --- FLOOR.PIC
const string FloorOutputPath = @"D:\data\Aethra\floor";
Directory.CreateDirectory(FloorOutputPath);

var floors = g4.Read(Path.Combine(GamePath, "FLOOR.PIC"), [
  (16, 16, -1),
]);
for (int i = 0; i < floors.Count; i++)
{
    var portrait = floors[i];
    portrait.SaveAsPng(Path.Combine(FloorOutputPath, $"FLOOR_{i}.png"));
}



// --- FRONTS.PIC
const string FrontsOutputPath = @"D:\data\Aethra\front";
Directory.CreateDirectory(FrontsOutputPath);

var fronts = g4.Read(Path.Combine(GamePath, "FRONTS.PIC"), [
  (180, 180, -1),
]);
for (int i = 0; i < fronts.Count; i++)
{
    var portrait = fronts[i];
    portrait.SaveAsPng(Path.Combine(FrontsOutputPath, $"FRONT_{i}.png"));
}




// --- ICONS.PIC
const string IconsOutputPath = @"D:\data\Aethra\icons";
Directory.CreateDirectory(IconsOutputPath);

var icons = g4.Read(Path.Combine(GamePath, "ICONS.PIC"), [
  (64, 74, 1),
  (44, 44, 1),
  (66, 22, 1),
  (22, 22, 4),
  (24, 24, 34),
]);
for (int i = 0; i < icons.Count; i++)
{
    var portrait = icons[i];
    portrait.SaveAsPng(Path.Combine(IconsOutputPath, $"ICON_{i}.png"));
}




// --- INFO1.DAT, INFO2.DAT
var infoDat = new InfoDat();
var records = infoDat.Read(Path.Combine(GamePath, "INFO1.DAT"));
records = infoDat.Read(Path.Combine(GamePath, "INFO2.DAT"));




// --- ITEM.DAT
var itemReader = new ItemDat();
var items = itemReader.Read(Path.Combine(GamePath, "ITEM.DAT"));




// --- MAP.RSC (fog-of-war)
var mapSource = Path.Combine(GamePath, "MAP.RSC");
var mapImages = renderer.RenderMap(mapSource);

for (var screenIndex = 0; screenIndex < mapImages.Count; screenIndex++)
{
    var sector = screenIndex / MapLayout.ScreensPerSector;
    var screenInSector = screenIndex % MapLayout.ScreensPerSector;
    var path = Path.Combine(MapOutputPath, $"MAP_s{sector:D2}_{screenInSector:D2}_{screenIndex:D2}.png");
    mapImages[screenIndex].SaveAsPng(path);
}

var mapSectorCount = (mapImages.Count + MapLayout.ScreensPerSector - 1) / MapLayout.ScreensPerSector;
var mapSectors = renderer.StitchSectors(mapImages, mapSectorCount);
for (var sector = 0; sector < mapSectors.Count; sector++)
{
    var sectorPath = Path.Combine(MapOutputPath, $"MAP_sector_{sector:D2}.png");
    mapSectors[sector].SaveAsPng(sectorPath);
}




// --- MAP.RSC (raw data)
var mapScreens = new MapRsc().Read(mapSource);

var mapSummaryPath = Path.Combine(MapOutputPath, "MAP_explored.txt");
using (var writer = new StreamWriter(mapSummaryPath))
{
    writer.WriteLine($"# MAP.RSC fog-of-war summary — {mapScreens.Count} screens (1 = explored)");
    writer.WriteLine("# screen sector screenInSector exploredTiles");
    for (var screenIndex = 0; screenIndex < mapScreens.Count; screenIndex++)
    {
        var screen = mapScreens[screenIndex];
        var explored = 0;
        for (var tileY = 0; tileY < MapLayout.ScreenHeight; tileY++)
        {
            for (var tileX = 0; tileX < MapLayout.ScreenWidth; tileX++)
            {
                if (screen.IsExplored(tileX, tileY))
                {
                    explored++;
                }
            }
        }

        var sector = screenIndex / MapLayout.ScreensPerSector;
        var screenInSector = screenIndex % MapLayout.ScreensPerSector;
        writer.WriteLine($"{screenIndex}\t{sector}\t{screenInSector}\t{explored}");
    }
}




// --- MAPS.PIC
const string MapsOutputPath = @"D:\data\Aethra\maps";
Directory.CreateDirectory(MapsOutputPath);

var mapPics = g4.Read(Path.Combine(GamePath, "MAPS.PIC"), [
  (192, 192, -1),
]);
for (int i = 0; i < mapPics.Count; i++)
{
    var portrait = mapPics[i];
    portrait.SaveAsPng(Path.Combine(MapsOutputPath, $"MAPS_{i}.png"));
}



// --- MONPIC.PIC
const string MonstersOutputPath = @"D:\data\Aethra\monsters";
Directory.CreateDirectory(MonstersOutputPath);

var monsterPics = g4.Read(Path.Combine(GamePath, "MONPIC.PIC"), [
  (24, 96, 450),
  (48, 48, 186),
]);
for (int i = 0; i < monsterPics.Count; i++)
{
    var portrait = monsterPics[i];
    portrait.SaveAsPng(Path.Combine(MonstersOutputPath, $"MONPIC_{i}.png"));
}



// --- NMONSTER.DAT
var monsterReader = new NMonsterDat();
var monsters = monsterReader.Read(Path.Combine(GamePath, "NMONSTER.DAT"));




// --- OPEN.PPC
// Unsupported




// --- PARCH.PIC
const string ParchOutputPath = @"D:\data\Aethra\parch";
Directory.CreateDirectory(ParchOutputPath);

var parchPics = g4.Read(Path.Combine(GamePath, "PARCH.PIC"), [
  (48, 48, -1),
]);
for (int i = 0; i < parchPics.Count; i++)
{
    var portrait = parchPics[i];
    portrait.SaveAsPng(Path.Combine(ParchOutputPath, $"PARCH_{i}.png"));
}




// --- PARTY.DAT
var partyReader = new PartyDat();
var party = partyReader.Read(Path.Combine(GamePath, "PARTY.DAT"));




// --- PIC1.RSC
const string Pic1OutputPath = @"D:\data\Aethra\pic1";
Directory.CreateDirectory(Pic1OutputPath);

var pic1Images = g4.Read(Path.Combine(GamePath, "PIC1.RSC"), new List<(int width, int height, int count)>() { (16, 16, -1) });
for (int i = 0; i < pic1Images.Count; i++)
{
    var pic1 = pic1Images[i];
    pic1.SaveAsPng(Path.Combine(Pic1OutputPath, $"PIC1_{i}.png"));
}




// --- PILLAR.PPR
const string PillarOutputPath = @"D:\data\Aethra\pillar";
Directory.CreateDirectory(PillarOutputPath);

var pillarReader = new PillarPpr();
var pillar = pillarReader.Read(Path.Combine(GamePath, "PILLAR.PPR"));
pillar.SaveAsPng(Path.Combine(PillarOutputPath, $"PILLAR.png"));




// --- PORTS.RSC
const string PortsOutputPath = @"D:\data\Aethra\ports";
Directory.CreateDirectory(PortsOutputPath);

var portraits = g4.Read(Path.Combine(GamePath, "PORTS.RSC"), new List<(int width, int height, int count)>() { (60, 60, -1) });
for (int i = 0; i < portraits.Count; i++)
{
    var portrait = portraits[i];
    portrait.SaveAsPng(Path.Combine(PortsOutputPath, $"PORTS_{i}.png"));
}



// --- ROS.RSC
var rosterReader = new PartyDat();
var roster = rosterReader.Read(Path.Combine(GamePath, "ROS.RSC"));




// --- SAVEGAME.DAT
//var saveGameReader = new SaveGameDat();
//var saveGame = saveGameReader.Read(Path.Combine(GamePath, "SAVEGAME.DAT"));




// --- SPECIALS.PIC
const string SpecialsOutputPath = @"D:\data\Aethra\specials";
Directory.CreateDirectory(SpecialsOutputPath);

var specialsPics = g4.Read(Path.Combine(GamePath, "SPECIALS.PIC"), [
  (24, 28, -1),
]);
for (int i = 0; i < specialsPics.Count; i++)
{
    var portrait = specialsPics[i];
    portrait.SaveAsPng(Path.Combine(SpecialsOutputPath, $"SPECIALS_{i}.png"));
}




// --- SPEFFS.DAT
const string SpellEffectsOutputPath = @"D:\data\Aethra\speffs";
Directory.CreateDirectory(SpellEffectsOutputPath);

var specialEffectsPics = g4.Read(Path.Combine(GamePath, "SPEFFS.DAT"), [
  (36, 108, -1),
]);
for (int i = 0; i < specialEffectsPics.Count; i++)
{
    var portrait = specialEffectsPics[i];
    portrait.SaveAsPng(Path.Combine(SpellEffectsOutputPath, $"SPEFFS_{i}.png"));
}



// --- STD.RSC (sounds)
const string StdOutputPath = @"D:\data\Aethra\std";
Directory.CreateDirectory(StdOutputPath);

var soundReader = new StdRsc();
var sounds = soundReader.Read(Path.Combine(GamePath, "STD.RSC"));
foreach (var sound in sounds)
{
    if (sound.Item2 == null)
        continue;
    var nullIndex = Array.IndexOf(sound.Item1.Filename, (byte)0);
    var filename = Encoding.ASCII.GetString(sound.Item1.Filename, 0, nullIndex < 0 ? sound.Item1.Filename.Length : nullIndex);
    File.WriteAllBytes(Path.Combine(StdOutputPath, $"{filename}"), sound.Item2);
}




// --- STORES.DAT (stores)
var storeReader = new StoresDat();
var store = storeReader.Read(Path.Combine(GamePath, "STORES.DAT"));





// --- T1.RSC
var t1Reader = new T1Rsc();
var t1 = t1Reader.Read(Path.Combine(GamePath, "T1.RSC"));




// --- TASKDESC.DAT (quest / conversation pages)
const string TaskDescOutputPath = @"D:\data\taskdesc";

var taskDescReader = new TaskDesc();
var tasks = taskDescReader.Read(Path.Combine(GamePath, "TASKDESC.DAT"));

for (var i = 0; i < tasks.Count; i++)
{
    var path = Path.Combine(TaskDescOutputPath, $"TASKDESC_{i:D3}.txt");
    File.WriteAllLines(path, tasks[i].Lines);
}




// --- TREE.DAT
const string TreeOutputPath = @"D:\data\Aethra\tree";
Directory.CreateDirectory(TreeOutputPath);

var treeReader = new TreeDat();
var trees = treeReader.Read(Path.Combine(GamePath, "TREE.DAT"));

for (var i = 0; i < trees.Count; i++)
{
    var path = Path.Combine(TreeOutputPath, $"TREE_{i:D2}.png");
    trees[i].Image.SaveAsPng(path);
}
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

Thanks to [The Aethra Chronicles](https://netsilik.nl/Aethra/fileInfo/)

The code is available in the [github repository](https://github.com/btigi/iiAethra)