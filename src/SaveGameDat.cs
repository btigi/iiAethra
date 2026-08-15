using ii.Aethra.Model;

namespace ii.Aethra
{
    public class SaveGameDat
    {
        public SaveGame Read(string filename)
        {
            var saveGam = new SaveGame();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);

            saveGam.Unknown = br.ReadBytes(1);
            saveGam.BeaconSpellActive = br.ReadByte();
            saveGam.Unknown2 = br.ReadBytes(8);
            saveGam.Time = br.ReadInt16();
            saveGam.DungeonMapSector = br.ReadInt16();
            saveGam.CityMapSector = br.ReadInt16();
            saveGam.WorldMapSector = br.ReadInt16();
            saveGam.Date = br.ReadInt16();
            saveGam.Unknown3 = br.ReadBytes(2);
            saveGam.PartyWorldMapSubSectorXCoord = br.ReadInt16();
            saveGam.PartyWorldMapSubSectorYCoord = br.ReadInt16();
            saveGam.MageLightSpellTimeRemaining = br.ReadInt16();
            saveGam.Unknown4 = br.ReadBytes(2);
            saveGam.BeaconSpellPartyWorldMapSubSectorXCoord = br.ReadInt16();
            saveGam.BeaconSpellPartyWorldMapSubSectorYCoord = br.ReadInt16();
            saveGam.BeaconSpellWorldMapSector = br.ReadInt16();
            saveGam.BeaconSpellWorldMapSubSectorXCoord = br.ReadInt16();
            saveGam.BeaconSpellWorldMapSubSectorYCoord = br.ReadInt16();
            saveGam.Unknown5 = br.ReadBytes(4);
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
            saveGam.Unknown6 = br.ReadBytes(4);
            saveGam.HeatColdCondition = br.ReadInt16();
            saveGam.EyesOfTheFelineSpellTimeRemaining = br.ReadInt16();
            saveGam.OldVizierQuestStatus = br.ReadInt16();
            saveGam.Unknown7 = br.ReadBytes(24);
            saveGam.OracleGemQuestStatus = br.ReadInt16();
            saveGam.DwarfKingQuestStatus = br.ReadInt16();
            saveGam.Unknown8 = br.ReadBytes(2);
            saveGam.DwarfPrisonerQuestStatus = br.ReadInt16();
            saveGam.DwarfPrisonerRewardQuestStatus = br.ReadInt16();
            saveGam.Unknown9 = br.ReadBytes(8);
            saveGam.ThiefQuestStatus = br.ReadInt16();
            saveGam.Unknown10 = br.ReadBytes(12);
            saveGam.LetterQuestStatus = br.ReadInt16();
            saveGam.Unknown11 = br.ReadBytes(2);
            saveGam.ProphecyQuestStatus = br.ReadInt16();
            saveGam.MarshQuestStatus = br.ReadInt16();
            saveGam.Unknown12 = br.ReadBytes(2);
            saveGam.GuardianQuestStatus = br.ReadInt16();
            saveGam.Unknown13 = br.ReadBytes(26);
            saveGam.CarnageOff = br.ReadInt16();
            saveGam.GroupRunOff = br.ReadInt16();

            return saveGam;
        }

        public void Write(SaveGame saveGam, string filename)
        {
            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            using var bw = new BinaryWriter(fs);

            bw.Write(saveGam.Unknown);
            bw.Write(saveGam.BeaconSpellActive);
            bw.Write(saveGam.Unknown2);
            bw.Write(saveGam.Time);
            bw.Write(saveGam.DungeonMapSector);
            bw.Write(saveGam.CityMapSector);
            bw.Write(saveGam.WorldMapSector);
            bw.Write(saveGam.Date);
            bw.Write(saveGam.Unknown3);
            bw.Write(saveGam.PartyWorldMapSubSectorXCoord);
            bw.Write(saveGam.PartyWorldMapSubSectorYCoord);
            bw.Write(saveGam.MageLightSpellTimeRemaining);
            bw.Write(saveGam.Unknown4);
            bw.Write(saveGam.BeaconSpellPartyWorldMapSubSectorXCoord);
            bw.Write(saveGam.BeaconSpellPartyWorldMapSubSectorYCoord);
            bw.Write(saveGam.BeaconSpellWorldMapSector);
            bw.Write(saveGam.BeaconSpellWorldMapSubSectorXCoord);
            bw.Write(saveGam.BeaconSpellWorldMapSubSectorYCoord);
            bw.Write(saveGam.Unknown5);
            bw.Write(saveGam.WorldMapSubSectorXCoord);
            bw.Write(saveGam.WorldMapSubSectorYCoord);
            bw.Write(saveGam.DungeonMapSubSectorXCoord);
            bw.Write(saveGam.DungeonMapSubSectorYCoord);
            bw.Write(saveGam.CityMapSubSectorXCoord);
            bw.Write(saveGam.CityMapSubSectorYCoord);
            bw.Write(saveGam.ConjureCausewaySpellTimeRemaining);
            bw.Write(saveGam.PartyCityMapSubSectorXCoord);
            bw.Write(saveGam.PartyCityMapSubSectorYCoord);
            bw.Write(saveGam.PartyDungeonMapSubSectorXCoord);
            bw.Write(saveGam.PartyDungeonMapSubSectorYCoord);
            bw.Write(saveGam.Unknown6);
            bw.Write(saveGam.HeatColdCondition);
            bw.Write(saveGam.EyesOfTheFelineSpellTimeRemaining);
            bw.Write(saveGam.OldVizierQuestStatus);
            bw.Write(saveGam.Unknown7);
            bw.Write(saveGam.OracleGemQuestStatus);
            bw.Write(saveGam.DwarfKingQuestStatus);
            bw.Write(saveGam.Unknown8);
            bw.Write(saveGam.DwarfPrisonerQuestStatus);
            bw.Write(saveGam.DwarfPrisonerRewardQuestStatus);
            bw.Write(saveGam.Unknown9);
            bw.Write(saveGam.ThiefQuestStatus);
            bw.Write(saveGam.Unknown10);
            bw.Write(saveGam.LetterQuestStatus);
            bw.Write(saveGam.Unknown11);
            bw.Write(saveGam.ProphecyQuestStatus);
            bw.Write(saveGam.MarshQuestStatus);
            bw.Write(saveGam.Unknown12);
            bw.Write(saveGam.GuardianQuestStatus);
            bw.Write(saveGam.Unknown13);
            bw.Write(saveGam.CarnageOff);
            bw.Write(saveGam.GroupRunOff);
        }
    }
}
