using System.Text;

namespace ii.Aethra
{
    public class InfoDat
    {
        public List<InfoDatRecord> Read(string filename)
        {
            const int MaxTextLength = 38;

            var result = new List<InfoDatRecord>();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            while (fs.Position < fs.Length)
            {
                var record = new InfoDatRecord();
                var length = br.BaseStream.ReadByte();
                var bytes = br.ReadBytes(MaxTextLength);
                var text = Encoding.UTF8.GetString(bytes.Take(length).ToArray());

                var lineCount = br.ReadByte();
                record.Lines = new List<string>(lineCount);
                for (int i = 0; i < lineCount; i++)
                {
                    var lineLength = br.BaseStream.ReadByte();
                    var lineBytes = br.ReadBytes(MaxTextLength);
                    var lineText = Encoding.UTF8.GetString(lineBytes.Take(lineLength).ToArray());
                    record.Lines.Add(lineText);
                }
                record.Title = text;
                result.Add(record);
            }
            return result;
        }

        public void Write(List<InfoDatRecord> records, string filename)
        {
            const int MaxTextLength = 38;

            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            using var bw = new BinaryWriter(fs);
            foreach (var record in records)
            {
                var titleBytes = Encoding.UTF8.GetBytes(record.Title);
                bw.Write((byte)titleBytes.Length);
                bw.Write(titleBytes.Concat(new byte[MaxTextLength - titleBytes.Length]).ToArray());

                bw.Write((byte)record.Lines.Count);
                foreach (var line in record.Lines)
                {
                    var lineBytes = Encoding.UTF8.GetBytes(line);
                    bw.Write((byte)lineBytes.Length);
                    bw.Write(lineBytes.Concat(new byte[MaxTextLength - lineBytes.Length]).ToArray());
                }
            }
        }
    }

    public class InfoDatRecord
    {
        public string Title { get; set; } = string.Empty;
        public List<string> Lines { get; set; } = new List<string>();
    }
}