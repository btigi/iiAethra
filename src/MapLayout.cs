namespace ii.Aethra
{
    // Shared layout constants for RSC files, screens are arranged in 3x3 sectors, stored column-first:
    // 0 3 6
    // 1 4 7
    // 2 5 8
    // Tile grids within a screen are stored column-wise
    public static class MapLayout
    {
        public const int ScreenWidth = 24;
        public const int ScreenHeight = 16;
        public const int TilesPerScreen = ScreenWidth * ScreenHeight; // 384

        public const int ScreensPerSector = 9;
        public const int SectorWidthInScreens = 3;
        public const int SectorHeightInScreens = 3;

        public const int SectorWidthInTiles = ScreenWidth * SectorWidthInScreens;   // 72
        public const int SectorHeightInTiles = ScreenHeight * SectorHeightInScreens; // 48

        public static (int screenCol, int screenRow) GetScreenPositionInSector(int screenIndexInSector)
        {
            if (screenIndexInSector < 0 || screenIndexInSector >= ScreensPerSector)
            {
                throw new ArgumentOutOfRangeException(nameof(screenIndexInSector));
            }

            // Column-first order
            return (screenIndexInSector / SectorHeightInScreens, screenIndexInSector % SectorHeightInScreens);
        }

        public static int GetScreenIndexInSector(int screenCol, int screenRow)
        {
            if (screenCol < 0 || screenCol >= SectorWidthInScreens)
            {
                throw new ArgumentOutOfRangeException(nameof(screenCol));
            }

            if (screenRow < 0 || screenRow >= SectorHeightInScreens)
            {
                throw new ArgumentOutOfRangeException(nameof(screenRow));
            }

            return screenCol * SectorHeightInScreens + screenRow;
        }

        public static (int sectorX, int sectorY) ToSectorCoordinates(int screenIndexInSector, int tileX, int tileY)
        {
            var (screenCol, screenRow) = GetScreenPositionInSector(screenIndexInSector);
            return (screenCol * ScreenWidth + tileX, screenRow * ScreenHeight + tileY);
        }
    }
}