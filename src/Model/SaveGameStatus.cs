namespace ii.Aethra.Model
{
    public class SaveGameStatus
    {
        public string SaveGameName { get; set; } = string.Empty;
        public byte[] Unknown { get; set; } = new byte[59];
    }
}