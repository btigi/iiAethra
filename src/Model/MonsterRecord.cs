namespace ii.Aethra.Model
{
    public class MonsterRecord
    {
        public string Name { get; set; } = string.Empty;
        public byte CombatIconId { get; set; }
        public Int32 SpecialAbilities { get; set; }
        public Int32 OffensiveBonus { get; set; }
        public Int32 DefensiveBonus { get; set; }
        public byte MaxDamage { get; set; }
        public byte Swings { get; set; }
        public Int32 MaxHits { get; set; }
        public Int32 MaxHitsInfo { get; set; }
        public byte CastSpells { get; set; }
        public byte SpellInfo { get; set; }
        public Int32 MaxSpellInfo { get; set; }
        public byte MonsterType { get; set; }
        public Int32 MagicResistance { get; set; }
        public byte CombatIconSize { get; set; }
        public byte PortraitId { get; set; }
    }
}