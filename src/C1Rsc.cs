using ii.Aethra.Model;

namespace ii.Aethra
{
    // Overworld tile maps
    // 36 screens (4 sectors × 9) of 24×16 cells. Graphics are column-wise
    // 4 sectors tile a 6×6-screen world in a 2×2 arrangement (sector 0 top-left, 1 top-right, 2 bottom-left, 3 bottom-right)
    // Each screen has 4 layers of int16 values.
    // A value is a 1-based index into the 16×16 tiles of PIC1.RSC (value − 1 = tile index); 0 = empty
    // Layers are drawn in order 0..3, magenta is transparent.
    // Empty cells show as a random plain-grass tile (PIC1 indices 69-78)
    public class C1Rsc
    {
        public const int ScreenCount = 36;
        public const int SectorCount = 4;
        public const int LayerCount = 4;
        public const int ScreenSizeBytes = MapLayout.TilesPerScreen * LayerCount * sizeof(short); // 3072
        public const int ExpectedFileSize = ScreenCount * ScreenSizeBytes; // 110592

        public List<C1RscScreen> Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            return Read(br);
        }

        public List<C1RscScreen> Read(BinaryReader br)
        {
            var result = new List<C1RscScreen>(ScreenCount);
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                var screen = new C1RscScreen();
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

            return result;
        }

        public void Write(List<C1RscScreen> screens, string filename)
        {
            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            using var bw = new BinaryWriter(fs);
            foreach (var screen in screens)
            {
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