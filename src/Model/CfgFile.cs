namespace ii.Aethra.Model
{
    public class CfgFile
    {
        public Int16 Unknown { get; set; }
        public byte MusicStatus { get; set; }
        public byte SoundStatus { get; set; }
        public List<SaveGameStatus> SaveGameStatus { get; set; } = [];
    }
}