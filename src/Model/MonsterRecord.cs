namespace ii.Aethra.Model
{
    public class MonsterRecord
    {
        public string Name { get; set; } = string.Empty;
        public byte Unknown { get; set; }
        public byte CombatIconId { get; set; }
        public byte Unknown2 { get; set; }
        public Int16 SpecialAbilities { get; set; }
        public Int16 OffensiveBonus { get; set; }
        public byte Unknown3 { get; set; }
        public Int16 DefensiveBonus { get; set; }
        public Int16 Unknown4 { get; set; }
        public byte MaxDamage { get; set; }
        public byte Swings { get; set; }
        public Int16 MaxHits { get; set; }
        public Int16 MaxHitsRelated { get; set; }
        public Int16 Unknown5 { get; set; }
        public byte CastSpells { get; set; }
        public byte SpellList { get; set; }
        public Int16 MaxSpellPoints { get; set; }
        public byte Unknown6 { get; set; }
        public byte MonsterType { get; set; }
        public Int16 MagicResistance { get; set; }
        public byte[] Unknown7 { get; set; } = new byte[8];
        public byte CombatIconSize { get; set; }
    }
}
