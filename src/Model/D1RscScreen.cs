namespace ii.Aethra.Model
{
    public class D1RscScreen
    {
        // Six layers of 1-based dungeon tile ids (0 = empty)
        // Layer 0 is the floor/wall base, higher layers hold overlays drawn on top
        public short[][,] Layers { get; set; } =
        [
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth]
        ];

        public byte[] Header { get; set; } = new byte[D1Rsc.HeaderBytes];
        public byte[] Trailing { get; set; } = new byte[D1Rsc.TrailingBytes];

        public short GetRaw(int layer, int x, int y) => Layers[layer][y, x];
        public void SetRaw(int layer, int x, int y, short value) => Layers[layer][y, x] = value;

        // 0-based PIC1 tile index for the cell (-1 = empty)
        public int GetPic1Index(int layer, int x, int y)
        {
            var value = Layers[layer][y, x];
            return value <= 0 ? -1 : value - 1 + D1Rsc.DungeonTileBase;
        }
    }
}