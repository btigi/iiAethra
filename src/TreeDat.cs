using ii.Aethra.Model;

namespace ii.Aethra
{
    // Combat-map scenery sprites (trees, bushes, pines, etc.)
    public class TreeDat
    {
        public const int Width = 64;
        public const int Height = 60;
        public const int RecordCount = 33;
        public const int TrailingBytes = 6;
        public const int ImageSizeBytes = Width * Height / 2; // 1920
        public const int RecordSizeBytes = ImageSizeBytes + TrailingBytes; // 1926
        public const int ExpectedFileSize = RecordCount * RecordSizeBytes; // 63558

        public List<TreeDatRecord> Read(string filename)
        {
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            return Read(br);
        }

        public List<TreeDatRecord> Read(BinaryReader br)
        {
            var g4 = new Graphics4();
            var result = new List<TreeDatRecord>(RecordCount);
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                var imageBytes = br.ReadBytes(ImageSizeBytes);
                var images = g4.Read(imageBytes, [(Width, Height, 1)]);
                result.Add(new TreeDatRecord
                {
                    Image = images[0],
                    Trailing = br.ReadBytes(TrailingBytes) // Unknown bytes
                });
            }

            return result;
        }

        public void Write(List<TreeDatRecord> records, string filename)
        {
            var g4 = new Graphics4();
            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            foreach (var record in records)
            {
                g4.Write([record.Image], fs);
                fs.Write(record.Trailing);
            }
        }
    }
}