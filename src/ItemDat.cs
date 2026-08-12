using System.Text;
using ii.Aethra.Model;

namespace ii.Aethra
{
    public class ItemDat
    {
        public List<Item> Read(string filename)
        {
            var result = new List<Item>();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            while (fs.Position < fs.Length)
            {
                var record = ReadItem(br);
                result.Add(record);
            }
            return result;
        }

        public Item ReadItem(BinaryReader br)
        {
            var record = new Item();
            record.Id = br.ReadInt16();
            var cost = br.ReadBytes(6);
            record.Cost = Utils.ConvertFromReal48(cost);
            record.ChargesRemaining = br.ReadByte();
            var unidentifiedNameLength = br.ReadByte();
            var unidentifiedName = br.ReadBytes(16);
            record.UnidentifiedName = Encoding.UTF8.GetString(unidentifiedName.Take(unidentifiedNameLength).ToArray());
            var identifiedNameLength = br.ReadByte();
            var identifiedName = br.ReadBytes(16);
            record.IdentifiedName = Encoding.UTF8.GetString(identifiedName.Take(identifiedNameLength).ToArray());
            record.UseResult1 = br.ReadByte();
            record.UseResult2 = br.ReadByte();
            record.ExtraMovement = br.ReadByte();
            record.ExtraShots = br.ReadByte();
            record.Defence = br.ReadByte();
            record.ExtraSpellPoints = br.ReadByte();
            record.PickLock = br.ReadByte();
            record.DisarmTraps = br.ReadByte();
            record.DeadlyStrike = br.ReadByte();
            record.Trading = br.ReadByte();
            record.ReadRunes = br.ReadByte();
            record.UnarmedCombat = br.ReadByte();
            record.HandheldArms = br.ReadByte();
            record.Bows = br.ReadByte();
            record.ItemIdentification = br.ReadByte();
            record.ExtraHits = br.ReadByte();
            record.ExtraSwings = br.ReadByte();
            record.MaxDamage = br.ReadByte();
            record.MinDamage = br.ReadByte();
            record.UseClass = br.ReadByte();
            record.Race = br.ReadByte();
            record.BodySlot = br.ReadByte();
            record.Class = br.ReadByte();
            record.MythicLore = br.ReadByte();
            record.WoodsLore = br.ReadByte();
            record.Mountaineering = br.ReadByte();
            record.DetectTraps = br.ReadByte();
            record.Perception = br.ReadByte();
            record.Cursed = br.ReadByte();
            record.Equipable = br.ReadByte();
            record.FireResistance = br.ReadByte();
            record.ColdResistance = br.ReadByte();
            record.WaterResistance = br.ReadByte();
            record.MindResistance = br.ReadByte();
            record.ShockResistance = br.ReadByte();
            return record;
        }

        public void Write(List<Item> items, string filename)
        {
            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            using var bw = new BinaryWriter(fs);
            foreach (var item in items)
            {
                WriteItem(bw, item);
            }
        }

        public void WriteItem(BinaryWriter bw, Item item)
        {
            bw.Write(item.Id);
            bw.Write(Utils.ConvertToReal48(item.Cost));
            bw.Write(item.ChargesRemaining);

            var unidentifiedNameBytes = Encoding.UTF8.GetBytes(item.UnidentifiedName);
            Array.Resize(ref unidentifiedNameBytes, 16);
            bw.Write((byte)item.UnidentifiedName.Length);
            bw.Write(unidentifiedNameBytes);

            var identifiedNameBytes = Encoding.UTF8.GetBytes(item.IdentifiedName);
            Array.Resize(ref identifiedNameBytes, 16);
            bw.Write((byte)item.IdentifiedName.Length);
            bw.Write(identifiedNameBytes);

            bw.Write(item.UseResult1);
            bw.Write(item.UseResult2);
            bw.Write(item.ExtraMovement);
            bw.Write(item.ExtraShots);
            bw.Write(item.Defence);
            bw.Write(item.ExtraSpellPoints);
            bw.Write(item.PickLock);
            bw.Write(item.DisarmTraps);
            bw.Write(item.DeadlyStrike);
            bw.Write(item.Trading);
            bw.Write(item.ReadRunes);
            bw.Write(item.UnarmedCombat);
            bw.Write(item.HandheldArms);
            bw.Write(item.Bows);
            bw.Write(item.ItemIdentification);
            bw.Write(item.ExtraHits);
            bw.Write(item.ExtraSwings);
            bw.Write(item.MaxDamage);
            bw.Write(item.MinDamage);
            bw.Write(item.UseClass);
            bw.Write(item.Race);
            bw.Write(item.BodySlot);
            bw.Write(item.Class);
            bw.Write(item.MythicLore);
            bw.Write(item.WoodsLore);
            bw.Write(item.Mountaineering);
            bw.Write(item.DetectTraps);
            bw.Write(item.Perception);
            bw.Write(item.Cursed);
            bw.Write(item.Equipable);
            bw.Write(item.FireResistance);
            bw.Write(item.ColdResistance);
            bw.Write(item.WaterResistance);
            bw.Write(item.MindResistance);
            bw.Write(item.ShockResistance);
        }

    }
}