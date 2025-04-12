using System.Text;

namespace iiAethra
{
    public class StoresDat
    {
        public List<StoreRecord> Read(string filename)
        {
            const int MaxStoreNameLength = 40;
            const int MaxItemNameLength = 22;

            const int ShopSize = 2541;

            var result = new List<StoreRecord>();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);            
            while (fs.Position < fs.Length && fs.Position + ShopSize < fs.Length)
            {
                var record = new StoreRecord();
                var nameLength = br.ReadByte();
                var name = br.ReadBytes(MaxStoreNameLength);
                record.Name = Encoding.UTF8.GetString(name.Take(nameLength).ToArray());

                for (int i = 0; i < 50; i++)
                {
                    var item = new ItemRecord();
                    var itemNameLength = br.ReadByte();
                    item.Name = Encoding.ASCII.GetString(br.ReadBytes(MaxItemNameLength).Take(itemNameLength).ToArray());
                    item.Unknown = br.ReadBytes(18);
                    record.Items.Add(item);
                }

                for (int i = 0; i < 50; i++)
                {
                    var item = record.Items.ElementAt(i);
                    var cost = br.ReadBytes(6);
                    item.Cost = Utils.Real48Convert(cost);
                }

                for (int i = 0; i < 50; i++)
                {
                    var item = record.Items.ElementAt(i);
                    item.Id = br.ReadInt16();
                }

                for (int i = 0; i < 50; i++)
                {
                    var item = record.Items.ElementAt(i);
                    item.Count = br.ReadByte ();
                }

                result.Add(record);
            }
            return result;
        }
    }

    public class StoreRecord
    {
        public string Name { get; set; } = string.Empty;
        public List<ItemRecord> Items { get; set; } = new List<ItemRecord>();
    }

    public class ItemRecord
    {
        public string Name { get; set; } = string.Empty;
        public byte[] Unknown { get; set; } = new byte[18];
        public double Cost { get; set; }
        public Int16 Id { get; set; }
        public Int16 Count { get; set; }
    }

}