using ii.Aethra.Model;

namespace ii.Aethra
{
    // Dungeon map tile data.
    // 108 screens (12 sectors × 9) of 24×16 cells stored column-wise, with 6 layers
    public class D1Rsc
    {
        public const int ScreenCount = 108;
        public const int SectorCount = 12;
        public const int LayerCount = 6;
        public const int HeaderBytes = 9;
        public const int TrailingBytes = 760;
        public const int ScreenSizeBytes = HeaderBytes + MapLayout.TilesPerScreen * LayerCount * sizeof(short) + TrailingBytes; // 5377
        public const int ExpectedFileSize = ScreenCount * ScreenSizeBytes; // 580716

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
                var screen = new D1RscScreen
                {
                    Header = br.ReadBytes(HeaderBytes)
                };

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

            return result;
        }

        public void Write(List<D1RscScreen> screens, string filename)
        {
            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            using var bw = new BinaryWriter(fs);
            foreach (var screen in screens)
            {
                bw.Write(screen.Header);
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
