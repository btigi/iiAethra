using ii.Aethra.Model;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ii.Aethra
{
    public class MapRenderer
    {
        public const int TileSize = 16;
        public const int GrassFirst = 69;
        public const int GrassCount = 10;
        public const int C1WorldSectorsWide = 2;
        public const int C1WorldSectorsHigh = 2;

        public const int ScreenWidthPx = MapLayout.ScreenWidth * TileSize;
        public const int ScreenHeightPx = MapLayout.ScreenHeight * TileSize;
        public const int SectorWidthPx = ScreenWidthPx * MapLayout.SectorWidthInScreens;
        public const int SectorHeightPx = ScreenHeightPx * MapLayout.SectorHeightInScreens;

        public List<Image> LoadPic1Tiles(string filename)
        {
            return new Graphics4().Read(filename, [(TileSize, TileSize, -1)]);
        }

        public List<Image> RenderC1(string pic1Filename, string c1Filename)
        {
            var tiles = LoadPic1Tiles(pic1Filename);
            var screens = new C1Rsc().Read(c1Filename);
            return RenderC1(screens, tiles);
        }

        public List<Image> RenderC2(string pic1Filename, string c2Filename)
        {
            var tiles = LoadPic1Tiles(pic1Filename);
            var screens = new C2Rsc().Read(c2Filename);
            return RenderC2(screens, tiles);
        }

        public List<Image> RenderD1(string pic1Filename, string d1Filename)
        {
            var tiles = LoadPic1Tiles(pic1Filename);
            var screens = new D1Rsc().Read(d1Filename);
            return RenderD1(screens, tiles);
        }

        public List<Image> RenderC1(IList<C1RscScreen> screens, IList<Image> pic1Tiles)
        {
            var tiles = ToRgba32(pic1Tiles);
            var result = new List<Image>(screens.Count);
            foreach (var screen in screens)
            {
                var image = CreateScreenImage();
                DrawPic1Screen(image, C1Rsc.LayerCount, screen.GetPic1Index, tiles, 0, 0, grassBase: 0);
                result.Add(image);
            }

            return result;
        }

        public List<Image> RenderC2(IList<C2RscScreen> screens, IList<Image> pic1Tiles)
        {
            var tiles = ToRgba32(pic1Tiles);
            var result = new List<Image>(screens.Count);
            foreach (var screen in screens)
            {
                var image = CreateScreenImage();
                DrawPic1Screen(image, C2Rsc.LayerCount, screen.GetPic1Index, tiles, 0, 0, grassBase: 0);
                result.Add(image);
            }

            return result;
        }

        public List<Image> RenderD1(IList<D1RscScreen> screens, IList<Image> pic1Tiles)
        {
            var tiles = ToRgba32(pic1Tiles);
            var result = new List<Image>(screens.Count);
            foreach (var screen in screens)
            {
                var image = CreateScreenImage();
                DrawD1Screen(image, screen, tiles, 0, 0);
                result.Add(image);
            }

            return result;
        }

        public List<Image> RenderMap(string filename)
        {
            var screens = new MapRsc().Read(filename);
            return RenderMap(screens);
        }

        public List<Image> RenderMap(IList<MapRscScreen> screens)
        {
            var result = new List<Image>(screens.Count);
            foreach (var screen in screens)
            {
                var image = CreateScreenImage();
                DrawMapFog(image, screen, 0, 0);
                result.Add(image);
            }

            return result;
        }

        public List<Image> StitchSectors(IList<Image> screens, int sectorCount)
        {
            var result = new List<Image>(sectorCount);
            for (var sector = 0; sector < sectorCount; sector++)
            {
                var sectorImage = new Image<Rgba32>(SectorWidthPx, SectorHeightPx, new Rgba32(0, 0, 0, 255));
                for (var screenInSector = 0; screenInSector < MapLayout.ScreensPerSector; screenInSector++)
                {
                    var screenIndex = sector * MapLayout.ScreensPerSector + screenInSector;
                    if (screenIndex >= screens.Count)
                    {
                        break;
                    }

                    var (screenCol, screenRow) = MapLayout.GetScreenPositionInSector(screenInSector);
                    DrawImage(sectorImage, screens[screenIndex], screenCol * ScreenWidthPx, screenRow * ScreenHeightPx);
                }

                result.Add(sectorImage);
            }

            return result;
        }

        public Image StitchC2World(IList<Image> screens) => StitchC1World(screens);

        public Image StitchC1World(IList<Image> screens)
        {
            var world = new Image<Rgba32>(SectorWidthPx * C1WorldSectorsWide, SectorHeightPx * C1WorldSectorsHigh, new Rgba32(0, 0, 0, 255));

            for (var sector = 0; sector < C1Rsc.SectorCount; sector++)
            {
                var sectorOriginX = (sector % C1WorldSectorsWide) * SectorWidthPx;
                var sectorOriginY = (sector / C1WorldSectorsWide) * SectorHeightPx;
                for (var screenInSector = 0; screenInSector < MapLayout.ScreensPerSector; screenInSector++)
                {
                    var screenIndex = sector * MapLayout.ScreensPerSector + screenInSector;
                    if (screenIndex >= screens.Count)
                    {
                        break;
                    }

                    var (screenCol, screenRow) = MapLayout.GetScreenPositionInSector(screenInSector);
                    DrawImage(world, screens[screenIndex], sectorOriginX + screenCol * ScreenWidthPx, sectorOriginY + screenRow * ScreenHeightPx);
                }
            }

            return world;
        }

        private static Image<Rgba32> CreateScreenImage() => new(ScreenWidthPx, ScreenHeightPx, new Rgba32(0, 0, 0, 255));

        private static void DrawPic1Screen(Image<Rgba32> image, int layerCount, Func<int, int, int, int> getPic1Index, IList<Image<Rgba32>> tiles, int originX, int originY, int grassBase)
        {
            for (var tileY = 0; tileY < MapLayout.ScreenHeight; tileY++)
            {
                for (var tileX = 0; tileX < MapLayout.ScreenWidth; tileX++)
                {
                    var destX = originX + tileX * TileSize;
                    var destY = originY + tileY * TileSize;
                    var grass = grassBase + GrassFirst + (tileX * 7 + tileY * 13) % GrassCount;
                    BlitTile(image, tiles, grass, destX, destY);

                    for (var layer = 0; layer < layerCount; layer++)
                    {
                        BlitTile(image, tiles, getPic1Index(layer, tileX, tileY), destX, destY);
                    }
                }
            }
        }

        private static void DrawD1Screen(Image<Rgba32> image, D1RscScreen screen, IList<Image<Rgba32>> tiles, int originX, int originY)
        {
            for (var layer = 0; layer < D1Rsc.LayerCount; layer++)
            {
                for (var tileY = 0; tileY < MapLayout.ScreenHeight; tileY++)
                {
                    for (var tileX = 0; tileX < MapLayout.ScreenWidth; tileX++)
                    {
                        BlitTile(
                            image,
                            tiles,
                            screen.GetPic1Index(layer, tileX, tileY),
                            originX + tileX * TileSize,
                            originY + tileY * TileSize);
                    }
                }
            }
        }

        private static void DrawMapFog(Image<Rgba32> image, MapRscScreen screen, int originX, int originY)
        {
            for (var tileY = 0; tileY < MapLayout.ScreenHeight; tileY++)
            {
                for (var tileX = 0; tileX < MapLayout.ScreenWidth; tileX++)
                {
                    var color = screen.IsExplored(tileX, tileY)
                        ? new Rgba32(255, 255, 255, 255)
                        : new Rgba32(0, 0, 0, 255);
                    var destX = originX + tileX * TileSize;
                    var destY = originY + tileY * TileSize;
                    for (var py = 0; py < TileSize; py++)
                    {
                        for (var px = 0; px < TileSize; px++)
                        {
                            image[destX + px, destY + py] = color;
                        }
                    }
                }
            }
        }

        private static void BlitTile(Image<Rgba32> dest, IList<Image<Rgba32>> tiles, int tileIndex, int destX, int destY)
        {
            if (tileIndex < 0 || tileIndex >= tiles.Count)
            {
                return;
            }

            var tile = tiles[tileIndex];
            for (var y = 0; y < TileSize; y++)
            {
                for (var x = 0; x < TileSize; x++)
                {
                    var pixel = tile[x, y];
                    if (pixel.R == 255 && pixel.G == 0 && pixel.B == 255)
                    {
                        continue;
                    }

                    dest[destX + x, destY + y] = pixel;
                }
            }
        }

        private static void DrawImage(Image<Rgba32> dest, Image source, int destX, int destY)
        {
            dest.Mutate(ctx => ctx.DrawImage(source, new Point(destX, destY), 1f));
        }

        private static List<Image<Rgba32>> ToRgba32(IList<Image> tiles)
        {
            var result = new List<Image<Rgba32>>(tiles.Count);
            foreach (var tile in tiles)
            {
                result.Add(tile as Image<Rgba32> ?? tile.CloneAs<Rgba32>());
            }

            return result;
        }
    }
}