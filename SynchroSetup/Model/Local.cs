using SynchroLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SynchroSetup.Model
{
    public interface Local
    {
        string RunProcess(string flagName, SyncItem SyncParent, string targetName, FileInfoEx item, List<string> deletedDirList, List<FileInfoEx> newerList);
    }

    public class DeleteFile : Local
    {
        public string RunProcess(string flagName, SyncItem SyncParent, string targetName, FileInfoEx item, List<string> deletedDirList, List<FileInfoEx> newerList) {
            string[] deletedFileList = SyncParent.DeletedDirOrFile.Split(',');
            foreach (var deletedDirOrFile in deletedFileList)
            {
                string deletedFile = deletedDirOrFile.ToLower();
                if (Directory.Exists(System.IO.Path.Combine(SyncParent.SyncToPath, deletedFile)) || File.Exists(System.IO.Path.Combine(SyncParent.SyncToPath, deletedFile)))
                {
                    FileAttributes attr = File.GetAttributes(System.IO.Path.Combine(SyncParent.SyncToPath, deletedFile));
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        deletedDirList.Add(System.IO.Path.Combine(SyncParent.SyncToPath, deletedFile));
                    }
                    else if (deletedFile == item.FileName.ToLower())
                    {
                        File.Delete(targetName);
                    }
                }
            }
            return flagName;
        }
    } 

    public class WritableFile: Local
    {
        public string RunProcess(string flagName, SyncItem SyncParent, string targetName, FileInfoEx item, List<string> deletedDirList, List<FileInfoEx> newerList) {
            File.SetAttributes(targetName, FileAttributes.Normal);
            return flagName;
        }
    }
    public class RunFile : Local
    {
        public string RunProcess(string flagName, SyncItem SyncParent, string targetName, FileInfoEx item, List<string> deletedDirList, List<FileInfoEx> newerList) {
            string[] RunFileList = SyncParent.RunFile.Split(',');
            foreach (var runFile in RunFileList)
            {
                foreach (var items in newerList)
                {
                    if (File.Exists(System.IO.Path.Combine(SyncParent.SyncToPath, runFile)))
                    {
                        Process.Start(System.IO.Path.Combine(SyncParent.SyncToPath, runFile));
                        break;
                    }

                    else if (items.FileInfoObj.Extension.ToLower().Equals(runFile.ToLower()))
                        if (File.Exists(System.IO.Path.Combine(SyncParent.SyncToPath, items.FileName)))
                        {
                            Process.Start(System.IO.Path.Combine(SyncParent.SyncToPath, items.FileName));
                            break;
                        }
                }
            }
            return flagName;
        }
    }

    public abstract class LocalClass
    {
        public abstract Local GetProcessFlag(string flagName);
    } 

    public class FMLocal : LocalClass
    {
        public override Local GetProcessFlag(string flagName)
        {
            switch (flagName)
            {
                case "-d":
                    return new DeleteFile();
                case "-r":
                    return new RunFile();
                case "-w":
                    return new WritableFile();
                default:
                    throw new ApplicationException();
            }
        }
    }
}