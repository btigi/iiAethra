using System.Text;

namespace iiAethra
{
    public class Cfg
    {
        public CfgFile Read(string filename)
        {
            var cfg = new CfgFile();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using var br = new BinaryReader(fs);

            cfg.Unknown = br.ReadInt16();
            cfg.MusicStatus = br.ReadByte();
            cfg.SoundStatus = br.ReadByte();

            for (int i = 0; i < 4; i++)
            {
                var saveGameStatus = new SaveGameStatus();
                var saveGameNameLength = br.ReadByte();
                var saveGameName = br.ReadBytes(20); 
                var name = Encoding.UTF8.GetString(saveGameName.Take(saveGameNameLength).ToArray());
                saveGameStatus.SaveGameName = name;
                saveGameStatus.Unknown = br.ReadBytes(59);
                cfg.SaveGameStatus.Add(saveGameStatus);
            }

            return cfg;
        }

        public bool Write(CfgFile cfg, string filename)
        {
            try
            {
                using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
                using var bw = new BinaryWriter(fs);

                bw.Write(cfg.Unknown);
                bw.Write(cfg.MusicStatus);
                bw.Write(cfg.SoundStatus);

                foreach (var saveGameStatus in cfg.SaveGameStatus)
                {
                    var nameBytes = Encoding.UTF8.GetBytes(saveGameStatus.SaveGameName);
                    bw.Write((byte)nameBytes.Length);
                    var paddedNameBytes = nameBytes.Concat(new byte[20 - nameBytes.Length]).ToArray();
                    bw.Write(paddedNameBytes);
                    bw.Write(saveGameStatus.Unknown);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class CfgFile
    {
        public Int16 Unknown { get; set; }
        public byte MusicStatus { get; set; }
        public byte SoundStatus { get; set; }
        public List<SaveGameStatus> SaveGameStatus { get; set; } = [];
    }

    public class SaveGameStatus
    {
        public string SaveGameName { get; set; } = string.Empty;
        public byte[] Unknown { get; set; } = new byte[59];
    }
}