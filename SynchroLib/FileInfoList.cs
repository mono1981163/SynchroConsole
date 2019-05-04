using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SynchroLib
{

	//////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Represents a list of FileInfoEx objects
	/// </summary>
	public class FileInfoList : List<FileInfoEx>
	{
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
							//where ((oldFile.FileName == newFile.FileName) && (oldFile.FileInfoObj.LastWriteTime != newFile.FileInfoObj.LastWriteTime || oldFile.FileInfoObj.Length != newFile.FileInfoObj.Length))
							where oldFile.Equal(newFile, m_compareFlags)
							select oldFile).Count();
			bool isChanged = (oldCount > 0);
			return (isNew || isChanged);
		}

		//--------------------------------------------------------------------------------
		public void Update(string path, bool incSubs)
		{
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
            List<string> deletedDirList = new List<string>();
			foreach (FileInfoEx item in newerList)
			{
				try
				{
					// build our file names
					string sourceName = System.IO.Path.Combine(SyncParent.SyncFromPath, item.FileName);
					string targetName = System.IO.Path.Combine(SyncParent.SyncToPath, item.FileName);
                    if (this.SyncParent.ForceDownlaod)
                    {
                        File.Copy(sourceName, targetName);
                    }
					if(!this.SyncParent.ExcludeDirOrFile.Equals(""))
                    {
                        bool isExist = false;
                        string[] exculdeFileList = this.SyncParent.ExcludeDirOrFile.Split(',');
                        for (int i = 0; i < exculdeFileList.Length; i++)
                        {
                            if (exculdeFileList[i].ToLower() == item.FileName.ToLower())
                            {
                                isExist = true;
                                break;
                            }
                        }
                        if (isExist)
                            continue;
                    }
                    // assume the path hasn't been verified
					bool pathVerified = false;
					// if the target file already exists
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
					File.Copy(sourceName, targetName);
					if (SyncParent.DeleteAfterSync)
					{
						File.Delete(sourceName);
					}
                    // Rui add
                    if (this.SyncParent.Writable && File.Exists(targetName))
                        File.SetAttributes(targetName, FileAttributes.Normal);
                    bool isDeleted = false;
                    if (!this.SyncParent.DeletedDirOrFile.Equals(""))
                    {
                        string[] deletedFileList = this.SyncParent.DeletedDirOrFile.Split(',');
                        for(int i = 0; i < deletedFileList.Length;i++)
                        {
                            string deletedFile = deletedFileList[i].ToLower();
                            if (deletedFile == item.FileName.ToLower())
                            {
                                isDeleted = true;
                                //FileAttributes attr = File.GetAttributes(targetName);
                                //if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                                //{
                                  
                                //}
                                //else
                                //{
                                    File.Delete(targetName);
                                //}
                            }
                        }
                    }

                    if (!isDeleted)
                        Process.Start(targetName);
				}
				catch (Exception ex)
				{
					throw new Exception(string.Format("Exception encountered while update the file {0}", item.FileName), ex);
				}
			}
            // Rui add
            //if (File.Exists(deleteDirName))
            //    File.Delete(deleteDirName);

		}

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
	}
}
