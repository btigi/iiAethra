using System.Text;

namespace iiAethra
{
    public class PartyDat
    {
        public List<PartyMemberRecord> Read(string filename)
        {
            const int MaxPartyMemberNameLength = 15;
            const int MaxPartyMemberRaceLength = 15;
            const int MaxPartyMemberLevelDescriptionLength = 15;

            var itemReader = new ItemDat();

            var result = new List<PartyMemberRecord>();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            while (fs.Position < fs.Length)
            {
                var record = new PartyMemberRecord();
                var nameLength = br.ReadByte();
                var name = br.ReadBytes(MaxPartyMemberNameLength);
                record.Name = Encoding.UTF8.GetString(name.Take(nameLength).ToArray());

                var raceLength = br.ReadByte();
                var race = br.ReadBytes(MaxPartyMemberRaceLength);
                record.Race = Encoding.UTF8.GetString(name.Take(raceLength).ToArray());

                var levelDescriptionLength = br.ReadByte();
                var levelDescription = br.ReadBytes(MaxPartyMemberLevelDescriptionLength);
                record.LevelDescription = Encoding.UTF8.GetString(name.Take(levelDescriptionLength).ToArray());

                record.ActiveStatus = br.ReadInt16();
                record.Level = br.ReadInt16();
                record.Unknown = br.ReadBytes(6);
                record.Class = br.ReadInt16();
                record.Unknown2 = br.ReadBytes(12);
                record.WeaponSwings = br.ReadInt16();
                record.BowShots = br.ReadInt16();
                record.Unknown3 = br.ReadInt32();
                record.Movement = br.ReadInt16();
                record.InventoryItemCount = br.ReadByte();
                record.Unknown4 = br.ReadByte();
                var experience = br.ReadBytes(6);
                record.Experience = Utils.ConvertFromReal48(experience);
                var gold = br.ReadBytes(6);
                record.Gold = Utils.ConvertFromReal48(gold);
                record.Unknown5 = br.ReadInt16();
                record.PickLocks = br.ReadInt16();
                record.DisarmTraps = br.ReadInt16();
                record.DeadlyStrike = br.ReadInt16();
                record.Trading = br.ReadInt16();
                record.ReadRunes = br.ReadInt16();
                record.UnarmedCombat = br.ReadInt16();
                record.HandheldArms = br.ReadInt16();
                record.Bows = br.ReadInt16();
                record.Identify = br.ReadInt16();
                record.Defence = br.ReadInt16();
                record.HitPointMax = br.ReadInt16();
                record.Damage = br.ReadInt16();
                record.SpellPointsMax = br.ReadInt16();
                record.SpellPoints = br.ReadInt16();
                record.Strength = br.ReadByte();
                record.Agility = br.ReadByte();
                record.Constitution = br.ReadByte();
                record.Intelligence = br.ReadByte();
                record.Wisdom = br.ReadByte();
                record.Presence = br.ReadByte();
                record.Memory = br.ReadByte();
                record.Reason = br.ReadByte();
                record.Unknown6 = br.ReadBytes(20);
                record.StrengthStatBonus = br.ReadInt16();
                record.AgilityStatBonus = br.ReadInt16();
                record.ConstitutionStatBonus = br.ReadInt16();
                record.IntelligenceStatBonus = br.ReadInt16();
                record.WisdomStatBonus = br.ReadInt16();
                record.PresenceStatBonus = br.ReadInt16();
                record.MemoryStatBonus = br.ReadInt16();
                record.ReasonStatBonus = br.ReadInt16();
                record.StrengthTotalStatBonus = br.ReadInt16();
                record.AgilityTotalStatBonus = br.ReadInt16();
                record.ConstitutionTotalStatBonus = br.ReadInt16();
                record.IntelligenceTotalStatBonus = br.ReadInt16();
                record.WisdomTotalStatBonus = br.ReadInt16();
                record.PresenceTotalStatBonus = br.ReadInt16();
                record.MemoryTotalStatBonus = br.ReadInt16();
                record.ReasonTotalStatBonus = br.ReadInt16();
                record.Unknown7 = br.ReadBytes(36);

                for (int i = 0; i < 20; i++)
                {
                    var item = itemReader.ReadItem(br);
                    record.Items[i] = item;
                }
                record.ItemEquipped = br.ReadBytes(20);
                record.ItemIdentified = br.ReadBytes(20);
                record.FireResistence = br.ReadInt16();
                record.ColdResistence = br.ReadInt16();
                record.WaterResistence = br.ReadInt16();
                record.MindResistence = br.ReadInt16();
                record.ShockResistence = br.ReadInt16();
                record.DetectTraps = br.ReadInt16();
                record.Perception = br.ReadInt16();
                record.MythicLore = br.ReadInt16();
                record.SpellList = br.ReadInt16();
                record.WoodLore = br.ReadInt16();
                record.Mountaineering = br.ReadInt16();
                record.Portrait = br.ReadByte();
                record.Unknown8 = br.ReadBytes(13);
                record.BookOfFaith = br.ReadByte();
                record.BookOfInvocation = br.ReadByte();
                record.BookOfRedemption = br.ReadByte();
                record.BookOfUniversalArcanum = br.ReadByte();
                record.ElementalDiscipline = br.ReadByte();
                record.DisciplineOfChronmetry = br.ReadByte();
                record.DisciplineOfTransmogrification = br.ReadByte();
                record.DisciplineOfSorcery = br.ReadByte();
                record.DisciplineOfMentalAcuity = br.ReadByte();
                record.DisciplineOfConjuration = br.ReadByte();
                record.SylvanMagick = br.ReadByte();
                record.PathOfTheAshikari = br.ReadByte();
                record.TheSwordOfRighteousness = br.ReadByte();
                record.SongsOfTheMinstrel = br.ReadByte();
                record.Unknown9 = br.ReadBytes(14);

                result.Add(record);
            }
            return result;
        }
    }

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