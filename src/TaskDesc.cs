using System.Text;
using ii.Aethra.Model;

namespace ii.Aethra
{
    public class TaskDesc
    {
        public const int HeaderLineCount = 3;
        public const int HeaderLineMaxLength = 30;
        public const int BodyLineCount = 9;
        public const int BodyLineMaxLength = 36;
        public const int LineCount = HeaderLineCount + BodyLineCount;
        public const int RecordSizeBytes =
            HeaderLineCount * (1 + HeaderLineMaxLength) +
            BodyLineCount * (1 + BodyLineMaxLength); // 426
        public const int RecordCount = 178;
        public const int ExpectedFileSize = RecordCount * RecordSizeBytes; // 75828

        public List<TaskDescRecord> Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            return Read(br);
        }

        public List<TaskDescRecord> Read(BinaryReader br)
        {
            var result = new List<TaskDescRecord>();
            while (br.BaseStream.Position + RecordSizeBytes <= br.BaseStream.Length)
            {
                var record = new TaskDescRecord();
                for (var i = 0; i < HeaderLineCount; i++)
                {
                    record.Lines.Add(ReadPascalString(br, HeaderLineMaxLength));
                }

                for (var i = 0; i < BodyLineCount; i++)
                {
                    record.Lines.Add(ReadPascalString(br, BodyLineMaxLength));
                }

                result.Add(record);
            }

            return result;
        }

        public void Write(List<TaskDescRecord> records, string filename)
        {
            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            using var bw = new BinaryWriter(fs);
            foreach (var record in records)
            {
                for (var i = 0; i < HeaderLineCount; i++)
                {
                    var text = i < record.Lines.Count ? record.Lines[i] : string.Empty;
                    WritePascalString(bw, text, HeaderLineMaxLength);
                }

                for (var i = 0; i < BodyLineCount; i++)
                {
                    var index = HeaderLineCount + i;
                    var text = index < record.Lines.Count ? record.Lines[index] : string.Empty;
                    WritePascalString(bw, text, BodyLineMaxLength);
                }
            }
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