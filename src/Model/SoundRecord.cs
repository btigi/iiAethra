namespace ii.Aethra.Model
{
    public class SoundRecord
    {
        public byte Unknown1 { get; set; }
        public Int32 Length { get; set; }
        public byte[] Unknown2 { get; set; } = new byte[4];
        public UInt32 Offset { get; set; }
        public byte[] Filename { get; set; } = new byte[12];
    }
}
