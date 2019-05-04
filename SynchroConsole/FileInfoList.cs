using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SynchroConsole
{
	//////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Represents a list of FileInfoEx objects
	/// </summary>
	public class FileInfoList : List<FileInfoEx>
	{
		string fromPath = @"D:\test";
		string toPath = @"D:\test2";

		public FileInfoList()
		{
		}

		public void Update(string path, bool incSubs)
		{
			FileInfoList newList = new FileInfoList();
			newList.GetFiles(path, incSubs);

			foreach (FileInfoEx item in newList)
			{
				item.IsDeleted = (!this.NewOrChanged(item));
			}
			var newerList = (from item in newList
							 where NewOrChanged(item)
							 select item).ToList();
			newList.Clear();
			newerList.Clear();
			foreach (FileInfoEx item in newerList)
			{
				try
				{
					//if (File.Exists(
					//File.Copy
				}
				catch (Exception)
				{
					throw;
				}
			}
		}

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

		private string StripRootPath(string filename)
		{
			filename = filename.Replace(filename.StartsWith(fromPath)?fromPath:toPath, "");
			return filename;
		}

		public bool NewOrChanged(FileInfoEx newFile)
		{
			var newCount = (from oldFile in this
							where oldFile.FileName == newFile.FileName
							select oldFile).Count();
			bool isNew = (newCount == 0);
			FileCompareFlags flags = FileCompareFlags.UnrootedName | 
									 FileCompareFlags.LastWrite    | 
									 FileCompareFlags.Length;
			var oldCount = (from oldFile in this
							//where ((oldFile.FileName == newFile.FileName) && (oldFile.FileInfoObj.LastWriteTime != newFile.FileInfoObj.LastWriteTime || oldFile.FileInfoObj.Length != newFile.FileInfoObj.Length))
							where oldFile.Equal(newFile, flags)
							select oldFile).Count();
			bool isChanged = (oldCount > 0);
			return (isNew || isChanged);
		}

	}
}
