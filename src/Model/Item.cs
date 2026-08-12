namespace ii.Aethra.Model
{
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