using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SynchroLib
{
	//////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////
	public static class ExtensionMethods
	{

		//////////////////////////////////////////////////////////////////////////////////
		// DateTime methods                                                               
		private static bool FlagIsSet(DatePartFlags flags, DatePartFlags flag)
		{
			bool isSet = ((flags & flag) == flag);
			return isSet;
		}

		public static DateTime ClearSeconds(this DateTime now)
		{
			TimeSpan seconds = new TimeSpan(0, 0, 0, now.Second, now.Millisecond);
			DateTime newNow = now.Subtract(seconds);
			return newNow;
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

		//////////////////////////////////////////////////////////////////////////////////
		// FileInfo methods                                                               

		//--------------------------------------------------------------------------------
		private static bool FlagIsSet(FileCompareFlags flags, FileCompareFlags flag)
		{
			bool isSet = ((flags & flag) == flag);
			return isSet;
		}

		//--------------------------------------------------------------------------------
		public static bool Equal(this FileInfo fileA, FileInfo fileB, FileCompareFlags flags)
		{
			bool isEqual = (fileA.EqualityFlags(fileB, flags) == flags);
			return isEqual;
		}

		//--------------------------------------------------------------------------------
		public static FileCompareFlags EqualityFlags(this FileInfo fileA, FileInfo fileB, FileCompareFlags flags)
		{
			FileCompareFlags equalFlags = FileCompareFlags.All;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.Attributes)    && fileA.Attributes        == fileB.Attributes)        ? FileCompareFlags.Attributes    : 0;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.Created)       && fileA.CreationTime      == fileB.CreationTime)      ? FileCompareFlags.Created       : 0;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.CreatedUTC)    && fileA.CreationTimeUtc   == fileB.CreationTimeUtc)   ? FileCompareFlags.CreatedUTC    : 0;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.Extension)     && fileA.Extension         == fileB.Extension)         ? FileCompareFlags.Extension     : 0;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.LastAccess)    && fileA.LastAccessTime    == fileB.LastAccessTime)    ? FileCompareFlags.LastAccess    : 0;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.LastAccessUTC) && fileA.LastAccessTimeUtc == fileB.LastAccessTimeUtc) ? FileCompareFlags.LastAccessUTC : 0;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.LastWrite)     && fileA.LastWriteTime     == fileB.LastWriteTime)     ? FileCompareFlags.LastWrite     : 0;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.LastWriteUTC)  && fileA.LastWriteTimeUtc  == fileB.LastWriteTimeUtc)  ? FileCompareFlags.LastWriteUTC  : 0;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.Length)        && fileA.Length            == fileB.Length)            ? FileCompareFlags.Length        : 0;
			equalFlags |= (FlagIsSet(flags, FileCompareFlags.FullName)      && fileA.FullName          == fileB.FullName)          ? FileCompareFlags.FullName      : 0;
			return equalFlags;
		}

		//////////////////////////////////////////////////////////////////////////////////
		// XElement methods                                                               

		//--------------------------------------------------------------------------------
		/// <summary>
		/// EXTENSION METHOD - Retrieves the value for the specified element name, and if 
		/// that element name doesn't exist, the default value is returned.
		/// </summary>
		/// <param name="root">The root element to parse</param>
		/// <param name="name">The element name we're looking for</param>
		/// <param name="defaultValue">The value to be returned if the named child element does not exist</param>
		/// <returns>True if the root element contains the specified name</returns>
		public static string GetValue(this XElement root, string name, string defaultValue)
		{
			return (string)root.Elements(name).FirstOrDefault() ?? defaultValue;
		}
		//--------------------------------------------------------------------------------
		public static double GetValue(this XElement root, string name, double defaultValue)
		{
			string strValue = (string)root.Elements(name).FirstOrDefault() ?? defaultValue.ToString();
			double value;
			if (!double.TryParse(strValue, out value))
			{
				throw new Exception(string.Format("Element {0}: Value retrieved was not a valid double", name));
			}
			return value;
		}
		//--------------------------------------------------------------------------------
		public static decimal GetValue(this XElement root, string name, decimal defaultValue)
		{
			string strValue = (string)root.Elements(name).FirstOrDefault() ?? defaultValue.ToString();
			decimal value;
			if (!decimal.TryParse(strValue, out value))
			{
				throw new Exception(string.Format("Element {0}: Value retrieved was not a valid decimal", name));
			}
			return value;
		}
		//--------------------------------------------------------------------------------
		public static Int32 GetValue(this XElement root, string name, Int32 defaultValue)
		{
			string strValue = (string)root.Elements(name).FirstOrDefault() ?? defaultValue.ToString();
			Int32 value;
			if (!Int32.TryParse(strValue, out value))
			{
				throw new Exception(string.Format("Element {0}: Value retrieved was not a valid 32-bit integer", name));
			}
			return value;
		}
		//--------------------------------------------------------------------------------
		public static bool GetValue(this XElement root, string name, bool defaultValue)
		{
			string strValue = (string)root.Elements(name).FirstOrDefault() ?? defaultValue.ToString();
			bool value;
			if (!bool.TryParse(strValue, out value))
			{
				throw new Exception(string.Format("Element {0}: Value retrieved was not a valid boolean", name));
			}
			return value;
		}
		//--------------------------------------------------------------------------------
		public static DateTime GetValue(this XElement root, string name, DateTime defaultValue)
		{
			string strValue = (string)root.Elements(name).FirstOrDefault() ?? defaultValue.ToString();
			DateTime value;
			if (!DateTime.TryParse(strValue, out value))
			{
				throw new Exception(string.Format("Element {0}: Value retrieved was not a valid DateTime", name));
			}
			return value;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// EXTENSION METHOD - Retrieves the value for the specified attribute name, and if 
		/// that attribute doesn't exist, the default value is returned.
		/// </summary>
		/// <param name="root">The root element to parse</param>
		/// <param name="name">The attribute name we're looking for</param>
		/// <param name="defaultValue">The value to be returned if the named child element does not exist</param>
		/// <returns>True if the root element contains the specified name</returns>
		public static string GetAttribute(this XElement root, string name, string defaultValue)
		{
			return (string)root.Attributes(name).FirstOrDefault() ?? defaultValue;
		}
		//--------------------------------------------------------------------------------
		public static double GetAttribute(this XElement root, string name, double defaultValue)
		{
			string strValue = (string)root.Attributes(name).FirstOrDefault() ?? defaultValue.ToString();
			double value;
			if (!double.TryParse(strValue, out value))
			{
				throw new Exception(string.Format("Attribute {0}: Value retrieved was not a valid double", name));
			}
			return value;
		}
		//--------------------------------------------------------------------------------
		public static decimal GetAttribute(this XElement root, string name, decimal defaultValue)
		{
			string strValue = (string)root.Attributes(name).FirstOrDefault() ?? defaultValue.ToString();
			decimal value;
			if (!decimal.TryParse(strValue, out value))
			{
				throw new Exception(string.Format("Attribute {0}: Value retrieved was not a valid decimal", name));
			}
			return value;
		}
		//--------------------------------------------------------------------------------
		public static Int32 GetAttribute(this XElement root, string name, Int32 defaultValue)
		{
			string strValue = (string)root.Attributes(name).FirstOrDefault() ?? defaultValue.ToString();
			Int32 value;
			if (!Int32.TryParse(strValue, out value))
			{
				throw new Exception(string.Format("Attribute {0}: Value retrieved was not a valid 32-bit integer", name));
			}
			return value;
		}
		//--------------------------------------------------------------------------------
		public static bool GetAttribute(this XElement root, string name, bool defaultValue)
		{
			string strValue = (string)root.Attributes(name).FirstOrDefault() ?? defaultValue.ToString();
			bool value;
			if (!bool.TryParse(strValue, out value))
			{
				throw new Exception(string.Format("Attribute {0}: Value retrieved was not a valid boolean", name));
			}
			return value;
		}
		//--------------------------------------------------------------------------------
		public static DateTime GetAttribute(this XElement root, string name, DateTime defaultValue)
		{
			string strValue = (string)root.Attributes(name).FirstOrDefault() ?? defaultValue.ToString();
			strValue = strValue.ToUpper().Replace("AS OF:", "").Trim();
			DateTime value;
			if (!DateTime.TryParse(strValue, out value))
			{
				throw new Exception(string.Format("Attribute {0}: Value retrieved was not a valid DateTime", name));
			}
			return value;
		}

	
	}
}
