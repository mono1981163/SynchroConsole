using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SynchroLib
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
		public string   FileName    { get; set; }
		public bool     IsDeleted   { get; set; }

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="fi">The FileInfo object represented by this object</param>
		public FileInfoEx(FileInfo info)
		{
			FileInfoObj = info;
			IsDeleted   = false;
			FileName    = FileInfoObj.FullName;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Determines if the specified file is "equal" to this file, bsased on the 
		/// specified comparison properties
		/// </summary>
		/// <param name="fileB">The file to be compared</param>
		/// <param name="flags">The properties to be compared for equality</param>
		/// <returns></returns>
		public bool Equal(FileInfoEx fileB, FileCompareFlags flags)
		{
			// assume no matches
			FileCompareFlags equalFlags = 0;
			// first, we compare the unrooted name (the filename without the 
			// root from/to path)
			if ((flags & FileCompareFlags.UnrootedName) == FileCompareFlags.UnrootedName)
			{
				equalFlags = (this.FileName == fileB.FileName) ? FileCompareFlags.UnrootedName : 0;
			}
			// and then we compare the actual FileInfo properties
			equalFlags |= this.FileInfoObj.EqualityFlags(fileB.FileInfoObj, flags);
			// if the flags that are set here are equal to the flags specified, this 
			// method will return true
			return (equalFlags == flags);
		}
	}

}
