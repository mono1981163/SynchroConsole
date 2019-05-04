using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Windows.Forms;

#if (__USE_THREAD_POOL__)
// if you want to use the SmartThreadPool, and the compiler failed here, you neeed to 
// include th SmartThreadPool assembly (from the OtherAssemblies folder). 
using Amib.Threading;
#endif

using System.Threading;

namespace SynchroLib
{
	//////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// 
	/// </summary>
	public class SyncItem
	{
		#region Threadpool data members
#if __USE_THREADPOOL__ 
		public  object          threadPoolState = null;
		public  IWorkItemResult workItemResult  = null;
#endif
		#endregion Threadpool data members

		#region Data properties
		public bool   SyncSubfolders   { get; set; }
		public bool   DeleteAfterSync  { get; set; }
		public bool   BackupBeforeSync { get; set; }
		public string Name             { get; set; }
		public string SyncFromPath     { get; set; }
		public string SyncToPath       { get; set; }
		public string BackupPath       { get; set; }
		public bool   Enabled          { get; set; }
        // Rui_Add
        public bool Writable           { get; set; }
        public string DeletedDirOrFile { get; set; }
		public XElement XElement
		{
			get
			{
				XElement value = new XElement("SyncItem"
											  ,new XElement("Name",             this.Name)
											  ,new XElement("SyncFromPath",     this.SyncFromPath)
											  ,new XElement("SyncToPath",       this.SyncToPath)
											  ,new XElement("BackupPath",       this.BackupPath)
											  ,new XElement("Enabled",          this.Enabled)
											  ,new XElement("SyncSubFolders",   this.SyncSubfolders)
											  ,new XElement("BackupBeforeSync", this.BackupBeforeSync)
											  ,new XElement("DeleteAfterSync",  this.DeleteAfterSync)
                                              // Rui_Add
                                              ,new XElement("Writable",         this.Writable)
                                              ,new XElement("DeletedDirOrFile", this.DeletedDirOrFile)
                                             );
				return value;
			}
			set
			{
				if (value != null)
				{
					this.Name             = value.GetValue("Name",             Guid.NewGuid().ToString("N"));
					this.SyncFromPath     = value.GetValue("SyncFromPath",     "");
					this.SyncToPath       = value.GetValue("SyncToPath",       "");
					this.BackupPath       = value.GetValue("backupPath",       "");
					this.Enabled          = value.GetValue("Enabled",          true);
					this.SyncSubfolders   = value.GetValue("SyncSubFolders",   true);
					this.BackupBeforeSync = value.GetValue("BackupBeforeSync", false);
					this.DeleteAfterSync  = value.GetValue("DeleteAfterSync",  false);
                    // Rui_Add
                    this.Writable         = value.GetValue("Writable",         false);
                    this.DeletedDirOrFile = value.GetValue("DeletedDirOrFile", "");
                }
            }
		}
		#endregion Data properties

		#region Control properties
		TimeSpan      UpdateInterval { get; set; } 
		DatePartFlags EqualityFlags  { get; set; } 
		FileInfoList  ToFilesList    { get; set; }
		int           UpdatesMade    { get; set; }

		//................................................................................
		/// <summary>
		/// 
		/// </summary>
		public bool CanStartSync 
		{
			get
			{
				bool canSync = false;
				if (Directory.Exists(this.SyncFromPath) && 
					Directory.Exists(this.SyncToPath) &&
					this.SyncFromPath.ToLower() != this.SyncToPath.ToLower() &&
					this.Enabled)
				{
					canSync = true;
				}
				return canSync;
			}
		}
		#endregion Data properties

#if (!__USE_THREADPOOL__)
		public Thread SyncThread { get; set; }
#endif
		public event FileInfoHandler FileInfoEvent = delegate { };

		#region Constructor/init
		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		public SyncItem()
		{
		    this.SyncSubfolders   = true;
		    this.DeleteAfterSync  = false;
		    this.BackupBeforeSync = false;
		    this.Name             = "";
		    this.SyncFromPath     = "";
		    this.SyncToPath       = "";
            // Rui add
            this.Writable         = false;
            this.DeletedDirOrFile = "";
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		public SyncItem(string from, string to)
		{
			this.SyncSubfolders   = true;
			this.DeleteAfterSync  = false;
			this.BackupBeforeSync = false;
			this.Name             = "";
			this.SyncFromPath     = from;
			this.SyncToPath       = to;
            // Rui add
            this.Writable         = false;
            this.DeletedDirOrFile = "";
			Init();
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="value"></param>
		public SyncItem(XElement value)
		{
			this.XElement = value;
			Init();
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Initializes the synchronier thread and the list of files that already exist 
		/// in the target folder.
		/// </summary>
		public void Init()
		{
#if (!__USE_THREADPOOL__)
			if (this.SyncThread != null)
			{
			    this.SyncThread.Abort();
			    this.SyncThread = null;
			}
			this.SyncThread              = new Thread(new ThreadStart(SyncFiles));
			this.SyncThread.IsBackground = true;
#endif
			if (this.CanStartSync)
			{
				this.ToFilesList = new FileInfoList(this);
				this.ToFilesList.GetFiles(this.SyncToPath, this.SyncSubfolders);
			}
		}
		#endregion Constructor/init

#if (!__USE_THREADPOOL__)
		//--------------------------------------------------------------------------------
		/// <summary>
		/// Starts the synchronizer thread
		/// </summary>
		public void Start()
		{
		    Debug.WriteLine("{0} STARTED ====================", this.Name);
		    if (this.SyncThread == null || this.SyncThread.ThreadState != System.Threading.ThreadState.Unstarted)
		    {
		        this.SyncThread = new Thread(new ThreadStart(SyncFiles));
		        this.SyncThread.IsBackground = true;
		    }
		    this.SyncThread.Start();
		}
#endif

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Stops the synchronizer thread
		/// </summary>
		public void Stop()
		{
			Debug.WriteLine("==================== {0} STOPPED", this.Name);
#if (__USE_THREADPOOL__)
			this.workItemResult.Cancel();
#else
			this.SyncThread.Abort();
#endif
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Synchronizer thread delegate - updates the files in the target folder, and 
		/// posts an event when it's done.
		/// </summary>
		private void SyncFiles()
		{
		    if (this.CanStartSync)
		    {
		        try
		        {
		            DateTime before  = DateTime.Now;
		            this.ToFilesList.Update(this.SyncFromPath, this.SyncSubfolders);
		            DateTime after   = DateTime.Now;
		            TimeSpan elapsed = after - before;
		            int      updates = this.ToFilesList.Updates;
		            FileInfoEvent(this, new FileInfoArgs(updates, elapsed));
		        }
		        catch (ThreadAbortException ex)
		        {
		            if (ex != null) {}
		        }
		        catch (Exception)
		        {
		            throw;
		        }
		    }
            
		}
       
		//--------------------------------------------------------------------------------
		/// <summary>
		/// Synchronizer thread delegate - updates the files in the target folder, and 
		/// posts an event when it's done.  Use this overload if you're managing the 
		/// threads with the SmartThreadPool object
		/// </summary>
		private object SyncFiles(object state)
		{
			if (this.CanStartSync)
			{
				try
				{
					DateTime before  = DateTime.Now;
					this.ToFilesList.Update(this.SyncFromPath, this.SyncSubfolders);
					DateTime after   = DateTime.Now;
					TimeSpan elapsed = after - before;
					this.UpdatesMade = this.ToFilesList.Updates;
					FileInfoEvent(this, new FileInfoArgs(this.UpdatesMade, elapsed));
				}
				catch (ThreadAbortException ex)
				{
					if (ex != null) {}
				}
				catch (Exception)
				{
					throw;
				}
			}
			return state;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Returns the string representation of this object, namely, the Name property.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return this.Name;
		}

#if (__USE_THREADPOOL__)
		//--------------------------------------------------------------------------------
		/// <summary>
		/// Queues this object in the thread pool.
		/// </summary>
		/// <param name="pool"></param>
		/// <returns></returns>
		public IWorkItemResult QueueProcess(SmartThreadPool pool)
		{
			workItemResult = null;
			if (pool != null)
			{
				try
				{
					if (threadPoolState == null)
					{
						threadPoolState = new object();
					}
					workItemResult = pool.QueueWorkItem(new WorkItemCallback(this.SyncFiles), WorkItemPriority.Normal);
				}
				catch (Exception ex)
				{
					ex.Data.Add("Name",  this.Name);
					throw ex;
				}
			}
			return workItemResult;
		}
#endif

	}
}
