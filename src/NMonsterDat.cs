using System.Text;
using ii.Aethra.Model;

namespace ii.Aethra
{
    public class NMonsterDat
    {
        public List<MonsterRecord> Read(string filename)
        {
            var result = new List<MonsterRecord>();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            while (fs.Position < fs.Length)
            {
                var record = new MonsterRecord();
                var nameLength = br.ReadByte();
                var name = br.ReadBytes(21);
                record.Name = Encoding.UTF8.GetString(name.Take(nameLength).ToArray());
                record.CombatIconId = br.ReadByte();
                record.SpecialAbilities = br.ReadInt32();
                record.OffensiveBonus = br.ReadInt32();
                record.DefensiveBonus = br.ReadInt32();
                record.MaxDamage = br.ReadByte();
                record.Swings = br.ReadByte();
                record.MaxHits = br.ReadInt32();
                record.MaxHitsInfo = br.ReadInt32();
                record.CastSpells = br.ReadByte();
                record.SpellInfo = br.ReadByte();
                record.MaxSpellInfo = br.ReadInt32();
                record.MonsterType = br.ReadByte();
                record.MagicResistance = br.ReadInt32();
                record.CombatIconSize = br.ReadByte();
                record.PortraitId = br.ReadByte();
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
                var nameLength = (byte)Math.Min(nameBytes.Length, 21);
                bw.Write(nameLength);
                bw.Write(nameBytes.Take(nameLength).ToArray());
                bw.Write(new byte[21 - nameLength]);
                bw.Write(record.CombatIconId);
                bw.Write(record.SpecialAbilities);
                bw.Write(record.OffensiveBonus);
                bw.Write(record.DefensiveBonus);
                bw.Write(record.MaxDamage);
                bw.Write(record.Swings);
                bw.Write(record.MaxHits);
                bw.Write(record.MaxHitsInfo);
                bw.Write(record.CastSpells);
                bw.Write(record.SpellInfo);
                bw.Write(record.MaxSpellInfo);
                bw.Write(record.MonsterType);
                bw.Write(record.MagicResistance);
                bw.Write(record.CombatIconSize);
                bw.Write(record.PortraitId);
            }
        }
    }
}