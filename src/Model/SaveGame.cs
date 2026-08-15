namespace ii.Aethra.Model
{
    public class SaveGame
    {
        public byte[] Unknown { get; set; } = new byte[1];
        public byte BeaconSpellActive { get; set; }
        public byte[] Unknown2 { get; set; } = new byte[8];
        public Int16 Time { get; set; }
        public Int16 DungeonMapSector { get; set; }
        public Int16 CityMapSector { get; set; }
        public Int16 WorldMapSector { get; set; }
        public Int16 Date { get; set; }
        public byte[] Unknown3 { get; set; } = new byte[2];
        public Int16 PartyWorldMapSubSectorXCoord { get; set; }
        public Int16 PartyWorldMapSubSectorYCoord { get; set; }
        public Int16 MageLightSpellTimeRemaining { get; set; }
        public byte[] Unknown4 { get; set; } = new byte[2];
        public Int16 BeaconSpellPartyWorldMapSubSectorXCoord { get; set; }
        public Int16 BeaconSpellPartyWorldMapSubSectorYCoord { get; set; }
        public Int16 BeaconSpellWorldMapSector { get; set; }
        public Int16 BeaconSpellWorldMapSubSectorXCoord { get; set; }
        public Int16 BeaconSpellWorldMapSubSectorYCoord { get; set; }
        public byte[] Unknown5 { get; set; } = new byte[4];
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
        public byte[] Unknown6 { get; set; } = new byte[4];
        public Int16 HeatColdCondition { get; set; }
        public Int16 EyesOfTheFelineSpellTimeRemaining { get; set; }
        public Int16 OldVizierQuestStatus { get; set; }
        public byte[] Unknown7 { get; set; } = new byte[24];
        public Int16 OracleGemQuestStatus { get; set; }
        public Int16 DwarfKingQuestStatus { get; set; }
        public byte[] Unknown8 { get; set; } = new byte[2];
        public Int16 DwarfPrisonerQuestStatus { get; set; }
        public Int16 DwarfPrisonerRewardQuestStatus { get; set; }
        public byte[] Unknown9 { get; set; } = new byte[8];
        public Int16 ThiefQuestStatus { get; set; }
        public byte[] Unknown10 { get; set; } = new byte[12];
        public Int16 LetterQuestStatus { get; set; }
        public byte[] Unknown11 { get; set; } = new byte[2];
        public Int16 ProphecyQuestStatus { get; set; }
        public Int16 MarshQuestStatus { get; set; }
        public byte[] Unknown12 { get; set; } = new byte[2];
        public Int16 GuardianQuestStatus { get; set; }
        public byte[] Unknown13 { get; set; } = new byte[26];
        public Int16 CarnageOff { get; set; }
        public Int16 GroupRunOff { get; set; }
    }
}
