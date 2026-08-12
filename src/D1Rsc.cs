namespace ii.Aethra
{
    // Dungeon map tile / cell data.
    // 108 screens (12 sectors × 9) of 24×16 cells stored column-major, with 7 layers
    // Layer 0 holds the floor tiles (FLOOR.PIC) - the tile id is stored in either the  low or high byte of each Int16
    // Other layers hold overlays, objects and trigger-related stuff
    public class D1Rsc
    {
        public const int ScreenCount = 108;
        public const int SectorCount = 12;
        public const int LayerCount = 7;
        public const int ScreenSizeBytes = MapLayout.TilesPerScreen * LayerCount * sizeof(short) + 1; // 5377
        public const int ExpectedFileSize = ScreenCount * ScreenSizeBytes; // 580716

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
                var screen = new D1RscScreen();
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

                screen.Padding = br.ReadByte();
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

                bw.Write(screen.Padding);
            }
        }

        public static byte GetSignificantByte(short value)
        {
            var unsigned = unchecked((ushort)value);
            var low = (byte)(unsigned & 0xFF);
            return low != 0 ? low : (byte)((unsigned >> 8) & 0xFF);
        }
    }

    public class D1RscScreen
    {
        public short[][,] Layers { get; set; } =
        [
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth]
        ];

        public byte Padding { get; set; }
        public short GetRaw(int layer, int x, int y) => Layers[layer][y, x];
        public void SetRaw(int layer, int x, int y, short value) => Layers[layer][y, x] = value;
        public byte GetFloorTileId(int x, int y) => D1Rsc.GetSignificantByte(Layers[0][y, x]);
    }
}