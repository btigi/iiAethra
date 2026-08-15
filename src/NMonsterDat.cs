using System.Text;
using ii.Aethra.Model;

namespace ii.Aethra
{
    public class NMonsterDat
    {
        public const int MaxNameLength = 20;
        public const int RecordSizeBytes = 58;
        public const int RecordCount = 130;

        public List<MonsterRecord> Read(string filename)
        {
            var result = new List<MonsterRecord>();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            while (fs.Position + RecordSizeBytes <= fs.Length)
            {
                var record = new MonsterRecord();
                var nameLength = br.ReadByte();
                var name = br.ReadBytes(MaxNameLength);
                record.Name = Encoding.UTF8.GetString(name.Take(nameLength).ToArray());
                record.Unknown = br.ReadByte();
                record.CombatIconId = br.ReadByte();
                record.Unknown2 = br.ReadByte();
                record.SpecialAbilities = br.ReadInt16();
                record.OffensiveBonus = br.ReadInt16();
                record.Unknown3 = br.ReadByte();
                record.DefensiveBonus = br.ReadInt16();
                record.Unknown4 = br.ReadInt16();
                record.MaxDamage = br.ReadByte();
                record.Swings = br.ReadByte();
                record.MaxHits = br.ReadInt16();
                record.MaxHitsRelated = br.ReadInt16();
                record.Unknown5 = br.ReadInt16();
                record.CastSpells = br.ReadByte();
                record.SpellList = br.ReadByte();
                record.MaxSpellPoints = br.ReadInt16();
                record.Unknown6 = br.ReadByte();
                record.MonsterType = br.ReadByte();
                record.MagicResistance = br.ReadInt16();
                record.Unknown7 = br.ReadBytes(8);
                record.CombatIconSize = br.ReadByte();
                result.Add(record);
            }
            return result;
        }

        public void Write(List<MonsterRecord> records, string filename)
        {
            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            using var bw = new BinaryWriter(fs);
            foreach (var record in records)
            {
                var nameBytes = Encoding.UTF8.GetBytes(record.Name);
                var nameLength = (byte)Math.Min(nameBytes.Length, MaxNameLength);
                bw.Write(nameLength);
                bw.Write(nameBytes.Take(nameLength).ToArray());
                bw.Write(new byte[MaxNameLength - nameLength]);
                bw.Write(record.Unknown);
                bw.Write(record.CombatIconId);
                bw.Write(record.Unknown2);
                bw.Write(record.SpecialAbilities);
                bw.Write(record.OffensiveBonus);
                bw.Write(record.Unknown3);
                bw.Write(record.DefensiveBonus);
                bw.Write(record.Unknown4);
                bw.Write(record.MaxDamage);
                bw.Write(record.Swings);
                bw.Write(record.MaxHits);
                bw.Write(record.MaxHitsRelated);
                bw.Write(record.Unknown5);
                bw.Write(record.CastSpells);
                bw.Write(record.SpellList);
                bw.Write(record.MaxSpellPoints);
                bw.Write(record.Unknown6);
                bw.Write(record.MonsterType);
                bw.Write(record.MagicResistance);
                bw.Write(record.Unknown7);
                bw.Write(record.CombatIconSize);
            }
        }
    }
}
