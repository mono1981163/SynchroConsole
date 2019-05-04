using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Data;

/*========================================================================================
 * Author: John Simmons
 * Date: 01/28/2008
========================================================================================*/
namespace SynchroService
{
	/// <summary>
	/// Application log class - this class intiailizes the application 
	/// log and allows the programmer to log any desired event. Optionally, 
	/// the log level can be set, which allows the programmer to specify 
	/// levels of log verbosity.  When calling the appropriate version of 
	/// the SendToLog function, this class will ignore log events that 
	/// exceed the specified maximum specified log level.
	/// </summary>
	public class AppLog
	{
		public enum LogLevel { Quiet=0, Noise=1, Verbose=2 };
		public enum Errors   { Success=0, LogNameIsNull=1, SecurityException=2, RegularException=3, LogWriteException=4 }

		private EventLog m_eventLog    = null;
		private int      m_eventID     = 0;
		private bool     m_logError    = false;
		private Errors   m_errorCode   = Errors.Success;
		private string   m_name        = "";
		private bool     m_enabled     = false;
		private LogLevel m_maxLogLevel = LogLevel.Verbose;
		private string   m_prefix      = "";
		private string   m_logEntries  = "";

		//................................................................................
		/// <summary>
		/// Sets/gets the name of the log
		/// </summary>
		public string Name
		{
			get { return m_name; }
			set { m_name = value; }
		}
		//................................................................................
		/// <summary>
		/// Sets/gets whether the log is enabled
		/// </summary>
		public bool Enabled
		{
			get { return m_enabled; }
			set { m_enabled = value; }
		}
		//................................................................................
		/// <summary>
		/// Sets/gets the max log level
		/// </summary>
		public LogLevel MaxLogLevel
		{
			get { return m_maxLogLevel; }
			set { m_maxLogLevel = value; }
		}
		//................................................................................
		/// <summary>
		/// Sets/gets the log entry prefix
		/// </summary>
		public string Prefix
		{
			get { return m_prefix; }
			set { m_prefix = value; }
		}
		//................................................................................
		/// <summary>
		/// Gets the last log error
		/// </summary>
		public bool LogError
		{
			get { return m_logError; }
		}

		//................................................................................
		public string LogEntries
		{
			get { return m_logEntries; }
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Retrieves log name from the "LogName" appSettings key in the app.config 
		/// file.
		/// </summary>
		public AppLog()
		{
			Initialize("Paddedwall Application", LogLevel.Verbose);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Provide just the log name, and assume verbose log level with no prefix.
		/// </summary>
		/// <param name="name"></param>
		public AppLog(string name)
		{
			Initialize(name, LogLevel.Verbose);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// If you instantiate this class, it is assumed you want to enable the 
		/// log. You can disable later if you want to.
		/// </summary>
		/// <param name="name">The log name</param>
		/// <param name="level">The log level (should be one of the available enumerations in the LogLevel enum)</param>
		public AppLog(string name, LogLevel level)
		{
			Initialize(name, level);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// This overload sets the name and level of log output, as well as 
		/// specifying a prefix to add to each log entry.  The prefix is 
		/// mostly for apps that might be run many times in a given period, 
		/// and that subsequently make entries in the log under the same 
		/// name.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="level"></param>
		/// <param name="prefix"></param>
		public AppLog(string name, LogLevel level, string prefix)
		{
			this.Prefix = prefix;
			Initialize(name, level);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Casts the specified integer to an appropriate enum. If all else fails, 
		/// the enum will be returned as the specified default ordinal.
		/// </summary>
		/// <param name="value">The integer value representing an enumeration element</param>
		/// <param name="deafaultValue">The default enumertion to be used if the specified "value" does not exist in the enumeration definition</param>
		/// <returns></returns>
		public static T IntToEnum<T>(int value, T defaultValue)
		{
			T enumValue = (Enum.IsDefined(typeof(T), value)) ? (T)(object)value : defaultValue;
			return enumValue;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Initializes the system log.
		/// </summary>
		private void Initialize(string name, LogLevel level)
		{
			this.m_eventID   = 0;
			this.Name        = name;
			this.MaxLogLevel = LogLevel.Verbose;
			this.m_errorCode = Errors.Success;

			if (this.m_eventLog == null)
			{
				if (Name != "")
				{
					try
					{
						this.m_eventLog        = new EventLog();
						this.m_eventLog.Source = this.Name;
						if (!EventLog.SourceExists(this.m_eventLog.Source))
						{
							EventLog.CreateEventSource(this.m_eventLog.Source, this.m_eventLog.Source);
						}
					}
					catch (System.Security.SecurityException se)
					{
						if (se != null) { }
						this.m_errorCode = Errors.SecurityException;
					}
					catch (Exception e)
					{
						if (e != null) { }
						this.m_errorCode = Errors.RegularException;
					}
					finally
					{
						if (this.m_errorCode != Errors.Success)
						{
							if (this.m_eventLog != null)
							{
								this.m_eventLog.Dispose();
							}
						}
					}
				}
				else
				{
					this.m_errorCode = Errors.LogNameIsNull;
				}
			}
			Enabled = (this.m_eventLog != null);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Sends the specified message to the event log
		/// </summary>
		/// <param name="msg">The message to send</param>
		/// <param name="entryType">The message type (error, info, etc)</param>
		private void SendToLog(string msg, EventLogEntryType entryType)
		{
			if (this.Enabled && this.m_eventLog != null)
			{
				this.m_eventID++;
				try
				{
					string entireMsg = string.Format("{0} {1}", this.Prefix, msg);
					this.m_eventLog.WriteEntry(entireMsg, entryType, this.m_eventID);
				}
				catch (Exception ex)
				{
					if (ex != null) { }
					this.m_errorCode = Errors.LogWriteException;
				}
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Sends the specified message to the event log. The entry type 
		/// is "info" for this overload.
		/// </summary>
		/// <param name="msg">The message to be sent to the log</param>
		public void SendToLog(string msg)
		{
			SendToLog(msg, EventLogEntryType.Information);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Sends the specified message to the event log. The entry type 
		/// is "info" for this overload.
		/// </summary>
		/// <param name="msg">The message to be sent to the log</param>
		/// <param name="logLevel">The level of specified message message</param>
		public void SendToLog(string msg, LogLevel logLevel)
		{
			if (logLevel <= this.MaxLogLevel)
			{
				SendToLog(msg, EventLogEntryType.Information);
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Sends the specified message to the event log
		/// </summary>
		/// <param name="msg">The message to send</param>
		/// <param name="entryType">The message type (error, info, etc)</param>
		/// <param name="logLevel">The level of specified message message</param>
		public void SendToLog(string msg, EventLogEntryType entryType, LogLevel logLevel)
		{
			if (logLevel <= this.MaxLogLevel)
			{
				SendToLog(msg, entryType);
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Sends the specified error message to the event log. ALL errors are 
		/// sent to the log, regardless of the active logLevel.
		/// </summary>
		/// <param name="msg"></param>
		public void SendErrorToLog(string msg)
		{
			SendToLog(msg, EventLogEntryType.Error);
		}

		//--------------------------------------------------------------------------------
		public void ReadLogEntries(DateTime start, DateTime stop)
		{
			this.m_logEntries = "";
			int count = this.m_eventLog.Entries.Count;
			StringBuilder entries = new StringBuilder("");
			for (int i = 0; i < count; i++)
			{
				EventLogEntry entry = this.m_eventLog.Entries[i];
				DateTime written = entry.TimeWritten;
				if (written >= start && written <= stop)
				{
					entries.AppendFormat("{0}\t{1}\t{2}\n", 
										 entry.InstanceId, 
										 entry.EntryType.ToString(), 
										 entry.Message);
				}
			}
			this.m_logEntries = entries.ToString();
		}
		
		//--------------------------------------------------------------------------------
		/// <summary>
		/// Reads all log entries from the beginning of time until now.  When 
		/// you're done with the logentries string, call ClearLogEntriesString() 
		/// to free up the memory.
		/// </summary>
		public void ReadLogEntries()
		{
			DateTime start = new DateTime(0);
			DateTime stop  = DateTime.Now;
			ReadLogEntries(start, stop);
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// When you're done with the log entries, call this function to free 
		/// up the memory consumed by the string (the string could potentially 
		/// be quite large).
		/// </summary>
		public void ClearLogEntriesString()
		{
			this.m_logEntries = "";
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Creates a string DataColumn
		/// </summary>
		/// <param name="name"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		private DataColumn MakeColumn(string name, string defaultValue)
		{
			DataColumn col   = new DataColumn(name, System.Type.GetType("System.String"));
			col.AllowDBNull  = true;
			col.DefaultValue = defaultValue;
			return col;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Creates an int DataColumn
		/// </summary>
		/// <param name="name"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		private DataColumn MakeColumn(string name, int defaultValue)
		{
			DataColumn col   = new DataColumn(name, System.Type.GetType("System.Int32"));
			col.AllowDBNull  = true;
			col.DefaultValue = defaultValue;
			return col;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Creates a DateTime dataColumn
		/// </summary>
		/// <param name="name"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		private DataColumn MakeColumn(string name, DateTime defaultValue)
		{
			DataColumn col   = new DataColumn(name, System.Type.GetType("System.DateTime"));
			col.AllowDBNull  = true;
			col.DefaultValue = defaultValue;
			return col;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Creates a datatable and saves the data as an XML file
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public bool SaveEventLog(string fileName)
		{
			bool result = false;
			DateTime defaultDate = new DateTime(0);
			try
			{
				DataTable dt = new DataTable();
				dt.Columns.Add(MakeColumn("InstanceID",     ""));
				dt.Columns.Add(MakeColumn("TimeGenerated",  defaultDate));
				dt.Columns.Add(MakeColumn("TimeWritten",    defaultDate));
				dt.Columns.Add(MakeColumn("EntryType",      ""));
				dt.Columns.Add(MakeColumn("CategoryNumber", ""));
				dt.Columns.Add(MakeColumn("Category",       ""));
				dt.Columns.Add(MakeColumn("Message",        ""));
				dt.Columns.Add(MakeColumn("Index",          ""));
				dt.Columns.Add(MakeColumn("MachineName",    ""));
				dt.Columns.Add(MakeColumn("Source",         ""));
				dt.Columns.Add(MakeColumn("UserName",       ""));

				int count = this.m_eventLog.Entries.Count;
				for (int i = 0; i < count; i++)
				{
					EventLogEntry entry = m_eventLog.Entries[i];
					DataRow       row   = dt.NewRow();
					row["InstanceID"]		= entry.InstanceId.ToString();
					row["TimeGenerated"]	= entry.TimeGenerated;
					row["TimeWritten"]		= entry.TimeWritten;
					row["EntryType"]		= entry.EntryType.ToString();
					row["CategoryNumber"]	= entry.CategoryNumber.ToString();
					row["Category"]			= (entry.Category == "") ? "None" : entry.Category;
					row["Message"]			= entry.Message;
					row["Index"]			= entry.Index.ToString();
					row["MachineName"]		= entry.MachineName;
					row["Source"]			= entry.Source;
					row["UserName"]			= entry.UserName;
				}
				if (dt.Rows.Count > 0)
				{
					dt.WriteXml(fileName);
				}
				result = true;
			}
			catch (Exception ex)
			{
				if (ex != null) { }
			}
			return result;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Clears the log (after optionally saving the log entries to an XML file)
		/// </summary>
		/// <param name="saveAsFile"></param>
		public void ClearLog(string saveAsFile)
		{
			bool fileSaved = true;
			if (saveAsFile != "")
			{
				fileSaved = SaveEventLog(saveAsFile);
				if (!fileSaved)
				{
					// something broke in the SaveEventLog function
				}
			}
			if (fileSaved)
			{
				try
				{
					this.m_eventLog.Clear();
				}
				catch (Exception e)
				{
					if (e != null) { }
				}
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Sends the "Started" message to the log
		/// </summary>
		public void Start()
		{
			SendToLog("Started...");
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Sends the "Stopped" message to the log
		/// </summary>
		public void Stop()
		{
			SendToLog("Stopped...");
		}
	}
}
