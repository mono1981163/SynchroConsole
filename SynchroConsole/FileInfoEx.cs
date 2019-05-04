using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SynchroConsole
{
	//////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// We need something more than just the FileInfo class because we have to strip 
	/// off the root folder of the file in order to perform some coparisons in other 
	/// places. Normally, I would just derive from the object in question, but the 
	/// FileInfo class is sealed.  Isn't that convenient?
	/// </summary>
	public class FileInfoEx
	{
		public FileInfo FileInfoObj { get; set; }
		public string FileName { get; set; }
		public bool IsDeleted { get; set; }

		public FileInfoEx(FileInfo fi)
		{
			FileInfoObj = fi;
			IsDeleted = false;
			FileName = FileInfoObj.FullName;
		}

		public bool Equal(FileInfoEx fileB, FileCompareFlags flags)
		{
			FileCompareFlags equalFlags = (this.FileName == fileB.FileName) ? FileCompareFlags.UnrootedName : 0;
			equalFlags |= this.FileInfoObj.EqualityFlags(fileB.FileInfoObj, flags);
			return (equalFlags == flags);
		}
	}

}
