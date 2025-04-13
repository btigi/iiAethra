namespace iiAethra
{
    public class T1Rsc
    {
        public List<T1RscRecord> Read(string filename)
        {
            var result = new List<T1RscRecord>();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);
            while (fs.Position < fs.Length)
            {
                var thisRecord = new T1RscRecord();
                for (int i = 0; i < 1000; i++)
                {
                    thisRecord.Items[i] = new T1ItemRecord
                    {
                        ItemId = br.ReadInt16(),
                        ChargesRemaining = br.ReadByte(),
                        XCoordinate = br.ReadByte(),
                        YCoordinate = br.ReadByte(),
                        Identified = br.ReadByte()
                    };
                }
                _ = br.ReadInt16(); // total number of items

                for (int i = 0; i < 100; i++)
                {
                    thisRecord.InteractiveElements[i] = new T1InteractiveElementRecord
                    {
                        XCoordinate = br.ReadByte(),
                        YCoordinate = br.ReadByte(),
                        ElementIndex = br.ReadInt16()
                    };
                }
                _ = br.ReadByte(); // total number of interactive elements

                for (int i = 0; i < 100; i++)
                {
                    thisRecord.FloorItems[i] = new T1FloorItemRecord
                    {
                        XCoordinate = br.ReadByte(),
                        YCoordinate = br.ReadByte(),
                        IconOffset = br.ReadInt16()
                    };
                }
                _ = br.ReadByte(); // total number of floor items in this dungeon sector

                for (int i = 0; i < 400; i++)
                {
                    thisRecord.InteractedElementRecords[i] = new T1InteractedElementRecord
                    {
                        XCoordinate = br.ReadByte(),
                        YCoordinate = br.ReadByte(),
                        OverlayOffset = br.ReadInt16()
                    };
                }
                _ = br.ReadInt16(); // total number of interacted elements

                result.Add(thisRecord);
            }
            return result;
        }
    }

    public class T1RscRecord
    {
        public T1ItemRecord[] Items { get; set; } = new T1ItemRecord[1000];
        public T1InteractiveElementRecord[] InteractiveElements { get; set; } = new T1InteractiveElementRecord[100];
        public T1FloorItemRecord[] FloorItems { get; set; } = new T1FloorItemRecord[100];
        public T1InteractedElementRecord[] InteractedElementRecords { get; set; } = new T1InteractedElementRecord[400];
    }

    public class T1ItemRecord
    {
        public Int16 ItemId { get; set; }
        public byte ChargesRemaining { get; set; }
        public byte XCoordinate { get; set; }
        public byte YCoordinate { get; set; }
        public byte Identified { get; set; }
    }

    public class T1InteractiveElementRecord
    {
        public byte XCoordinate { get; set; }
        public byte YCoordinate { get; set; }
        public Int16 ElementIndex { get; set; }
    }

    public class T1FloorItemRecord
    {
        public byte XCoordinate { get; set; }
        public byte YCoordinate { get; set; }
        public Int16 IconOffset { get; set; }
    }

    public class T1InteractedElementRecord
    {
        public byte XCoordinate { get; set; }
        public byte YCoordinate { get; set; }
        public Int16 OverlayOffset { get; set; }
    }
}