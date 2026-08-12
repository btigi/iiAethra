namespace ii.Aethra.Model
{
    public class C1RscScreen
    {
        // Four layers of 1-based PIC1 tile ids
        // Layer 0 is the primary ground; higher layers hold overlays drawn on top
        public short[][,] Layers { get; set; } =
        [
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth]
        ];

        public short GetTile(int layer, int x, int y) => Layers[layer][y, x];
        public void SetTile(int layer, int x, int y, short tileId) => Layers[layer][y, x] = tileId;
        public int GetPic1Index(int layer, int x, int y) => Layers[layer][y, x] - 1;
    }
}