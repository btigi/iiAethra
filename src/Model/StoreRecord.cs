namespace ii.Aethra.Model
{
    public class StoreRecord
    {
        public string Name { get; set; } = string.Empty;
        public List<ItemRecord> Items { get; set; } = new List<ItemRecord>();
    }

}