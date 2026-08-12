namespace ii.Aethra.Model
{
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