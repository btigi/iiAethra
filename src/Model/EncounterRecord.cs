namespace ii.Aethra.Model
{
    public class EncounterRecord
    {
        public byte NameLength { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte MonsterCount { get; set; }
        public Int16 MonsterId1 { get; set; }
        public Int16 MonsterId2 { get; set; }
        public Int16 MonsterId3 { get; set; }
        public Int16 MonsterId4 { get; set; }
        public byte MonsterId1CountMax { get; set; }
        public byte MonsterId2CountMax { get; set; }
        public byte MonsterId3CountMax { get; set; }
        public byte MonsterId4CountMax { get; set; }
        public byte MonsterId1CountMin { get; set; }
        public byte MonsterId2CountMin { get; set; }
        public byte MonsterId3CountMin { get; set; }
        public byte MonsterId4CountMin { get; set; }
    }
}