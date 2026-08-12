namespace ii.Aethra.Model
{
    public class ItemRecord
    {
        public string Name { get; set; } = string.Empty;
        public byte[] Unknown { get; set; } = new byte[18];
        public double Cost { get; set; }
        public Int16 Id { get; set; }
        public Int16 Count { get; set; }
    }

}