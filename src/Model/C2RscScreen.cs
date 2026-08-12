namespace ii.Aethra.Model
{
    public class C2RscScreen
    {
        //Four layers of gameplay values for the city/world map screen
        public short[][,] Layers { get; set; } =
        [
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth],
            new short[MapLayout.ScreenHeight, MapLayout.ScreenWidth]
        ];
        
        public byte Padding { get; set; }

        public short GetValue(int layer, int x, int y) => Layers[layer][y, x];

        public void SetValue(int layer, int x, int y, short value) => Layers[layer][y, x] = value;
    }
}
