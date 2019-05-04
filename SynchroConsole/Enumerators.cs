using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SynchroConsole
{
	[Flags]
	public enum DatePartFlags {Ticks=0, Year=1, Month=2, Day=4, Hour=8, Minute=16, Second=32, Millisecond=64 };

	[Flags]
	public enum FileCompareFlags {All=0, FullName=1, Created=2, LastAccess=4, LastWrite=8, Length=16,
	                              CreatedUTC=32, LastAccessUTC=64, LastWriteUTC=128, Attributes=256, 
		                          Extension=512, UnrootedName=1024};
}
