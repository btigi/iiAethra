namespace ii.Aethra.Model
{
    public class SoundRecord
    {
        public byte Unknown1 { get; set; }
        public Int16 Length { get; set; }
        public byte[] Unknown2 { get; set; } = new byte[6];
        public UInt16 Offset { get; set; }
        public Int16 Unknown3 { get; set; }
        public byte[] Filename { get; set; } = new byte[12];
    }
}