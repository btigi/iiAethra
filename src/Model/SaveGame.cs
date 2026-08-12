namespace ii.Aethra.Model
{
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