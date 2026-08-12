namespace ii.Aethra.Model
{
    public class PartyMemberRecord
    {
        public string Name { get; set; } = string.Empty;
        public string Race { get; set; } = string.Empty;
        public string LevelDescription { get; set; } = string.Empty;
        public Int16 ActiveStatus { get; set; }
        public Int16 Level { get; set; }
        public byte[] Unknown { get; set; } = new byte[6];
        public Int16 Class { get; set; }
        public byte[] Unknown2 { get; set; } = new byte[12];
        public Int16 WeaponSwings { get; set; }
        public Int16 BowShots { get; set; }
        public Int32 Unknown3 { get; set; }
        public Int16 Movement { get; set; }
        public byte InventoryItemCount { get; set; }
        public byte Unknown4 { get; set; }
        public double Experience { get; set; }
        public double Gold { get; set; }
        public Int16 Unknown5 { get; set; }
        public Int16 PickLocks { get; set; }
        public Int16 DisarmTraps { get; set; }
        public Int16 DeadlyStrike { get; set; }
        public Int16 Trading { get; set; }
        public Int16 ReadRunes { get; set; }
        public Int16 UnarmedCombat { get; set; }
        public Int16 HandheldArms { get; set; }
        public Int16 Bows { get; set; }
        public Int16 Identify { get; set; }
        public Int16 Defence { get; set; }
        public Int16 HitPointMax { get; set; }
        public Int16 Damage { get; set; }
        public Int16 SpellPointsMax { get; set; }
        public Int16 SpellPoints { get; set; }
        public byte Strength { get; set; }
        public byte Agility { get; set; }
        public byte Constitution { get; set; }
        public byte Intelligence { get; set; }
        public byte Wisdom { get; set; }
        public byte Presence { get; set; }
        public byte Memory { get; set; }
        public byte Reason { get; set; }
        public byte[] Unknown6 { get; set; } = new byte[20];
        public Int16 StrengthStatBonus { get; set; }
        public Int16 AgilityStatBonus { get; set; }
        public Int16 ConstitutionStatBonus { get; set; }
        public Int16 IntelligenceStatBonus { get; set; }
        public Int16 WisdomStatBonus { get; set; }
        public Int16 PresenceStatBonus { get; set; }
        public Int16 MemoryStatBonus { get; set; }
        public Int16 ReasonStatBonus { get; set; }
        public Int16 StrengthTotalStatBonus { get; set; }
        public Int16 AgilityTotalStatBonus { get; set; }
        public Int16 ConstitutionTotalStatBonus { get; set; }
        public Int16 IntelligenceTotalStatBonus { get; set; }
        public Int16 WisdomTotalStatBonus { get; set; }
        public Int16 PresenceTotalStatBonus { get; set; }
        public Int16 MemoryTotalStatBonus { get; set; }
        public Int16 ReasonTotalStatBonus { get; set; }
        public byte[] Unknown7 { get; set; } = new byte[36];
        public Item[] Items { get; set; } = new Item[20];
        public byte[] ItemEquipped { get; set; } = new byte[20];
        public byte[] ItemIdentified { get; set; } = new byte[20];
        public Int16 FireResistence { get; set; }
        public Int16 ColdResistence { get; set; }
        public Int16 WaterResistence { get; set; }
        public Int16 MindResistence { get; set; }
        public Int16 ShockResistence { get; set; }
        public Int16 DetectTraps { get; set; }
        public Int16 Perception { get; set; }
        public Int16 MythicLore { get; set; }
        public Int16 SpellList { get; set; }
        public Int16 WoodLore { get; set; }
        public Int16 Mountaineering { get; set; }
        public byte Portrait { get; set; }
        public byte[] Unknown8 { get; set; } = new byte[13];
        public byte BookOfFaith { get; set; }
        public byte BookOfInvocation { get; set; }
        public byte BookOfRedemption { get; set; }
        public byte BookOfUniversalArcanum { get; set; }
        public byte ElementalDiscipline { get; set; }
        public byte DisciplineOfChronmetry { get; set; }
        public byte DisciplineOfTransmogrification { get; set; }
        public byte DisciplineOfSorcery { get; set; }
        public byte DisciplineOfMentalAcuity { get; set; }
        public byte DisciplineOfConjuration { get; set; }
        public byte SylvanMagick { get; set; }
        public byte PathOfTheAshikari { get; set; }
        public byte TheSwordOfRighteousness { get; set; }
        public byte SongsOfTheMinstrel { get; set; }
        public byte[] Unknown9 { get; set; } = new byte[14];
    }
}