using ii.Aethra.Model;

namespace ii.Aethra
{
    // Dungeon map tile data.
    // 108 screens (12 sectors × 9) of 24×16 cells stored column-wise, with 6 layers.
    // Each sector is preceded by a 9-byte header
    public class D1Rsc
    {
        public const int ScreenCount = 108;
        public const int SectorCount = 12;
        public const int LayerCount = 6;
        // 9-byte header precedes each sector, not each screen
        public const int HeaderBytes = 9;
        public const int TrailingBytes = 768;
        public const int ScreenSizeBytes = MapLayout.TilesPerScreen * LayerCount * sizeof(short) + TrailingBytes; // 5376
        public const int ExpectedFileSize = SectorCount * HeaderBytes + ScreenCount * ScreenSizeBytes; // 580716

        public const int DungeonTileBase = 1440;

        public List<D1RscScreen> Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            return Read(br);
        }

        public List<D1RscScreen> Read(BinaryReader br)
        {
            var result = new List<D1RscScreen>(ScreenCount);
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                var header = br.ReadBytes(HeaderBytes);
                for (var screenInSector = 0; screenInSector < MapLayout.ScreensPerSector; screenInSector++)
                {
                    if (br.BaseStream.Position + ScreenSizeBytes > br.BaseStream.Length)
                    {
                        break;
                    }

                    var screen = new D1RscScreen();
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

                    screen.Trailing = br.ReadBytes(TrailingBytes);
                    result.Add(screen);
                }
            }

            return result;
        }

        public void Write(List<D1RscScreen> screens, string filename)
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

                    bw.Write(screen.Trailing);
                }
            }
        }
    }
}
