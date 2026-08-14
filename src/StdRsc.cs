using ii.Aethra.Model;

namespace ii.Aethra
{
    public class StdRsc
    {
        public List<(SoundRecord, byte[]?)> Read(string filename)
        {
            var result = new List<(SoundRecord, byte[]?)>();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);

            var soundCount = br.ReadByte();
            var records = new List<SoundRecord>(soundCount);
            for (int i = 0; i < soundCount; i++)
            {
                records.Add(new SoundRecord
                {
                    Unknown1 = br.ReadByte(),
                    Length = br.ReadInt32(),
                    Unknown2 = br.ReadBytes(4),
                    Offset = br.ReadUInt32(),
                    Filename = br.ReadBytes(12)
                });
            }

            foreach (var record in records)
            {
                byte[]? data = null;
                if (record.Offset > 0 && record.Length > 0)
                {
                    br.BaseStream.Seek(record.Offset, SeekOrigin.Begin);
                    data = br.ReadBytes(record.Length);
                }

                result.Add((record, data));
            }

            return result;
        }
    }
}