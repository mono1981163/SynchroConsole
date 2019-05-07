using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SynchroLib
{
	public class FileInfoArgs : EventArgs
	{
		public int      UpdateCount { get; set; }
		public TimeSpan Elapsed     { get; set; }

		public FileInfoArgs(int count, TimeSpan elapsed)
		{
			this.UpdateCount = count;
			this.Elapsed     = elapsed;
		}
	}

	public delegate void FileInfoHandler(object sender, FileInfoArgs e);

}
