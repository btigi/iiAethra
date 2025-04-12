using System.Text;

namespace iiAethra
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
                var record = new Item();
                record.Id = br.ReadInt16();
                var cost = br.ReadBytes(6);
                record.Cost = Utils.Real48Convert(cost);
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

                result.Add(record);
            }
            return result;
        }
    }

    public class Item
    {
        public Int16 Id { get; set; }
        public double Cost { get; set; }
        public byte ChargesRemaining { get; set; }        
        public string UnidentifiedName { get; set; } = string.Empty;
        public string IdentifiedName { get; set; } = string.Empty;
        public byte UseResult1 { get; set; }
        public byte UseResult2 { get; set; }
        public byte ExtraMovement { get; set; }
        public byte ExtraShots { get; set; }
        public byte Defence { get; set; }
        public byte ExtraSpellPoints { get; set; }
        public byte PickLock { get; set; }
        public byte DisarmTraps { get; set; }
        public byte DeadlyStrike { get; set; }
        public byte Trading { get; set; }
        public byte ReadRunes { get; set; }
        public byte UnarmedCombat { get; set; }
        public byte HandheldArms { get; set; }
        public byte Bows { get; set; }
        public byte ItemIdentification { get; set; }
        public byte ExtraHits { get; set; }
        public byte ExtraSwings { get; set; }
        public byte MaxDamage { get; set; }
        public byte MinDamage { get; set; }
        public byte UseClass { get; set; }
        public byte Race { get; set; }
        public byte BodySlot { get; set; }
        public byte Class { get; set; }
        public byte MythicLore { get; set; }
        public byte WoodsLore { get; set; }
        public byte Mountaineering { get; set; }
        public byte DetectTraps { get; set; }
        public byte Perception { get; set; }
        public byte Cursed { get; set; }
        public byte Equipable { get; set; }
        public byte FireResistance { get; set; }
        public byte ColdResistance { get; set; }
        public byte WaterResistance { get; set; }
        public byte MindResistance { get; set; }
        public byte ShockResistance { get; set; }
    }
}