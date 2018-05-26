using fNbt;
using KeyKeeper.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Managers.Replays
{
    internal static class ReplayFileManager
    {

        public static void SaveTagToFile(NbtCompound rootTag, string filePath, NbtCompression compression)
        {
            try
            {
                new NbtFile(rootTag).SaveToFile(filePath, compression);
            }catch(Exception ex)
            {
                throw new ReplayException("Unable to save tag to file.", ex);
            }
        }
       
        public static NbtCompound LoadRootTagFromFile(string filePath, NbtCompression compression)
        {
            NbtFile file = new NbtFile();
            file.LoadFromFile(filePath, compression, null);
            return file.RootTag;
        }

        public static void CheckAndCreateReplayFolder(string replayFolderPath)
        {
            if (!Directory.Exists(replayFolderPath))
            {
                Directory.CreateDirectory(replayFolderPath);
            }
        }
    }
}
