using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using AppSettingsLib;
using SynchroSetup.Model;
namespace SynchroLib
{ 
	//////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Represents a list of FileInfoEx objects
	/// </summary>
	public class FileInfoList : List<FileInfoEx>
	{
        PatternsClass patternsClass = new FMPatterns();
        LocalClass localClass = new FMLocal();
        PathClass pathClass = new FMPath();

        protected string APP_DATA_FOLDER = "PaddedwallSync";
        protected string APP_DATA_FILENAME = "Settings.xml";
        protected string SETTINGS_KEYNAME = "Settings";
        protected string           m_syncFromPath = "";
		protected string           m_syncToPath   = "";
		protected FileCompareFlags m_compareFlags = FileCompareFlags.UnrootedName | 
												    FileCompareFlags.LastWrite    | 
												    FileCompareFlags.Length;

		public    int              Updates    { get; private set; }
		public    SyncItem         SyncParent { get; set;         }

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="from">The folder we're syncing from (where the files are copied from)</param>
		/// <param name="to">The folder we're syncing to (where the files wil be copied to)</param>
		public FileInfoList(string from, string to)
		{
			this.SyncParent     = null;
			this.m_syncFromPath = from;
			this.m_syncToPath   = to;
		}

		//--------------------------------------------------------------------------------
		public FileInfoList(SyncItem parent)
		{
			this.SyncParent = parent;
			m_syncFromPath  = this.SyncParent.SyncFromPath;
			m_syncToPath    = this.SyncParent.SyncToPath;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Gets a list of files in the specified folder
		/// </summary>
		/// <param name="path"></param>
		/// <param name="incSubs"></param>
		public void GetFiles(string path, bool incSubs)
		{
			DirectoryInfo dirInfo = new DirectoryInfo(path);
			FileInfo[] files = dirInfo.GetFiles("*.*", (incSubs) ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
			foreach(FileInfo file in files)
			{
				FileInfoEx newFile = new FileInfoEx(file);
				newFile.FileName = StripRootPath(newFile.FileName);
				this.Add(newFile);
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Strips the root path from the filename, and stores it in the FileName 
		/// property of the item. This needs to be done so that we can compare filename 
		/// within the syncing folder heirarchy, regardless of where the folders actually 
		/// live.
		/// </summary>
		/// <param name="filename">Fileanem to be unrooted</param>
		/// <returns>Unrooted filename</returns>
		private string StripRootPath(string filename)
		{
			filename = filename.Replace(filename.StartsWith(m_syncFromPath) ? m_syncFromPath+"\\" : m_syncToPath+"\\", "");
			return filename;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Determines if the file is new or changed within the TO folde 
		/// </summary>
		/// <param name="newFile"></param>
		/// <returns></returns>
		public bool NewOrChanged(FileInfoEx newFile)
		{
			var newCount = (from oldFile in this
							where oldFile.FileName == newFile.FileName
							select oldFile).Count();
			bool isNew = (newCount == 0);
			var oldCount = (from oldFile in this
							where oldFile.Equal(newFile, m_compareFlags)
							select oldFile).Count();
			bool isChanged = (oldCount > 0);
			return (isNew || isChanged);
		}
       
        //--------------------------------------------------------------------------------
        public void Update(string path, bool incSubs)
		{
            // read from Settings.xml file
            string FullyQualifiedPath = System.IO.Path.Combine(CreateAppDataFolder(APP_DATA_FOLDER), APP_DATA_FILENAME);
            SyncItemCollection SyncItems = new SyncItemCollection();

            try
            {
                XDocument doc = XDocument.Load(FullyQualifiedPath);
                XElement root = doc.Element("ROOT");
                if (root != null)
                {
                    XElement settings = root.Element(SETTINGS_KEYNAME);
                    if (SyncParent != null)
                    {
                        this.SyncParent = null;
                    }
                    SyncItems = new SyncItemCollection(root.Element("SyncItems"));
                    this.SyncParent = SyncItems[0];
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Select synchronize folders");
            }
            

            FileInfoList newList = new FileInfoList(SyncParent.SyncFromPath, this.SyncParent.SyncToPath);
			newList.GetFiles(path, incSubs);

			foreach (FileInfoEx item in newList)
			{
				item.IsDeleted = (!this.NewOrChanged(item));
			}
			var newerList = (from item in newList
							 where NewOrChanged(item)
							 select item).ToList();
			newList.Clear();
			this.Updates = newerList.Count;

            // Rui add
            FileInfoList targetList = new FileInfoList(SyncParent.SyncFromPath, this.SyncParent.SyncToPath);
            targetList.GetFiles(SyncParent.SyncToPath, incSubs);
            var targetAllFileList = (from item in targetList
                                     where NewOrChanged(item)
                                     select item).ToList();
            targetList.Clear();
            if (SyncParent.Mirror)
            {
                SynchroSetup.Model.Path MirrorFlag = pathClass.GetProcessFlag("-m");
                MirrorFlag.RunProcess("-m", SyncParent, null, null, targetAllFileList, newerList);
            }
             
            List<string> deletedDirList = new List<string>();
            string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString());
            bool isFirstStart = true;
            // parse FileStates
            JObject filestates = null;
            if (!this.SyncParent.FileStates.Equals(""))
            {
                filestates = JObject.Parse(this.SyncParent.FileStates);
            }
            foreach (FileInfoEx item in newerList)
			{
				try
				{
                    item.FileState = (int)FileStates.Release;
                    if (filestates != null && filestates[item.FileName] != null)
                    {
                        item.FileState = (int)Enum.Parse(typeof(FileStates), filestates[item.FileName].ToString());
                    }
					// build our file names
					string sourceName = System.IO.Path.Combine(SyncParent.SyncFromPath, item.FileName);
					string targetName = System.IO.Path.Combine(SyncParent.SyncToPath, item.FileName);
                    string tempFileName = System.IO.Path.Combine(tempPath, item.FileName);

                    if (!this.SyncParent.ExcludeDirOrFile.Equals(""))
                    {
                        Patterns excludeFlag = patternsClass.GetProcessFlag("-e");
                        if (excludeFlag.RunProcess(false, SyncParent, null, sourceName, null, null, null, 0))
                            continue;
                    } 
                    if (!this.SyncParent.Pattern.Equals(""))
                    {
                        Patterns patternFlag = patternsClass.GetProcessFlag("-p");
                        if (patternFlag.RunProcess(true, SyncParent, item.FileName, null, null, item, null, 0))
                            continue;
                    }

                    if (!this.SyncParent.State.Equals("") /*&& !this.SyncParent.FileStates.Equals("")*/)
                    {
                        Patterns patternStateFlag = patternsClass.GetProcessFlag("-s");
                        if (!patternStateFlag.RunProcess(false, null, null, null, null, null, SyncParent.State.Split(','), item.FileState))
                            continue;
                    }

                    if (!this.SyncParent.IgnoreState.Equals(""))
                    {
                        Patterns ignoreStateFlag = patternsClass.GetProcessFlag("-is");
                        if (ignoreStateFlag.RunProcess(false, null, null, null, null, null, SyncParent.IgnoreState.Split(','), item.FileState))
                            continue;
                    }
                    
                    if (!this.SyncParent.FolderMapping.Equals(""))
                    {
                        SynchroSetup.Model.Path folderMappingFlag = pathClass.GetProcessFlag("-fm");
                        targetName = folderMappingFlag.RunProcess("-fm", SyncParent, sourceName, targetName, null, null);
                    }
                    
                    if(this.SyncParent.ForceDownlaod && isFirstStart)
                    {
                        SynchroSetup.Model.Path forceDownFlag = pathClass.GetProcessFlag("-fm");
                        forceDownFlag.RunProcess("-f", SyncParent, null, null, null, null);
                        isFirstStart = false;
                    }

                    if (this.SyncParent.Lock)
                    {
                        if (!Directory.Exists(tempPath))
                            Directory.CreateDirectory(tempPath);
                        Patterns lockFlag = patternsClass.GetProcessFlag("-lck");
                        lockFlag.RunProcess(true, SyncParent, null, sourceName, tempFileName, item, null, 0);
                        lockFlag.RunProcess(false, SyncParent,null, tempFileName, targetName, item, null, 0);
                    }
                    else
                        FileDeleteAndCopy(sourceName, targetName, false, item, true);

                    if (this.SyncParent.Writable && File.Exists(targetName))
                    {
                        Local writableFlag = localClass.GetProcessFlag("-w");
                        writableFlag.RunProcess("-w", null, targetName, null, null, null);
                    }

                    if (!this.SyncParent.DeletedDirOrFile.Equals(""))
                    {
                        Local deleteFlag = localClass.GetProcessFlag("-d");
                        deleteFlag.RunProcess("-d", SyncParent, targetName, item, deletedDirList, null);
                    }
                }
				catch (Exception ex)
				{
					throw new Exception(string.Format("Exception encountered while update the file {0}", item.FileName), ex);
				}
			}
            
            if (deletedDirList.Count != 0)
                DeleteDirectory(deletedDirList);
              
            if (!this.SyncParent.RunFile.Equals(""))
            {
                Local runFlag = localClass.GetProcessFlag("-r");
                runFlag.RunProcess("-r", SyncParent, null, null, null, newerList);
            }
        }
        // << Rui add
       
        
        private void DeleteDirectory(List<string> deletedDirList)
        {
            foreach (var deletedFile in deletedDirList)
            {
                if (Directory.Exists(System.IO.Path.Combine(SyncParent.SyncToPath, deletedFile)))
                    Directory.Delete(System.IO.Path.Combine(SyncParent.SyncToPath, deletedFile));
            }
        }

        private void FileDeleteAndCopy(string sourceName, string targetName, bool pathVerified, FileInfoEx item, bool copyFlag)
        {
            if (File.Exists(targetName))
            {
                // back it up if necessary
                if (SyncParent.BackupBeforeSync)
                {
                    // copy to backup folder
                    BackupFile(item, targetName);
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
            if (copyFlag)
                File.Copy(sourceName, targetName);
            else
                File.Move(sourceName, targetName);
            if (SyncParent.DeleteAfterSync)
            {
                File.Delete(sourceName);
            }
        }
        // >>

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Verifies that the path we need in the TO folder already exists. If it doesn't, 
        /// we build it one sub-folder at a time.
        /// </summary>
        /// <param name="path"></param>
        private void VerifyPath(string path)
		{
			string   pathSoFar = "";
			try
			{
				string[] dirParts  = path.Split('\\');
				int      pos       = 0;
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

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Backs up the specified file, keeping old versions if they exist.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="sourceName"></param>
		private void BackupFile(FileInfoEx item, string sourceName)
		{
			try
			{
				string backupFile = System.IO.Path.Combine(SyncParent.BackupPath, item.FileName);
				VerifyPath(System.IO.Path.GetDirectoryName(backupFile));

				string path          = System.IO.Path.GetDirectoryName           (backupFile);
				string baseFileName  = System.IO.Path.GetFileNameWithoutExtension(backupFile);
				string fileExtension = System.IO.Path.GetExtension               (backupFile);
				int counter          = 0;
				bool backedUp        = false;

				do
				{
					if (File.Exists(backupFile))
					{
						counter++;
						string filename = string.Format("{0}({1:000}){2}", baseFileName, counter, fileExtension);
						backupFile      = System.IO.Path.Combine(path, filename);
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
        protected string CreateAppDataFolder(string folderName)
        {
            string appDataPath = "";
            string dataFilePath = "";

            folderName = folderName.Trim();
            if (folderName != "")
            {
                try
                {
                    // Set the directory where the file will come from.  The folder name 
                    // returned will be different between XP and Vista. Under XP, the default 
                    // folder name is "C:\Documents and Settings\All Users\Application Data\[folderName]"
                    // while under Vista, the folder name is "C:\Program Data\[folderName]".
                    appDataPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                }
                catch (Exception)
                {
                    throw;
                }

                if (folderName.Contains("\\"))
                {
                    string[] path = folderName.Split('\\');
                    int folderCount = 0;
                    int folderIndex = -1;
                    for (int i = 0; i < path.Length; i++)
                    {
                        string folder = path[i];
                        if (folder != "")
                        {
                            if (folderIndex == -1)
                            {
                                folderIndex = i;
                            }
                            folderCount++;
                        }
                    }
                    if (folderCount != 1)
                    {
                        throw new Exception("Invalid folder name specified (this function only creates the root app data folder for the application).");
                    }
                    folderName = path[folderIndex];
                }
            }
            if (folderName == "")
            {
                throw new Exception("Processed folder name resulted in an empty string.");
            }
            try
            {
                dataFilePath = System.IO.Path.Combine(appDataPath, folderName);
                if (!Directory.Exists(dataFilePath))
                {
                    Directory.CreateDirectory(dataFilePath);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dataFilePath;
        }
    }
}
