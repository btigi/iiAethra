using ii.Aethra.Model;

namespace ii.Aethra
{
    // Dungeon fog-of-war / explored-tile state
    // Each screen is a 24x16 byte grid stored column-wise (1 = explored, 0 = unexplored)
    // 99 screens = 11 dungeon sectors of 9 screens (the 12th T1 sector is unused)
    public class MapRsc
    {
        public const int ScreenCount = 99;
        public const int ScreenSizeBytes = MapLayout.TilesPerScreen; // 384
        public const int ExpectedFileSize = ScreenCount * ScreenSizeBytes; // 38016

        public List<MapRscScreen> Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            return Read(br);
        }

        public List<MapRscScreen> Read(BinaryReader br)
        {
            var result = new List<MapRscScreen>(ScreenCount);
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                var screen = new MapRscScreen();
                for (var x = 0; x < MapLayout.ScreenWidth; x++)
                {
                    for (var y = 0; y < MapLayout.ScreenHeight; y++)
                    {
                        screen.Visibility[y, x] = br.ReadByte();
                    }
                }

                result.Add(screen);
            }

            return result;
        }

        public void Write(List<MapRscScreen> screens, string filename)
        {
            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            using var bw = new BinaryWriter(fs);
            foreach (var screen in screens)
            {
                for (var x = 0; x < MapLayout.ScreenWidth; x++)
                {
                    for (var y = 0; y < MapLayout.ScreenHeight; y++)
                    {
                        bw.Write(screen.Visibility[y, x]);
                    }
                }
            }
        }
    }
}