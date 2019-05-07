using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

#if (__USE_THREAD_POOL__)
// if you want to use the SmartThreadPool, and the compiler failed here, you neeed to 
// include th SmartThreadPool assembly (from the OtherAssemblies folder). 
using Amib.Threading;
#endif

using System.Threading;

namespace SynchroLib
{
	public class SyncItemCollection : List<SyncItem>
	{
		//................................................................................
		/// <summary>
		/// Get/set the list of sync items via their XElement value
		/// </summary>
		public XElement XElement
		{
			get
			{
				XElement value = new XElement("SyncItems");
				foreach(SyncItem item in this)
				{
					value.Add(item.XElement);
				}
				return value;
			}
			set
			{
				if (value != null)
				{
					foreach(XElement element in value.Elements())
					{
						this.Add(new SyncItem(element));
					}
				}
			}
		}

#if (__USE_THREAD_POOL__)
		#region Threadpool Code
		/// <summary>
		/// Get/set the thread pool
		/// </summary>
		public SmartThreadPool Pool              { get; set; }
		/// <summary>
		/// Get/set the number of concurrent threads that can be running at once
		/// </summary>
		public int             ConcurrentThreads { get; set; }
		/// <summary>
		/// Get/set the maximum number pf threads we will be running
		/// </summary>
		public int             MaxThreads        { get; set; }
		/// <summary>
		/// Get/set the default thread pool startup criteria
		/// </summary>
		public STPStartInfo    StpStartInfo      { get; set; }
		/// <summary>
		/// Get/set how long the thread pool is idle before it times out
		/// </summary>
		public int             PoolIdleTimeout   { get; set; }
		/// <summary>
		/// Get/set the progress reporting frequency to assign to all threads.
		/// </summary>
		public int             ProgressFrequency { get; set; }

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		public SyncItemCollection()
		{
			Init();
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="value"></param>
		public SyncItemCollection(XElement value)
		{
			XElement = value;
			Init();
		}

		//--------------------------------------------------------------------------------
		private void Init()
		{
			this.Pool              = null;
			this.ConcurrentThreads = 1;
			this.MaxThreads        = 1;
			this.PoolIdleTimeout   = 1000;
			this.ProgressFrequency = 10;

			this.StpStartInfo                                = new STPStartInfo();
			this.StpStartInfo.IdleTimeout                    = this.PoolIdleTimeout;
			this.StpStartInfo.MaxWorkerThreads               = this.ConcurrentThreads;
			this.StpStartInfo.StartSuspended                 = true;
			this.StpStartInfo.PerformanceCounterInstanceName = "SmartThreadPool";
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Executes the processes in the thread pool 
		/// </summary>
		public void StartUpdate()
		{
			if (this.Pool != null)
			{
				QueueWorkItems();
				this.Pool.Start();
				while (this.Pool.InUseThreads > 0)
				{
					Thread.Sleep(1000);
				}
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Places the threads into the thread pool queue
		/// </summary>
		private void QueueWorkItems()
		{
			if (this.Pool != null)
			{
				foreach(SyncItem item in this)
				{
					IWorkItemResult workItem = item.QueueProcess(this.Pool);
				}
			}
		}
		#endregion Threadpool Code
#else

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		public SyncItemCollection()
		{
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="value"></param>
		public SyncItemCollection(XElement value)
		{
			XElement = value;
		}

		//--------------------------------------------------------------------------------
		public void StartUpdate()
		{
			if (this.Count > 0)
			{
				foreach(SyncItem item in this)
				{
					item.Start();
				}
			}
		}
#endif

	}
}
