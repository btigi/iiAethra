namespace ii.Aethra.Model
{
    public class C2RscScreen
    {
        // Three layers of gameplay values for the city/world map screen
        public short[][,] Layers { get; set; } =
        [
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth]
        ];

        public byte[] Header { get; set; } = new byte[C2Rsc.HeaderBytes];
        public byte[] Trailing { get; set; } = new byte[C2Rsc.TrailingBytes];

        public short GetValue(int layer, int x, int y) => Layers[layer][y, x];

        public void SetValue(int layer, int x, int y, short value) => Layers[layer][y, x] = value;
    }
}
