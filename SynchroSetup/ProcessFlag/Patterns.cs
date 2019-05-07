using SynchroLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SynchroSetup.Model
{
    public interface IPattern
    {
        bool RunProcess(bool flag, SyncItem SyncParent, string fileName, string sourceName, string targetName, FileInfoEx item, string[] fileState, int fileStateValue);
    }

    public class ExcludeFile : IPattern
    {
        public bool RunProcess(bool flag, SyncItem SyncParent, string fileName, string sourceName, string targetName, FileInfoEx item, string[] fileState, int fileStateValue)
        {
            string[] exculdeFileList = SyncParent.ExcludeDirOrFile.Split(',');
            foreach (var exculdeDirOrFile in exculdeFileList)
            {
                if (Directory.Exists(System.IO.Path.Combine(SyncParent.SyncFromPath, exculdeDirOrFile).ToLower()) || File.Exists(System.IO.Path.Combine(SyncParent.SyncFromPath, exculdeDirOrFile).ToLower()))
                {
                    if (sourceName.ToLower().Contains(System.IO.Path.Combine(SyncParent.SyncFromPath, exculdeDirOrFile).ToLower()))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            return flag;
        }
    }

    public class Pattern : IPattern
    {
        public bool RunProcess(bool flag, SyncItem SyncParent, string fileName, string sourceName, string targetName, FileInfoEx item, string[] fileState, int fileStateValue)
        {
            bool isFound = flag; 
            string[] extensionList = SyncParent.Pattern.Split(',');
            foreach (var extension in extensionList)
            {
                if (extension.ToLower() == item.FileInfoObj.Extension.ToLower())
                {
                    isFound = false;
                    break;
                }
            }
            return isFound;
        }
    }

    public class LockFile : IPattern
    {
        public bool RunProcess(bool flag, SyncItem SyncParent, string fileName, string sourceName, string targetName, FileInfoEx item, string[] fileState, int fileStateValue)
        {
            bool pathVerified = false;
            if (File.Exists(targetName))
            {
                // back it up if necessary
                if (SyncParent.BackupBeforeSync)
                {
                    // copy to backup folder
                    BackupFile(SyncParent,item, targetName);
                }
                // delete  the file we're replacing
                File.Delete(targetName);
                // since the file exists, the path must exist as well 
                pathVerified = true;
            }
            if (!pathVerified)
            {
                VerifyPath(System.IO.Path.GetDirectoryName(targetName));
            }
            if (flag)
                File.Copy(sourceName, targetName);
            else
                File.Move(sourceName, targetName);
            if (SyncParent.DeleteAfterSync)
            {
                File.Delete(sourceName);
            }
            return flag;
        }

        private void VerifyPath(string path)
        {
            string pathSoFar = "";
            try
            {
                string[] dirParts = path.Split('\\');
                int pos = 0;
                while (pos < dirParts.Length)
                {
                    if (pos == 0)
                    {
                        pathSoFar = dirParts[0] + "\\";
                    }
                    else
                    {
                        pathSoFar = System.IO.Path.Combine(pathSoFar, dirParts[pos]);
                    }
                    pos++;
                    if (!Directory.Exists(pathSoFar))
                    {
                        Directory.CreateDirectory(pathSoFar);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Exception encountered in FileInfoList.VerifyPath - path was {0}, pathSoFar was {1}", path, pathSoFar), ex);
            }
        }
        private void BackupFile(SyncItem SyncParent ,FileInfoEx item, string sourceName)
        {
            try
            {
                string backupFile = System.IO.Path.Combine(SyncParent.BackupPath, item.FileName);
                VerifyPath(System.IO.Path.GetDirectoryName(backupFile));

                string path = System.IO.Path.GetDirectoryName(backupFile);
                string baseFileName = System.IO.Path.GetFileNameWithoutExtension(backupFile);
                string fileExtension = System.IO.Path.GetExtension(backupFile);
                int counter = 0;
                bool backedUp = false;

                do
                {
                    if (File.Exists(backupFile))
                    {
                        counter++;
                        string filename = string.Format("{0}({1:000}){2}", baseFileName, counter, fileExtension);
                        backupFile = System.IO.Path.Combine(path, filename);
                    }
                    else
                    {
                        File.Copy(sourceName, backupFile);
                        backedUp = true;
                    }
                } while (!backedUp);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Exception while backing up the file '{0}' - {1}", item.FileName, ex.Message), ex);
            }
        }

    }

    public class StateFile : IPattern
    {
        public bool RunProcess(bool flag, SyncItem SyncParent, string fileName, string sourceName, string targetName, FileInfoEx item, string[] fileState, int fileStateValue)
        {
            bool isFound = flag;
            foreach (var state in fileState)
            {
                int stateValue = -1;
                switch (state.ToLower())
                {
                    case "release":
                        stateValue = 0;
                        break;
                    case "work":
                        stateValue = 1;
                        break;
                    case "init":
                        stateValue = 2;
                        break;
                    default:
                        break;
                }
                if (fileStateValue == stateValue)
                {
                    isFound = true;
                    break;
                }
            }
            return isFound;
        }
    }
    public abstract class PatternsClass
    {
        public abstract IPattern GetProcessFlag(string flagName);
    }
    public class FMPatterns : PatternsClass
    {
        public override IPattern GetProcessFlag(string flagName)
        {
            switch (flagName)
            {
                case "-e":
                    return new ExcludeFile();
                case "-p":
                    return new Pattern();
                case "-lck":
                    return new LockFile();
                case "-s":
                    return new StateFile();
                case "-is":
                    return new StateFile();
                
                default:
                    throw new ApplicationException();
            }
        }
    }

    //    // flag -i
    //    public string SyncToPath { get; set; }
    //    // flag -o
    //    public string SyncFromPath { get; set; }
}
