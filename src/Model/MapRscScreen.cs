namespace ii.Aethra.Model
{
    public class MapRscScreen
    {
        public byte[,] Visibility { get; set; } = new byte[MapLayout.ScreenHeight, MapLayout.ScreenWidth];

        public bool IsExplored(int x, int y) => Visibility[y, x] != 0;
    }
}