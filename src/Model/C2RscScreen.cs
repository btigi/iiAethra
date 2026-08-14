namespace ii.Aethra.Model
{
    public class C2RscScreen
    {
        // Four layers of 1-based PIC1 tile ids for a city/town screen
        public short[][,] Layers { get; set; } =
        [
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth]
        ];

        public byte[] Header { get; set; } = new byte[C2Rsc.HeaderBytes];

        public short GetTile(int layer, int x, int y) => Layers[layer][y, x];
        public void SetTile(int layer, int x, int y, short tileId) => Layers[layer][y, x] = tileId;

        public int GetPic1Index(int layer, int x, int y)
        {
            var value = Layers[layer][y, x];
            return value <= 0 ? -1 : value - 1 + C2Rsc.CityTileBase;
        }
    }
}