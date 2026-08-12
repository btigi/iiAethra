using ii.Aethra.Model;

namespace ii.Aethra
{
    public class T1RscRecord
    {
        public T1ItemRecord[] Items { get; set; } = new T1ItemRecord[1000];
        public T1InteractiveElementRecord[] InteractiveElements { get; set; } = new T1InteractiveElementRecord[100];
        public T1FloorItemRecord[] FloorItems { get; set; } = new T1FloorItemRecord[100];
        public T1InteractedElementRecord[] InteractedElementRecords { get; set; } = new T1InteractedElementRecord[400];
    }
}