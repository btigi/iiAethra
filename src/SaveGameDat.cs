namespace iiAethra
{
    public class SaveGameDat
    {
        public SaveGame Read(string filename)
        {
            var saveGam = new SaveGame();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);

            saveGam.BeaconSpellActive = br.ReadByte();
            saveGam.WorldMapSector = br.ReadInt16();
            saveGam.Date = br.ReadInt16();
            saveGam.PartyWorldMapSubSectorXCoord = br.ReadInt16();
            saveGam.PartyWorldMapSubSectorYCoord = br.ReadInt16();
            saveGam.MageLightSpellTimeRemaining = br.ReadInt16();
            saveGam.BeaconSpellPartyWorldMapSubSectorXCoord = br.ReadInt16();
            saveGam.BeaconSpellPartyWorldMapSubSectorYCoord = br.ReadInt16();
            saveGam.BeaconSpellWorldMapSubSector = br.ReadInt16();
            saveGam.BeaconSpellWorldMapSubSectorXCoord = br.ReadInt16();
            saveGam.BeaconSpellWorldMapSubSectorYCoord = br.ReadInt16();
            saveGam.WorldMapSubSectorXCoord = br.ReadInt16();
            saveGam.WorldMapSubSectorYCoord = br.ReadInt16();
            saveGam.DungeonMapSubSectorXCoord = br.ReadInt16();
            saveGam.DungeonMapSubSectorYCoord = br.ReadInt16();
            saveGam.CityMapSubSectorXCoord = br.ReadInt16();
            saveGam.CityMapSubSectorYCoord = br.ReadInt16();
            saveGam.ConjureCausewaySpellTimeRemaining = br.ReadInt16();
            saveGam.PartyCityMapSubSectorXCoord = br.ReadInt16();
            saveGam.PartyCityMapSubSectorYCoord = br.ReadInt16();
            saveGam.PartyDungeonMapSubSectorXCoord = br.ReadInt16();
            saveGam.PartyDungeonMapSubSectorYCoord = br.ReadInt16();
            saveGam.Unknown = br.ReadBytes(1058);
            saveGam.EyesOfTheFelineSpellTimeRemaining = br.ReadInt16();
            saveGam.OldVizierQuestStatus = br.ReadInt16();
            saveGam.OracleGemQuestStatus = br.ReadInt16();
            saveGam.DwarfKingQuestStatus = br.ReadInt16();
            saveGam.DwarfPrisonerQuestStatus = br.ReadInt16();
            saveGam.DwarfPrisonerRewardQuestStatus = br.ReadInt16();
            saveGam.ThiefQuestStatus = br.ReadInt16();
            saveGam.LetterQuestStatus = br.ReadInt16();
            saveGam.ProphecyQuestStatus = br.ReadInt16();
            saveGam.MarshQuestStatus = br.ReadInt16();
            saveGam.GuardianQuestStatus = br.ReadInt16();
            saveGam.Time = br.ReadInt16();
            saveGam.CarnageOff = br.ReadInt16();
            saveGam.GroupRunOff = br.ReadInt16();
            saveGam.DungeonMapSector = br.ReadInt16();
            saveGam.CityMapSector = br.ReadInt16();

            return saveGam;
        }
    }

    public class SaveGame
    {
        public byte BeaconSpellActive { get; set; }
        public Int16 WorldMapSector { get; set; }
        public Int16 Date { get; set; }
        public Int16 PartyWorldMapSubSectorXCoord { get; set; }
        public Int16 PartyWorldMapSubSectorYCoord { get; set; }
        public Int16 MageLightSpellTimeRemaining { get; set; }
        public Int16 BeaconSpellPartyWorldMapSubSectorXCoord { get; set; }
        public Int16 BeaconSpellPartyWorldMapSubSectorYCoord { get; set; }
        public Int16 BeaconSpellWorldMapSubSector { get; set; }
        public Int16 BeaconSpellWorldMapSubSectorXCoord { get; set; }
        public Int16 BeaconSpellWorldMapSubSectorYCoord { get; set; }
        public Int16 WorldMapSubSectorXCoord { get; set; }
        public Int16 WorldMapSubSectorYCoord { get; set; }
        public Int16 DungeonMapSubSectorXCoord { get; set; }
        public Int16 DungeonMapSubSectorYCoord { get; set; }
        public Int16 CityMapSubSectorXCoord { get; set; }
        public Int16 CityMapSubSectorYCoord { get; set; }
        public Int16 ConjureCausewaySpellTimeRemaining { get; set; }
        public Int16 PartyCityMapSubSectorXCoord { get; set; }
        public Int16 PartyCityMapSubSectorYCoord { get; set; }
        public Int16 PartyDungeonMapSubSectorXCoord { get; set; }
        public Int16 PartyDungeonMapSubSectorYCoord { get; set; }
        public byte[] Unknown { get; set; } = new byte[1058];
        public Int16 EyesOfTheFelineSpellTimeRemaining { get; set; }
        public Int16 OldVizierQuestStatus { get; set; }
        public Int16 OracleGemQuestStatus { get; set; }
        public Int16 DwarfKingQuestStatus { get; set; }
        public Int16 DwarfPrisonerQuestStatus { get; set; }
        public Int16 DwarfPrisonerRewardQuestStatus { get; set; }
        public Int16 ThiefQuestStatus { get; set; }
        public Int16 LetterQuestStatus { get; set; }
        public Int16 ProphecyQuestStatus { get; set; }
        public Int16 MarshQuestStatus { get; set; }
        public Int16 GuardianQuestStatus { get; set; }
        public Int16 Time { get; set; }
        public Int16 CarnageOff { get; set; }
        public Int16 GroupRunOff { get; set; }
        public Int16 DungeonMapSector { get; set; }
        public Int16 CityMapSector { get; set; }
    }
}