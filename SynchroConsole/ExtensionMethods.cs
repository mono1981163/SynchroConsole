using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SynchroConsole
{
	//////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////
	public static class ExtensionMethods
	{
		private static bool FlagIsSet(DatePartFlags flags, DatePartFlags flag)
		{
			bool isSet = ((flags & flag) == flag);
			return isSet;
		}
		private static bool FlagIsSet(FileCompareFlags flags, FileCompareFlags flag)
		{
			bool isSet = ((flags & flag) == flag);
			return isSet;
		}

		public static bool Equal(this DateTime now, DateTime then, DatePartFlags flags)
		{
			bool isEqual = false;
			if (flags == DatePartFlags.Ticks)
			{
				isEqual = (now == then);
			}
			else
			{
				DatePartFlags equalFlags = DatePartFlags.Ticks;
				equalFlags |= (FlagIsSet(flags, DatePartFlags.Year)         && now.Year        == then.Year)        ? DatePartFlags.Year        : 0;
				equalFlags |= (FlagIsSet(flags, DatePartFlags.Month)        && now.Month       == then.Month)       ? DatePartFlags.Month       : 0;
				equalFlags |= (FlagIsSet(flags, DatePartFlags.Day)          && now.Day         == then.Day)         ? DatePartFlags.Day         : 0;
				equalFlags |= (FlagIsSet(flags, DatePartFlags.Hour)         && now.Hour        == then.Hour)        ? DatePartFlags.Hour        : 0;
				equalFlags |= (FlagIsSet(flags, DatePartFlags.Minute)       && now.Minute      == then.Minute)      ? DatePartFlags.Minute      : 0;
				equalFlags |= (FlagIsSet(flags, DatePartFlags.Second)       && now.Second      == then.Second)      ? DatePartFlags.Second      : 0;
				equalFlags |= (FlagIsSet(flags, DatePartFlags.Millisecond)  && now.Millisecond == then.Millisecond) ? DatePartFlags.Millisecond : 0;
				isEqual = (flags == equalFlags);
			}
			return isEqual;
		}

		public static bool Equal(this FileInfo fileA, FileInfo fileB, FileCompareFlags flags)
		{
			bool isEqual = (fileA.EqualityFlags(fileB, flags) == flags);
			return isEqual;
		}

		public static FileCompareFlags EqualityFlags(this FileInfo fileA, FileInfo fileB, FileCompareFlags flags)
		{
			FileCompareFlags equalFlags = FileCompareFlags.All;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.Attributes)    && fileA.Attributes        == fileB.Attributes)        ? FileCompareFlags.Attributes : 0;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.Created)       && fileA.CreationTime      == fileB.CreationTime)      ? FileCompareFlags.Attributes : 0;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.CreatedUTC)    && fileA.CreationTimeUtc   == fileB.CreationTimeUtc)   ? FileCompareFlags.Attributes : 0;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.Extension)     && fileA.Extension         == fileB.Extension)         ? FileCompareFlags.Attributes : 0;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.LastAccess)    && fileA.LastAccessTime    == fileB.LastAccessTime)    ? FileCompareFlags.Attributes : 0;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.LastAccessUTC) && fileA.LastAccessTimeUtc == fileB.LastAccessTimeUtc) ? FileCompareFlags.Attributes : 0;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.LastWrite)     && fileA.LastWriteTime     == fileB.LastWriteTime)     ? FileCompareFlags.Attributes : 0;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.LastWriteUTC)  && fileA.LastWriteTimeUtc  == fileB.LastWriteTimeUtc)  ? FileCompareFlags.Attributes : 0;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.Length)        && fileA.Length            == fileB.Length)            ? FileCompareFlags.Attributes : 0;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.FullName)      && fileA.FullName          == fileB.FullName)          ? FileCompareFlags.Attributes : 0;
			return equalFlags;
		}

	}
}
