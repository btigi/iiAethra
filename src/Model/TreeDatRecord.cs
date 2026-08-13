using SixLabors.ImageSharp;

namespace ii.Aethra.Model
{
    public class TreeDatRecord
    {
        public Image Image { get; set; } = null!;
        public byte[] Trailing { get; set; } = new byte[TreeDat.TrailingBytes];
    }
}
