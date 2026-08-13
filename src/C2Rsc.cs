using ii.Aethra.Model;

namespace ii.Aethra
{
    // City / overworld gameplay map data (collision, triggers, etc.)
    // 36 screens (4 sectors × 9) of 24×16 cells stored column-wise, with 3 layers.
    // Layer values are gameplay flags rather than tile ids
    public class C2Rsc
    {
        public const int ScreenCount = 36;
        public const int SectorCount = 4;
        public const int LayerCount = 3;
        public const int HeaderBytes = 9;
        public const int TrailingBytes = 760;
        public const int ScreenSizeBytes = HeaderBytes + MapLayout.TilesPerScreen * LayerCount * sizeof(short) + TrailingBytes; // 3073
        public const int ExpectedFileSize = ScreenCount * ScreenSizeBytes; // 110628

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
                var screen = new C2RscScreen
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

        public void Write(List<C2RscScreen> screens, string filename)
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
