using SynchroLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SynchroSetup.Model
{
    public interface Path
    {
        string RunProcess(string flagName, SyncItem SyncParent, string sourceName, string targetName, List<FileInfoEx> targetAllFileList, List<FileInfoEx> newerList);
    }

    public class FolderMapping : Path
    {
        public string RunProcess(string flagName, SyncItem SyncParent, string sourceName, string targetName, List<FileInfoEx> targetAllFileList, List<FileInfoEx> newerList)
        {
            string[] folderMappingList = SyncParent.FolderMapping.Split(',');
            foreach (var folderMapping in folderMappingList)
            {
                var source = folderMapping.Split(';')[0].Split('=')[1];
                var target = folderMapping.Split(';')[1].Split('=')[1];
                if (sourceName.ToLower().Contains(source.ToLower()))
                {
                    StringBuilder builder = new StringBuilder(targetName);
                    builder.Replace(source, target);
                    targetName = builder.ToString();
                }
            }
            return targetName;
        }
    }
    public class ForceDownload : Path
    {
        public string RunProcess(string flagName, SyncItem SyncParent, string sourceName, string targetName, List<FileInfoEx> targetAllFileList, List<FileInfoEx> newerList)
        {
            Directory.Delete(SyncParent.SyncToPath, true);
            Directory.CreateDirectory(SyncParent.SyncToPath);
            return flagName; // return false
        }
    }
    public class Mirror : Path
    {
        public string RunProcess(string flagName, SyncItem SyncParent, string sourceName, string targetName, List<FileInfoEx> targetAllFileList, List<FileInfoEx> newerList)
        {
            foreach (FileInfoEx item in targetAllFileList)
            {
                bool isFound = false;
                foreach (FileInfoEx file in newerList)
                {
                    if (item.FileName == file.FileName)
                    {
                        isFound = true;
                        break;
                    }
                }
                if (!isFound)
                    File.Delete(System.IO.Path.Combine(SyncParent.SyncToPath, item.FileName));
            }
            return flagName;
        }
    }
    public abstract class PathClass
    {
        public abstract Path GetProcessFlag(string flagName);
    }

    public class FMPath : PathClass
    {
        public override Path GetProcessFlag(string flagName)
        {
            switch (flagName)
            {
                case "-fm":
                    return new FolderMapping();
                case "-f":
                    return new ForceDownload();
                case "-m":
                    return new Mirror();
                default:
                    throw new ApplicationException();
            }
        }
    }
}
