using ii.Aethra.Model;

namespace ii.Aethra
{
    // City / town tile maps
    // 36 screens (4 sectors × 9) of 24×16 cells stored column-wise, with 4 layers.
    // Each sector is preceded by a 9-byte header
    // A value is a 1-based index into the city bank of PIC1.RSC (value − 1 + 720 = tile index); 0 = empty
    // Empty cells show as a random plain-grass tile (PIC1 indices 789-798)
    public class C2Rsc
    {
        public const int ScreenCount = 36;
        public const int SectorCount = 4;
        public const int LayerCount = 4;
        public const int HeaderBytes = 9;
        public const int ScreenSizeBytes = MapLayout.TilesPerScreen * LayerCount * sizeof(short); // 3072
        public const int ExpectedFileSize = SectorCount * HeaderBytes + ScreenCount * ScreenSizeBytes; // 110628

        // PIC1.RSC holds three banks of 720 tiles: overworld (0), city (720), dungeon (1440)
        public const int CityTileBase = 720;

        public List<C2RscScreen> Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            return Read(br);
        }

        public List<C2RscScreen> Read(BinaryReader br)
        {
            var result = new List<C2RscScreen>(ScreenCount);
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                var header = br.ReadBytes(HeaderBytes);
                for (var screenInSector = 0; screenInSector < MapLayout.ScreensPerSector; screenInSector++)
                {
                    if (br.BaseStream.Position + ScreenSizeBytes > br.BaseStream.Length)
                    {
                        break;
                    }

                    var screen = new C2RscScreen();
                    if (screenInSector == 0)
                    {
                        screen.Header = header;
                    }

                    for (var layer = 0; layer < LayerCount; layer++)
                    {
                        for (var x = 0; x < MapLayout.ScreenWidth; x++)
                        {
                            for (var y = 0; y < MapLayout.ScreenHeight; y++)
                            {
                                screen.Layers[layer][y, x] = br.ReadInt16();
                            }
                        }
                    }

                    result.Add(screen);
                }
            }

            return result;
        }

        public void Write(List<C2RscScreen> screens, string filename)
        {
            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            using var bw = new BinaryWriter(fs);
            var sectorCount = (screens.Count + MapLayout.ScreensPerSector - 1) / MapLayout.ScreensPerSector;
            for (var sector = 0; sector < sectorCount; sector++)
            {
                var firstIndex = sector * MapLayout.ScreensPerSector;
                var header = firstIndex < screens.Count && screens[firstIndex].Header.Length == HeaderBytes
                    ? screens[firstIndex].Header
                    : new byte[HeaderBytes];
                bw.Write(header);

                for (var screenInSector = 0; screenInSector < MapLayout.ScreensPerSector; screenInSector++)
                {
                    var screenIndex = firstIndex + screenInSector;
                    if (screenIndex >= screens.Count)
                    {
                        break;
                    }

                    var screen = screens[screenIndex];
                    for (var layer = 0; layer < LayerCount; layer++)
                    {
                        for (var x = 0; x < MapLayout.ScreenWidth; x++)
                        {
                            for (var y = 0; y < MapLayout.ScreenHeight; y++)
                            {
                                bw.Write(screen.Layers[layer][y, x]);
                            }
                        }
                    }
                }
            }
        }
    }
}
