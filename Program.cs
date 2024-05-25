//Written for Virtual Pool 4, https://store.steampowered.com/app/336150
//and Virtual Pool 4 Multiplayer. https://store.steampowered.com/app/432310
using System.IO;

namespace Virtual_Pool_4_Extractor
{
    class Program
    {
        static BinaryReader br;
        static void Main(string[] args)
        {
            br = new(File.OpenRead(args[0]));
            br.ReadInt32();
            br.ReadInt32();
            br.BaseStream.Position = 24;
            int fileCount = br.ReadInt32();

            System.Collections.Generic.List<Subfile> subfiles = new();
            for (int i = 0; i < fileCount; i++)
                subfiles.Add(new());

            string path = Path.GetDirectoryName(args[0]) + "\\" + Path.GetFileNameWithoutExtension(args[0]) + "\\";
            for (int i = 0; i < subfiles.Count; i++)
            {
                br.BaseStream.Position = subfiles[i].start;
                Directory.CreateDirectory(path + Path.GetDirectoryName(subfiles[i].name));
                BinaryWriter bw = new(File.Create(path + subfiles[i].name));
                bw.Write(br.ReadBytes(subfiles[i].size));
                bw.Close();
            }
        }

        class Subfile
        {
            public string name = new string(br.ReadChars(48)).TrimEnd('\0', '�');
            public int start = br.ReadInt32();
            public int size = br.ReadInt32();
        }
    }
}
