using ii.Aethra.Model;

namespace ii.Aethra
{
    public class RosRsc
    {
        public const int RecordCount = 64;
        public const int RecordSizeBytes = 1886;
        public const int ExpectedFileSize = RecordCount * RecordSizeBytes; // 120704

        public List<PartyMemberRecord> Read(string filename)
        {
            return new PartyDat().Read(filename);
        }

        public void Write(List<PartyMemberRecord> records, string filename)
        {
            new PartyDat().Write(records, filename);
        }
    }
}
