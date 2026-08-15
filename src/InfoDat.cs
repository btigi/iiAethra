using System.Text;
using ii.Aethra.Model;

namespace ii.Aethra
{
    public class InfoDat
    {
        public const int MaxTextLength = 38;
        public const int StringSizeBytes = 1 + MaxTextLength; // 39

        public const int Info1LineSlots = 5;
        public const int Info2LineSlots = 15;

        public const int Info1RecordSizeBytes = StringSizeBytes + 1 + Info1LineSlots * StringSizeBytes; // 235
        public const int Info2RecordSizeBytes = StringSizeBytes + 1 + Info2LineSlots * StringSizeBytes; // 625

        public const int Info1RecordCount = 82;
        public const int Info2RecordCount = 12;

        public List<InfoDatRecord> Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            var lineSlots = ResolveLineSlots(fs.Length);
            var recordSize = RecordSizeBytes(lineSlots);

            var result = new List<InfoDatRecord>();
            while (fs.Position + recordSize <= fs.Length)
            {
                var record = new InfoDatRecord
                {
                    Title = ReadPascalString(br, MaxTextLength)
                };

                var usedLineCount = br.ReadByte();
                if (usedLineCount > lineSlots)
                {
                    usedLineCount = (byte)lineSlots;
                }

                record.Lines = new List<string>(usedLineCount);
                for (var i = 0; i < lineSlots; i++)
                {
                    var lineText = ReadPascalString(br, MaxTextLength);
                    if (i < usedLineCount)
                    {
                        record.Lines.Add(lineText);
                    }
                }

                result.Add(record);
            }

            return result;
        }

        public void Write(List<InfoDatRecord> records, string filename)
        {
            var lineSlots = ResolveLineSlots(records);

            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            using var bw = new BinaryWriter(fs);
            foreach (var record in records)
            {
                WritePascalString(bw, record.Title, MaxTextLength);

                var usedLineCount = record.Lines.Count;
                if (usedLineCount > lineSlots)
                {
                    usedLineCount = lineSlots;
                }

                bw.Write((byte)usedLineCount);
                for (var i = 0; i < lineSlots; i++)
                {
                    var text = i < record.Lines.Count ? record.Lines[i] : string.Empty;
                    WritePascalString(bw, text, MaxTextLength);
                }
            }
        }

        public static int RecordSizeBytes(int lineSlots) => StringSizeBytes + 1 + lineSlots * StringSizeBytes;

        private static int ResolveLineSlots(long fileLength)
        {
            if (fileLength % Info2RecordSizeBytes == 0 && fileLength % Info1RecordSizeBytes != 0)
            {
                return Info2LineSlots;
            }

            return Info1LineSlots;
        }

        private static int ResolveLineSlots(List<InfoDatRecord> records)
        {
            var maxLines = 0;
            foreach (var record in records)
            {
                if (record.Lines.Count > maxLines)
                {
                    maxLines = record.Lines.Count;
                }
            }

            return maxLines > Info1LineSlots ? Info2LineSlots : Info1LineSlots;
        }

        private static string ReadPascalString(BinaryReader br, int maxLength)
        {
            var length = br.ReadByte();
            var bytes = br.ReadBytes(maxLength);
            if (length > maxLength)
            {
                length = (byte)maxLength;
            }

            return Encoding.UTF8.GetString(bytes.Take(length).ToArray());
        }

        private static void WritePascalString(BinaryWriter bw, string text, int maxLength)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            if (bytes.Length > maxLength)
            {
                bytes = bytes.Take(maxLength).ToArray();
            }

            bw.Write((byte)bytes.Length);
            bw.Write(bytes);
            if (bytes.Length < maxLength)
            {
                bw.Write(new byte[maxLength - bytes.Length]);
            }
        }
    }
}