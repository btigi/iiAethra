using System.Text;

namespace iiAethra
{
    public class EncnterDat
    {
        public List<EncounterRecord> Read(string filename)
        {
            const int MaxNameLength = 40;

            var result = new List<EncounterRecord>();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            while (fs.Position < fs.Length)
            {
                var record = new EncounterRecord();
                var nameLength = br.BaseStream.ReadByte();
                var name = br.ReadBytes(MaxNameLength);
                record.Name = Encoding.UTF8.GetString(name.Take(nameLength).ToArray());
                record.MonsterCount = br.ReadByte();

                record.MonsterId1 = br.ReadInt16();
                record.MonsterId2 = br.ReadInt16();
                record.MonsterId3 = br.ReadInt16();
                record.MonsterId4 = br.ReadInt16();

                record.MonsterId1CountMax = br.ReadByte();
                record.MonsterId2CountMax = br.ReadByte();
                record.MonsterId3CountMax = br.ReadByte();
                record.MonsterId4CountMax = br.ReadByte();

                record.MonsterId1CountMin = br.ReadByte();
                record.MonsterId2CountMin = br.ReadByte();
                record.MonsterId3CountMin = br.ReadByte();
                record.MonsterId4CountMin = br.ReadByte();

                result.Add(record);
            }
            return result;
        }

        public void Write(List<EncounterRecord> records, string filename)
        {
            const int MaxNameLength = 40;

            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            using var bw = new BinaryWriter(fs);
            foreach (var record in records)
            {
                var nameBytes = Encoding.UTF8.GetBytes(record.Name);
                var paddedName = nameBytes.Concat(new byte[MaxNameLength - nameBytes.Length]).ToArray();
                bw.Write((byte)nameBytes.Length);
                bw.Write(paddedName);

                bw.Write(record.MonsterCount);

                bw.Write(record.MonsterId1);
                bw.Write(record.MonsterId2);
                bw.Write(record.MonsterId3);
                bw.Write(record.MonsterId4);

                bw.Write(record.MonsterId1CountMax);
                bw.Write(record.MonsterId2CountMax);
                bw.Write(record.MonsterId3CountMax);
                bw.Write(record.MonsterId4CountMax);

                bw.Write(record.MonsterId1CountMin);
                bw.Write(record.MonsterId2CountMin);
                bw.Write(record.MonsterId3CountMin);
                bw.Write(record.MonsterId4CountMin);
            }
        }
    }

    public class EncounterRecord
    {
        public byte NameLength { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte MonsterCount { get; set; }
        public Int16 MonsterId1 { get; set; }
        public Int16 MonsterId2 { get; set; }
        public Int16 MonsterId3 { get; set; }
        public Int16 MonsterId4 { get; set; }
        public byte MonsterId1CountMax { get; set; }
        public byte MonsterId2CountMax { get; set; }
        public byte MonsterId3CountMax { get; set; }
        public byte MonsterId4CountMax { get; set; }
        public byte MonsterId1CountMin { get; set; }
        public byte MonsterId2CountMin { get; set; }
        public byte MonsterId3CountMin { get; set; }
        public byte MonsterId4CountMin { get; set; }
    }
}