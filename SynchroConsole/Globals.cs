using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SynchroLib;

namespace SynchroConsole
{
	public static class Globals
	{
		public static SyncItemCollection SyncItems { get; set; }

		static Globals()
		{
			SyncItems = new SyncItemCollection();
		}

	}

}
