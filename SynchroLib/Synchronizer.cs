using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WatcherLib;

namespace SynchroLib
{
	public class FolderWatchersCollection : List<WatcherEx> {};

	public class Synchronizer
	{
		FolderWatchersCollection m_folderWatchers = new FolderWatchersCollection();

		public Synchronizer()
		{
		}

	}
}
