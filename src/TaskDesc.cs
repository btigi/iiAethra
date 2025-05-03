using System.Reflection;
using System.Text;

namespace ii.Aethra
{
    public class TaskDesc
    {
        public List<TaskDescRecord> Read(string filename)
        {
            const int MaxHeaderTextLength = 30;
            const int MaxBodyTextLength = 36;

            var done116hack = false;

            var result = new List<TaskDescRecord>();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            while (fs.Position < fs.Length)
            {
                var record = new TaskDescRecord();
                var length = br.BaseStream.ReadByte();
                var bytes = br.ReadBytes(MaxHeaderTextLength);
                var text = Encoding.UTF8.GetString(bytes.Take(length).ToArray());

                var lineCount = 11;
                record.Lines = new List<string>(lineCount);
                for (int i = 0; i < lineCount; i++)
                {
                    var lineLength = br.BaseStream.ReadByte();

                    while (lineLength > MaxBodyTextLength)
                    {
                        if (lineLength == 116 && !done116hack)
                        {
                            lineLength = br.BaseStream.ReadByte();
                            done116hack = true;
                        }
                        lineLength = br.BaseStream.ReadByte();
                    }

                    if (lineLength < 0)
                        continue;


                    var lineBytes = br.ReadBytes(lineLength);
                    var exclamationIndex = Array.IndexOf(lineBytes, (byte)'!');
                    if (exclamationIndex != -1)
                    {
                        // Skip bytes up to and including the '!' character
                        lineBytes = lineBytes.Skip(exclamationIndex + 1).ToArray();

                        // Read additional bytes equal to the number skipped
                        var additionalBytes = br.ReadBytes(exclamationIndex + 2);
                        lineBytes = lineBytes.Concat(additionalBytes).ToArray();
                    }

                    exclamationIndex = Array.IndexOf(lineBytes, (byte)'#');
                    if (exclamationIndex != -1)
                    {
                        // Skip bytes up to and including the '!' character
                        lineBytes = lineBytes.Skip(exclamationIndex + 1).ToArray();

                        // Read additional bytes equal to the number skipped
                        var additionalBytes = br.ReadBytes(exclamationIndex + 3);
                        lineBytes = lineBytes.Concat(additionalBytes).ToArray();
                    }


                    var lineText = Encoding.ASCII.GetString(lineBytes.Take(lineBytes.Length).ToArray());
                    record.Lines.Add(lineText);


                }
                record.Title = text;
                result.Add(record);
            }
            return result;
        }
    }

    public class TaskDescRecord
    {
        public string Title { get; set; } = string.Empty;
        public List<string> Lines { get; set; } = new List<string>();
    }
}