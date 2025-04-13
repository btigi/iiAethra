namespace iiAethra
{
    public class StdRsc
    {
        public List<(SoundRecord, byte[]?)> Read(string filename)
        {
            var result = new List<(SoundRecord, byte[]?)>();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);

            var soundCount = br.ReadByte();
            for (int i = 0; i < soundCount; i++)
            {
                var record = new SoundRecord
                {
                    Unknown1 = br.ReadByte(),
                    Length = br.ReadInt16(),
                    Unknown2 = br.ReadBytes(6),
                    Offset = br.ReadUInt16(),
                    Unknown3 = br.ReadInt16(),
                    Filename = br.ReadBytes(12)
                };

                byte[]? data = null;
                if (record.Offset > 0)
                {
                    try
                    {
                        br.BaseStream.Seek(record.Offset, SeekOrigin.Begin);
                        data = br.ReadBytes(record.Length);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading sound data: {ex.Message}");
                    }
                }

                result.Add((record, data));
            }

            return result;
        }
    }

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